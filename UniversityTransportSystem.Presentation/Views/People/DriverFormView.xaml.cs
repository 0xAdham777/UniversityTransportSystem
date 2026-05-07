using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class DriverFormView : UserControl
{
    private BaseService<Driver> _driverService;
    private Driver? _editingDriver;

    public DriverFormView()
    {
        InitializeComponent();
        _driverService = new BaseService<Driver>(new DriverRepository());
    }

    public void SetDriver(Driver driver)
    {
        _editingDriver = driver;
        PopulateFields();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingDriver == null) return;

        txtEmployeeID.Text = _editingDriver.EmployeeID.ToString();
        txtLicenseNumber.Text = _editingDriver.LicenseNumber;
        dpLicenseExpiryDate.SelectedDate = _editingDriver.LicenseExpiryDate;
        cmbDriverStatus.SelectedIndex = _editingDriver.DriverStatus ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var isActive = cmbDriverStatus.SelectedIndex == 0;
        txtBadgeStatus.Text = isActive ? "Active" : "Inactive";
        txtPreviewLicense.Text = string.IsNullOrWhiteSpace(txtLicenseNumber.Text)
            ? "License: --"
            : $"License: {txtLicenseNumber.Text}";
        badgeStatus.Background = new SolidColorBrush(isActive ? Color.FromRgb(0x1D, 0xE5, 0xA8) : Color.FromRgb(0xE5, 0x4D, 0x6A));
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DriverListView());
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var driver = new Driver
            {
                EmployeeID = int.Parse(txtEmployeeID.Text.Trim()),
                LicenseNumber = txtLicenseNumber.Text.Trim(),
                LicenseExpiryDate = dpLicenseExpiryDate.SelectedDate ?? DateTime.Now,
                DriverStatus = cmbDriverStatus.SelectedIndex == 0
            };

            var id = await _driverService.InsertAsync(driver);
            if (id > 0)
            {
                MessageBox.Show("Driver created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new DriverListView());
            }
            else
            {
                MessageBox.Show("Failed to create driver.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingDriver == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingDriver.EmployeeID = int.Parse(txtEmployeeID.Text.Trim());
            _editingDriver.LicenseNumber = txtLicenseNumber.Text.Trim();
            _editingDriver.LicenseExpiryDate = dpLicenseExpiryDate.SelectedDate ?? DateTime.Now;
            _editingDriver.DriverStatus = cmbDriverStatus.SelectedIndex == 0;

            var success = await _driverService.UpdateAsync(_editingDriver);
            if (success)
            {
                MessageBox.Show("Driver updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new DriverListView());
            }
            else
            {
                MessageBox.Show("Failed to update driver.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtEmployeeID.Text, out _))
        {
            MessageBox.Show("Valid Employee ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
        {
            MessageBox.Show("License Number is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpLicenseExpiryDate.SelectedDate == null)
        {
            MessageBox.Show("License Expiry Date is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbDriverStatus.SelectedIndex < 0)
        {
            MessageBox.Show("Driver Status is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        txtEmployeeID.Clear();
        txtLicenseNumber.Clear();
        dpLicenseExpiryDate.SelectedDate = null;
        cmbDriverStatus.SelectedIndex = -1;
        _editingDriver = null;
        btnSave.Visibility = Visibility.Visible;
        btnUpdate.Visibility = Visibility.Collapsed;
        UpdatePreview();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DriverListView());
    }
}
