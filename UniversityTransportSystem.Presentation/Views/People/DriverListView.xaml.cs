using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class DriverListView : UserControl
{
    private BaseService<Driver> _driverService;
    private List<Driver> _allDrivers;

    public DriverListView()
    {
        InitializeComponent();
        _driverService = new BaseService<Driver>(new DriverRepository());
        _allDrivers = new List<Driver>();
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
            _allDrivers = await _driverService.GetAllAsync();
            dgDrivers.ItemsSource = _allDrivers;
            txtTotalDrivers.Text = _allDrivers.Count.ToString();
            txtActiveDrivers.Text = _allDrivers.Count(d => d.DriverStatus).ToString();
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
            dgDrivers.ItemsSource = _allDrivers;
        else
            dgDrivers.ItemsSource = _allDrivers.Where(d =>
                d.LicenseNumber?.ToLower().Contains(filter) ?? false ||
                d.DriverID.ToString().Contains(filter)).ToList();
    }

    private void DgDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAddDriver_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DriverFormView());
    }

    private void BtnEditDriver_Click(object sender, RoutedEventArgs e)
    {
        if (dgDrivers.SelectedItem is Driver driver)
        {
            var form = new DriverFormView();
            form.SetDriver(driver);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a driver to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDeleteDriver_Click(object sender, RoutedEventArgs e)
    {
        if (dgDrivers.SelectedItem is not Driver driver)
        {
            MessageBox.Show("Please select a driver to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete Driver #{driver.DriverID}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _driverService.DeleteAsync(driver.DriverID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete driver.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting driver: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
