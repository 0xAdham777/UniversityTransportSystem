using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class BusFormView : UserControl
{
    private BaseService<Bus> _service;
    private Bus? _editingItem;

    public BusFormView()
    {
        InitializeComponent();
        _service = new BaseService<Bus>(new BusRepository());
        Loaded += OnLoaded;
    }

    public void SetBus(Bus item)
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

        txtBusModelID.Text = _editingItem.BusModelID.ToString();
        txtPlateNumber.Text = _editingItem.PlateNumber;
        txtBusCode.Text = _editingItem.BusCode;
        txtManufacturingYear.Text = _editingItem.ManufacturingYear?.ToString();
        cmbBusStatus.SelectedIndex = _editingItem.BusStatus ? 0 : 1;

        btnSave.Content = "Update";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(txtBusModelID.Text, out var modelId))
        {
            MessageBox.Show("Valid BusModelID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPlateNumber.Text))
        {
            MessageBox.Show("Plate Number is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Bus
                {
                    BusModelID = modelId,
                    PlateNumber = txtPlateNumber.Text.Trim(),
                    BusCode = txtBusCode.Text?.Trim(),
                    ManufacturingYear = int.TryParse(txtManufacturingYear.Text, out var year) ? year : null,
                    BusStatus = cmbBusStatus.SelectedIndex == 0
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Bus created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create bus.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.BusModelID = modelId;
                _editingItem.PlateNumber = txtPlateNumber.Text.Trim();
                _editingItem.BusCode = txtBusCode.Text?.Trim();
                _editingItem.ManufacturingYear = int.TryParse(txtManufacturingYear.Text, out var year) ? year : null;
                _editingItem.BusStatus = cmbBusStatus.SelectedIndex == 0;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Bus updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update bus.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new BusListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Bus Management";
        }
    }
}
