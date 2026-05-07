using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class BusModelFormView : UserControl
{
    private BaseService<BusModel> _service;
    private BusModel? _editingItem;

    public BusModelFormView()
    {
        InitializeComponent();
        _service = new BaseService<BusModel>(new BusModelRepository());
        Loaded += OnLoaded;
    }

    public void SetBusModel(BusModel item)
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

        txtModelName.Text = _editingItem.ModelName;
        txtManufacturerName.Text = _editingItem.ManufacturerName;
        txtDefaultCapacity.Text = _editingItem.DefaultCapacity.ToString();

        btnSave.Content = "Update";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtModelName.Text))
        {
            MessageBox.Show("Model Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtManufacturerName.Text))
        {
            MessageBox.Show("Manufacturer Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(txtDefaultCapacity.Text, out var capacity))
        {
            MessageBox.Show("Valid Default Capacity is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new BusModel
                {
                    ModelName = txtModelName.Text.Trim(),
                    ManufacturerName = txtManufacturerName.Text.Trim(),
                    DefaultCapacity = capacity
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Bus model created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create bus model.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.ModelName = txtModelName.Text.Trim();
                _editingItem.ManufacturerName = txtManufacturerName.Text.Trim();
                _editingItem.DefaultCapacity = capacity;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Bus model updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BtnCancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update bus model.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new BusModelListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Bus Model Management";
        }
    }
}
