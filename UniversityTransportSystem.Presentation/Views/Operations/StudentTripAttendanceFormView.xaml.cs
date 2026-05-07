using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class StudentTripAttendanceFormView : UserControl
{
    private BaseService<StudentTripAttendance> _service;
    private StudentTripAttendance? _editingItem;

    public StudentTripAttendanceFormView()
    {
        InitializeComponent();
        _service = new BaseService<StudentTripAttendance>(new StudentTripAttendanceRepository());
        Loaded += OnLoaded;
    }

    public void SetAttendance(StudentTripAttendance item)
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

        txtStudentID.Text = _editingItem.StudentID.ToString();
        txtTripID.Text = _editingItem.TripID.ToString();
        txtBoardingStationID.Text = _editingItem.BoardingStationID?.ToString() ?? "";
        txtDropOffStationID.Text = _editingItem.DropOffStationID?.ToString() ?? "";
        txtBoardingTime.Text = _editingItem.BoardingTime?.ToString(@"hh\:mm\:ss") ?? "";
        txtDropOffTime.Text = _editingItem.DropOffTime?.ToString(@"hh\:mm\:ss") ?? "";
        cmbAttendanceStatus.SelectedIndex = _editingItem.AttendanceStatus ? 0 : 1;
        txtNotes.Text = _editingItem.Notes ?? "";

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new AttendanceListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Student Trip Attendance Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new StudentTripAttendance
            {
                StudentID = int.Parse(txtStudentID.Text.Trim()),
                TripID = int.Parse(txtTripID.Text.Trim()),
                BoardingStationID = ParseNullableInt(txtBoardingStationID.Text),
                DropOffStationID = ParseNullableInt(txtDropOffStationID.Text),
                BoardingTime = ParseTimeSpan(txtBoardingTime.Text),
                DropOffTime = ParseTimeSpan(txtDropOffTime.Text),
                AttendanceStatus = cmbAttendanceStatus.SelectedIndex == 0,
                Notes = txtNotes.Text.Trim()
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Attendance record created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create attendance record.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _editingItem.StudentID = int.Parse(txtStudentID.Text.Trim());
            _editingItem.TripID = int.Parse(txtTripID.Text.Trim());
            _editingItem.BoardingStationID = ParseNullableInt(txtBoardingStationID.Text);
            _editingItem.DropOffStationID = ParseNullableInt(txtDropOffStationID.Text);
            _editingItem.BoardingTime = ParseTimeSpan(txtBoardingTime.Text);
            _editingItem.DropOffTime = ParseTimeSpan(txtDropOffTime.Text);
            _editingItem.AttendanceStatus = cmbAttendanceStatus.SelectedIndex == 0;
            _editingItem.Notes = txtNotes.Text.Trim();

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Attendance record updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update attendance record.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private int? ParseNullableInt(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        if (int.TryParse(input, out var result)) return result;
        return null;
    }

    private TimeSpan? ParseTimeSpan(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        if (TimeSpan.TryParse(input, out var result)) return result;
        return null;
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtStudentID.Text, out _))
        {
            MessageBox.Show("Valid StudentID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtTripID.Text, out _))
        {
            MessageBox.Show("Valid TripID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbAttendanceStatus.SelectedIndex < 0)
        {
            MessageBox.Show("AttendanceStatus is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
