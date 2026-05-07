using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Geography;

public partial class StationFormView : UserControl
{
    private BaseService<Station> _service;
    private Station? _editingItem;

    public StationFormView()
    {
        InitializeComponent();
        _service = new BaseService<Station>(new StationRepository());
        Loaded += OnLoaded;
    }

    public void SetStation(Station item)
    {
        _editingItem = item;
        PopulateFields();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var storyboard = new Storyboard();
        var opacity = new DoubleAnimation { From = 0, To = 1, Duration = new Duration(TimeSpan.FromSeconds(0.3)) };
        Storyboard.SetTargetProperty(opacity, new PropertyPath("Opacity"));
        storyboard.Children.Add(opacity);
        var translate = new DoubleAnimation { From = 20, To = 0, Duration = new Duration(TimeSpan.FromSeconds(0.3)), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
        Storyboard.SetTargetProperty(translate, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
        storyboard.Children.Add(translate);
        storyboard.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingItem == null) return;
        StationNameTextBox.Text = _editingItem.StationName;
        LocationDescriptionTextBox.Text = _editingItem.LocationDescription;
        MunicipalityIdTextBox.Text = _editingItem.MunicipalityID.ToString();
        StationStatusComboBox.SelectedIndex = _editingItem.StationStatus ? 0 : 1;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(StationNameTextBox.Text))
        {
            MessageBox.Show("Station Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(MunicipalityIdTextBox.Text, out var munId))
        {
            MessageBox.Show("Valid Municipality ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Station
                {
                    StationName = StationNameTextBox.Text.Trim(),
                    LocationDescription = LocationDescriptionTextBox.Text?.Trim(),
                    MunicipalityID = munId,
                    StationStatus = StationStatusComboBox.SelectedIndex == 0
                };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Station created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create station.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.StationName = StationNameTextBox.Text.Trim();
                _editingItem.LocationDescription = LocationDescriptionTextBox.Text?.Trim();
                _editingItem.MunicipalityID = munId;
                _editingItem.StationStatus = StationStatusComboBox.SelectedIndex == 0;
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Station updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update station.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new StationListView());
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        StationNameTextBox.Clear();
        LocationDescriptionTextBox.Clear();
        MunicipalityIdTextBox.Clear();
        StationStatusComboBox.SelectedIndex = 0;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new StationListView());
    }
}
