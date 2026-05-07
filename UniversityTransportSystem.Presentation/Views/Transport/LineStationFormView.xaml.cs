using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class LineStationFormView : UserControl
{
    private BaseService<LineStation> _service;
    private LineStation? _editingItem;

    public LineStationFormView()
    {
        InitializeComponent();
        _service = new BaseService<LineStation>(new LineStationRepository());
        Loaded += OnLoaded;
    }

    public void SetLineStation(LineStation item)
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
        txtStationID.Text = _editingItem.StationID.ToString();
        txtStationOrder.Text = _editingItem.StationOrder.ToString();
        txtDistanceFromOrigin.Text = _editingItem.DistanceFromOrigin?.ToString();

        btnSave.Content = "Update";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(txtTransportLineID.Text, out var lineId))
        {
            MessageBox.Show("Valid TransportLineID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(txtStationID.Text, out var stationId))
        {
            MessageBox.Show("Valid StationID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(txtStationOrder.Text, out var order))
        {
            MessageBox.Show("Valid Station Order is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new LineStation
                {
                    TransportLineID = lineId,
                    StationID = stationId,
                    StationOrder = order,
                    DistanceFromOrigin = decimal.TryParse(txtDistanceFromOrigin.Text, out var dist) ? dist : null
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Line station created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create line station.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.TransportLineID = lineId;
                _editingItem.StationID = stationId;
                _editingItem.StationOrder = order;
                _editingItem.DistanceFromOrigin = decimal.TryParse(txtDistanceFromOrigin.Text, out var dist) ? dist : null;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Line station updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update line station.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new LineStationListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Line Station Management";
        }
    }
}
