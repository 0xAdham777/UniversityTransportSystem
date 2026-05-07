using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class EmployeeFormView : UserControl
{
    private BaseService<Employee> _employeeService;
    private Employee? _editingEmployee;

    public EmployeeFormView()
    {
        InitializeComponent();
        _employeeService = new BaseService<Employee>(new EmployeeRepository());
    }

    public void SetEmployee(Employee employee)
    {
        _editingEmployee = employee;
        PopulateFields();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingEmployee == null) return;

        txtPersonID.Text = _editingEmployee.PersonID.ToString();
        dpHireDate.SelectedDate = _editingEmployee.HireDate;
        cmbEmployeeStatus.SelectedIndex = _editingEmployee.EmployeeStatus ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var isActive = cmbEmployeeStatus.SelectedIndex == 0;
        txtBadgeStatus.Text = isActive ? "Active" : "Inactive";
        txtPreviewStatus.Text = isActive ? "Active Employee" : "Inactive Employee";
        badgeStatus.Background = new SolidColorBrush(isActive ? Color.FromRgb(0x1D, 0xE5, 0xA8) : Color.FromRgb(0xE5, 0x4D, 0x6A));
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new EmployeeListView());
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var employee = new Employee
            {
                PersonID = int.Parse(txtPersonID.Text.Trim()),
                HireDate = dpHireDate.SelectedDate ?? DateTime.Now,
                EmployeeStatus = cmbEmployeeStatus.SelectedIndex == 0
            };

            var id = await _employeeService.InsertAsync(employee);
            if (id > 0)
            {
                MessageBox.Show("Employee created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new EmployeeListView());
            }
            else
            {
                MessageBox.Show("Failed to create employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingEmployee == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingEmployee.PersonID = int.Parse(txtPersonID.Text.Trim());
            _editingEmployee.HireDate = dpHireDate.SelectedDate ?? DateTime.Now;
            _editingEmployee.EmployeeStatus = cmbEmployeeStatus.SelectedIndex == 0;

            var success = await _employeeService.UpdateAsync(_editingEmployee);
            if (success)
            {
                MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new EmployeeListView());
            }
            else
            {
                MessageBox.Show("Failed to update employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtPersonID.Text, out _))
        {
            MessageBox.Show("Valid Person ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpHireDate.SelectedDate == null)
        {
            MessageBox.Show("Hire Date is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbEmployeeStatus.SelectedIndex < 0)
        {
            MessageBox.Show("Employee Status is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        txtPersonID.Clear();
        dpHireDate.SelectedDate = null;
        cmbEmployeeStatus.SelectedIndex = -1;
        _editingEmployee = null;
        btnSave.Visibility = Visibility.Visible;
        btnUpdate.Visibility = Visibility.Collapsed;
        UpdatePreview();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new EmployeeListView());
    }
}
