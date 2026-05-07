using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class EmployeeListView : UserControl
{
    private BaseService<Employee> _employeeService;
    private List<Employee> _allEmployees;

    public EmployeeListView()
    {
        InitializeComponent();
        _employeeService = new BaseService<Employee>(new EmployeeRepository());
        _allEmployees = new List<Employee>();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            _allEmployees = await _employeeService.GetAllAsync();
            dgEmployees.ItemsSource = _allEmployees;
            txtTotalEmployees.Text = _allEmployees.Count.ToString();
            txtActiveEmployees.Text = _allEmployees.Count(e => e.EmployeeStatus).ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = txtSearch.Text?.ToLower() ?? "";
        if (string.IsNullOrWhiteSpace(filter))
            dgEmployees.ItemsSource = _allEmployees;
        else
            dgEmployees.ItemsSource = _allEmployees.Where(emp =>
                emp.EmployeeID.ToString().Contains(filter)).ToList();
    }

    private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new EmployeeFormView());
    }

    private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
    {
        if (dgEmployees.SelectedItem is Employee employee)
        {
            var form = new EmployeeFormView();
            form.SetEmployee(employee);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select an employee to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
    {
        if (dgEmployees.SelectedItem is not Employee employee)
        {
            MessageBox.Show("Please select an employee to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete Employee #{employee.EmployeeID}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _employeeService.DeleteAsync(employee.EmployeeID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
