using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class TripFormView : UserControl
{
    private BaseService<Trip> _service;
    private Trip? _editingItem;

    public TripFormView()
    {
        InitializeComponent();
        _service = new BaseService<Trip>(new TripRepository());
        Loaded += OnLoaded;
    }

    public void SetTrip(Trip item)
    {
        _editingItem = item;
        PopulateFields();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var storyboard = TryFindResource("PageEnterStoryboard") as Storyboard;
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingItem == null) return;

        txtBusID.Text = _editingItem.BusID.ToString();
        txtDriverID.Text = _editingItem.DriverID.ToString();
        txtTransportLineID.Text = _editingItem.TransportLineID.ToString();
        txtScheduleID.Text = _editingItem.ScheduleID.ToString();
        dpTripDate.SelectedDate = _editingItem.TripDate;
        txtActualDepartureTime.Text = _editingItem.ActualDepartureTime?.ToString(@"hh\:mm\:ss") ?? "";
        txtActualArrivalTime.Text = _editingItem.ActualArrivalTime?.ToString(@"hh\:mm\:ss") ?? "";
        cmbTripStatus.SelectedIndex = _editingItem.TripStatus ? 0 : 1;
        txtDelayInMinutes.Text = _editingItem.DelayInMinutes.ToString();

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new TripListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Trip Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new Trip
            {
                BusID = int.Parse(txtBusID.Text.Trim()),
                DriverID = int.Parse(txtDriverID.Text.Trim()),
                TransportLineID = int.Parse(txtTransportLineID.Text.Trim()),
                ScheduleID = int.Parse(txtScheduleID.Text.Trim()),
                TripDate = dpTripDate.SelectedDate ?? DateTime.Now,
                ActualDepartureTime = ParseTimeSpan(txtActualDepartureTime.Text),
                ActualArrivalTime = ParseTimeSpan(txtActualArrivalTime.Text),
                TripStatus = cmbTripStatus.SelectedIndex == 0,
                DelayInMinutes = int.Parse(txtDelayInMinutes.Text.Trim())
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Trip created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create trip.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingItem == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingItem.BusID = int.Parse(txtBusID.Text.Trim());
            _editingItem.DriverID = int.Parse(txtDriverID.Text.Trim());
            _editingItem.TransportLineID = int.Parse(txtTransportLineID.Text.Trim());
            _editingItem.ScheduleID = int.Parse(txtScheduleID.Text.Trim());
            _editingItem.TripDate = dpTripDate.SelectedDate ?? DateTime.Now;
            _editingItem.ActualDepartureTime = ParseTimeSpan(txtActualDepartureTime.Text);
            _editingItem.ActualArrivalTime = ParseTimeSpan(txtActualArrivalTime.Text);
            _editingItem.TripStatus = cmbTripStatus.SelectedIndex == 0;
            _editingItem.DelayInMinutes = int.Parse(txtDelayInMinutes.Text.Trim());

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Trip updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update trip.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private TimeSpan? ParseTimeSpan(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        if (TimeSpan.TryParse(input, out var result)) return result;
        return null;
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtBusID.Text, out _))
        {
            MessageBox.Show("Valid BusID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtDriverID.Text, out _))
        {
            MessageBox.Show("Valid DriverID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtTransportLineID.Text, out _))
        {
            MessageBox.Show("Valid TransportLineID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtScheduleID.Text, out _))
        {
            MessageBox.Show("Valid ScheduleID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpTripDate.SelectedDate == null)
        {
            MessageBox.Show("TripDate is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbTripStatus.SelectedIndex < 0)
        {
            MessageBox.Show("TripStatus is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtDelayInMinutes.Text, out _))
        {
            MessageBox.Show("Valid DelayInMinutes is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
