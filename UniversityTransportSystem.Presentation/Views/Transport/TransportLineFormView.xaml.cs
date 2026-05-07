using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class TransportLineFormView : UserControl
{
    private BaseService<TransportLine> _service;
    private TransportLine? _editingItem;

    public TransportLineFormView()
    {
        InitializeComponent();
        _service = new BaseService<TransportLine>(new TransportLineRepository());
        Loaded += OnLoaded;
    }

    public void SetTransportLine(TransportLine item)
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

        txtLineName.Text = _editingItem.LineName;
        txtOriginStationID.Text = _editingItem.OriginStationID.ToString();
        txtDestinationStationID.Text = _editingItem.DestinationStationID.ToString();
        cmbLineStatus.SelectedIndex = _editingItem.LineStatus ? 0 : 1;

        btnSave.Content = "Update";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtLineName.Text))
        {
            MessageBox.Show("Line Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(txtOriginStationID.Text, out var originId))
        {
            MessageBox.Show("Valid Origin Station ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(txtDestinationStationID.Text, out var destId))
        {
            MessageBox.Show("Valid Destination Station ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new TransportLine
                {
                    LineName = txtLineName.Text.Trim(),
                    OriginStationID = originId,
                    DestinationStationID = destId,
                    LineStatus = cmbLineStatus.SelectedIndex == 0
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Transport line created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create transport line.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.LineName = txtLineName.Text.Trim();
                _editingItem.OriginStationID = originId;
                _editingItem.DestinationStationID = destId;
                _editingItem.LineStatus = cmbLineStatus.SelectedIndex == 0;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Transport line updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update transport line.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new TransportLineListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Transport Line Management";
        }
    }
}
