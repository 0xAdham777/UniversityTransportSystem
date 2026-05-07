using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class PersonFormView : UserControl
{
    private BaseService<Person> _personService;
    private Person? _editingPerson;

    public PersonFormView()
    {
        InitializeComponent();
        _personService = new BaseService<Person>(new PersonRepository());
    }

    public void SetPerson(Person person)
    {
        _editingPerson = person;
        PopulateFields();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingPerson == null) return;

        txtFirstName.Text = _editingPerson.FirstName;
        txtMidName.Text = _editingPerson.MidName;
        txtLastName.Text = _editingPerson.LastName;
        dpDateOfBirth.SelectedDate = _editingPerson.DateOfBirth;
        cmbGender.SelectedIndex = _editingPerson.Gender ? 0 : 1;
        txtPhoneNumber.Text = _editingPerson.PhoneNumber;
        txtEmail.Text = _editingPerson.Email;
        txtAddress.Text = _editingPerson.Address;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var firstName = txtFirstName.Text;
        var lastName = txtLastName.Text;
        txtPreviewName.Text = $"{firstName} {txtMidName.Text} {lastName}".Trim();
        txtPreviewInitials.Text = $"{(firstName.Length > 0 ? firstName[0].ToString() : "")}{(lastName.Length > 0 ? lastName[0].ToString() : "")}";
        txtPreviewEmail.Text = txtEmail.Text;
        txtPreviewDOB.Text = dpDateOfBirth.SelectedDate?.ToString("yyyy-MM-dd") ?? "--";
        txtPreviewGender.Text = cmbGender.SelectedItem is ComboBoxItem item ? item.Content.ToString() : "--";
        txtPreviewPhone.Text = txtPhoneNumber.Text;
        txtPreviewAddress.Text = txtAddress.Text;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new PersonListView());
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var person = new Person
            {
                FirstName = txtFirstName.Text.Trim(),
                MidName = txtMidName.Text?.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = dpDateOfBirth.SelectedDate,
                Gender = cmbGender.SelectedIndex == 0,
                PhoneNumber = txtPhoneNumber.Text?.Trim(),
                Email = txtEmail.Text?.Trim(),
                Address = txtAddress.Text?.Trim()
            };

            var id = await _personService.InsertAsync(person);
            if (id > 0)
            {
                MessageBox.Show("Person created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new PersonListView());
            }
            else
            {
                MessageBox.Show("Failed to create person.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingPerson == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingPerson.FirstName = txtFirstName.Text.Trim();
            _editingPerson.MidName = txtMidName.Text?.Trim();
            _editingPerson.LastName = txtLastName.Text.Trim();
            _editingPerson.DateOfBirth = dpDateOfBirth.SelectedDate;
            _editingPerson.Gender = cmbGender.SelectedIndex == 0;
            _editingPerson.PhoneNumber = txtPhoneNumber.Text?.Trim();
            _editingPerson.Email = txtEmail.Text?.Trim();
            _editingPerson.Address = txtAddress.Text?.Trim();

            var success = await _personService.UpdateAsync(_editingPerson);
            if (success)
            {
                MessageBox.Show("Person updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new PersonListView());
            }
            else
            {
                MessageBox.Show("Failed to update person.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        {
            MessageBox.Show("First Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Last Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbGender.SelectedIndex < 0)
        {
            MessageBox.Show("Gender is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        txtFirstName.Clear();
        txtMidName.Clear();
        txtLastName.Clear();
        dpDateOfBirth.SelectedDate = null;
        cmbGender.SelectedIndex = -1;
        txtPhoneNumber.Clear();
        txtEmail.Clear();
        txtAddress.Clear();
        _editingPerson = null;
        btnSave.Visibility = Visibility.Visible;
        btnUpdate.Visibility = Visibility.Collapsed;
        UpdatePreview();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new PersonListView());
    }
}
