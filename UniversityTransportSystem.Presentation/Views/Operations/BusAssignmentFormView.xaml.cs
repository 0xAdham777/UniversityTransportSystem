using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class BusAssignmentFormView : UserControl
{
    private BaseService<BusAssignment> _service;
    private BusAssignment? _editingItem;

    public BusAssignmentFormView()
    {
        InitializeComponent();
        _service = new BaseService<BusAssignment>(new BusAssignmentRepository());
        Loaded += OnLoaded;
    }

    public void SetAssignment(BusAssignment item)
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
        txtTransportLineID.Text = _editingItem.TransportLineID.ToString();
        dpStartDate.SelectedDate = _editingItem.StartDate;
        dpEndDate.SelectedDate = _editingItem.EndDate;
        cmbStatus.SelectedIndex = _editingItem.AssignmentStatus ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new BusAssignmentListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Bus Assignment Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new BusAssignment
            {
                BusID = int.Parse(txtBusID.Text.Trim()),
                TransportLineID = int.Parse(txtTransportLineID.Text.Trim()),
                StartDate = dpStartDate.SelectedDate ?? DateTime.Now,
                EndDate = dpEndDate.SelectedDate,
                AssignmentStatus = cmbStatus.SelectedIndex == 0
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Bus assignment created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create bus assignment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _editingItem.TransportLineID = int.Parse(txtTransportLineID.Text.Trim());
            _editingItem.StartDate = dpStartDate.SelectedDate ?? DateTime.Now;
            _editingItem.EndDate = dpEndDate.SelectedDate;
            _editingItem.AssignmentStatus = cmbStatus.SelectedIndex == 0;

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Bus assignment updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update bus assignment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtBusID.Text, out _))
        {
            MessageBox.Show("Valid BusID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtTransportLineID.Text, out _))
        {
            MessageBox.Show("Valid TransportLineID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpStartDate.SelectedDate == null)
        {
            MessageBox.Show("StartDate is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbStatus.SelectedIndex < 0)
        {
            MessageBox.Show("AssignmentStatus is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
