using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class IncidentFormView : UserControl
{
    private BaseService<Incident> _service;
    private Incident? _editingItem;

    public IncidentFormView()
    {
        InitializeComponent();
        _service = new BaseService<Incident>(new IncidentRepository());
        Loaded += OnLoaded;
    }

    public void SetIncident(Incident item)
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

        txtTripID.Text = _editingItem.TripID.ToString();
        txtReportedByEmployeeID.Text = _editingItem.ReportedByEmployeeID.ToString();
        txtIncidentTypeID.Text = _editingItem.IncidentTypeID.ToString();
        txtIncidentDescription.Text = _editingItem.IncidentDescription ?? "";
        dpIncidentDateTime.SelectedDate = _editingItem.IncidentDateTime;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new IncidentListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Incident Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new Incident
            {
                TripID = int.Parse(txtTripID.Text.Trim()),
                ReportedByEmployeeID = int.Parse(txtReportedByEmployeeID.Text.Trim()),
                IncidentTypeID = int.Parse(txtIncidentTypeID.Text.Trim()),
                IncidentDescription = txtIncidentDescription.Text.Trim(),
                IncidentDateTime = dpIncidentDateTime.SelectedDate ?? DateTime.Now
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Incident created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create incident.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _editingItem.TripID = int.Parse(txtTripID.Text.Trim());
            _editingItem.ReportedByEmployeeID = int.Parse(txtReportedByEmployeeID.Text.Trim());
            _editingItem.IncidentTypeID = int.Parse(txtIncidentTypeID.Text.Trim());
            _editingItem.IncidentDescription = txtIncidentDescription.Text.Trim();
            _editingItem.IncidentDateTime = dpIncidentDateTime.SelectedDate ?? DateTime.Now;

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Incident updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update incident.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtTripID.Text, out _))
        {
            MessageBox.Show("Valid TripID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtReportedByEmployeeID.Text, out _))
        {
            MessageBox.Show("Valid ReportedByEmployeeID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtIncidentTypeID.Text, out _))
        {
            MessageBox.Show("Valid IncidentTypeID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpIncidentDateTime.SelectedDate == null)
        {
            MessageBox.Show("IncidentDateTime is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
