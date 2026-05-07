using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class ScheduleFormView : UserControl
{
    private BaseService<Schedule> _service;
    private Schedule? _editingItem;

    public ScheduleFormView()
    {
        InitializeComponent();
        _service = new BaseService<Schedule>(new ScheduleRepository());
        Loaded += OnLoaded;
    }

    public void SetSchedule(Schedule item)
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

        txtTransportLineID.Text = _editingItem.TransportLineID.ToString();
        foreach (ComboBoxItem comboItem in cmbDayOfWeek.Items)
        {
            if (comboItem.Content.ToString() == _editingItem.DayOfWeek)
            {
                cmbDayOfWeek.SelectedItem = comboItem;
                break;
            }
        }
        txtDepartureTime.Text = _editingItem.DepartureTime.ToString(@"hh\:mm");
        txtArrivalTime.Text = _editingItem.ArrivalTime.ToString(@"hh\:mm");
        cmbScheduleStatus.SelectedIndex = _editingItem.ScheduleStatus ? 0 : 1;

        btnSave.Content = "Update";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(txtTransportLineID.Text, out var lineId))
        {
            MessageBox.Show("Valid TransportLineID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!TimeSpan.TryParse(txtDepartureTime.Text, out var depTime))
        {
            MessageBox.Show("Valid Departure Time (HH:mm) is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!TimeSpan.TryParse(txtArrivalTime.Text, out var arrTime))
        {
            MessageBox.Show("Valid Arrival Time (HH:mm) is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var dayOfWeek = (cmbDayOfWeek.SelectedItem as ComboBoxItem)?.Content?.ToString();
        if (string.IsNullOrEmpty(dayOfWeek))
        {
            MessageBox.Show("Day Of Week is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Schedule
                {
                    TransportLineID = lineId,
                    DayOfWeek = dayOfWeek,
                    DepartureTime = depTime,
                    ArrivalTime = arrTime,
                    ScheduleStatus = cmbScheduleStatus.SelectedIndex == 0
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Schedule created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create schedule.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.TransportLineID = lineId;
                _editingItem.DayOfWeek = dayOfWeek;
                _editingItem.DepartureTime = depTime;
                _editingItem.ArrivalTime = arrTime;
                _editingItem.ScheduleStatus = cmbScheduleStatus.SelectedIndex == 0;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Schedule updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update schedule.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new ScheduleListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Schedule Management";
        }
    }
}
