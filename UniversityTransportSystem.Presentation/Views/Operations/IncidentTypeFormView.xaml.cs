using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class IncidentTypeFormView : UserControl
{
    private BaseService<IncidentType> _service;
    private IncidentType? _editingItem;

    public IncidentTypeFormView()
    {
        InitializeComponent();
        _service = new BaseService<IncidentType>(new IncidentTypeRepository());
        Loaded += OnLoaded;
    }

    public void SetIncidentType(IncidentType item)
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

        txtIncidentTypeName.Text = _editingItem.IncidentTypeName;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new IncidentTypeListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Incident Type Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new IncidentType
            {
                IncidentTypeName = txtIncidentTypeName.Text.Trim()
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Incident type created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create incident type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _editingItem.IncidentTypeName = txtIncidentTypeName.Text.Trim();

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Incident type updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update incident type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtIncidentTypeName.Text))
        {
            MessageBox.Show("IncidentTypeName is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
