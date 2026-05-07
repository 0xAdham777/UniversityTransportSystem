using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;
using UniversityTransportSystem.Presentation.Converters;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class PersonListView : UserControl
{
    private BaseService<Person> _personService;
    private List<Person> _allPersons;

    public PersonListView()
    {
        InitializeComponent();
        _personService = new BaseService<Person>(new PersonRepository());
        _allPersons = new List<Person>();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        if (storyboard != null)
            BeginStoryboard(storyboard);

    
        dgPersons.ItemsSource = _allPersons;
        txtTotalPersons.Text = "0";
        txtActivePersons.Text = "0";

        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            _allPersons = await _personService.GetAllAsync();
            if (_allPersons.Count > 0)
            {
                dgPersons.ItemsSource = _allPersons;
                txtTotalPersons.Text = _allPersons.Count.ToString();
                txtActivePersons.Text = _allPersons.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"DB Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = txtSearch.Text?.ToLower() ?? "";
        if (string.IsNullOrWhiteSpace(filter))
        {
            dgPersons.ItemsSource = _allPersons;
        }
        else
        {
            dgPersons.ItemsSource = _allPersons.Where(p =>
                (p.FirstName?.ToLower().Contains(filter) ?? false) ||
                (p.LastName?.ToLower().Contains(filter) ?? false) ||
                (p.Email?.ToLower().Contains(filter) ?? false) ||
                (p.PhoneNumber?.ToLower().Contains(filter) ?? false)).ToList();
        }
    }

    private void DgPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new PersonFormView());
    }

    private void BtnEditPerson_Click(object sender, RoutedEventArgs e)
    {
        if (dgPersons.SelectedItem is Person person)
        {
            var form = new PersonFormView();
            form.SetPerson(person);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a person to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
    {
        if (dgPersons.SelectedItem is not Person person)
        {
            MessageBox.Show("Please select a person to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete {person.FirstName} {person.LastName}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _personService.DeleteAsync(person.PersonID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete person.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting person: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
