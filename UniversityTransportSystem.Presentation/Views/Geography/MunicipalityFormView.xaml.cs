using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Geography;

public partial class MunicipalityFormView : UserControl
{
    private BaseService<Municipality> _service;
    private Municipality? _editingItem;

    public MunicipalityFormView()
    {
        InitializeComponent();
        _service = new BaseService<Municipality>(new MunicipalityRepository());
        Loaded += OnLoaded;
    }

    public void SetMunicipality(Municipality item)
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
        WilayaIdTextBox.Text = _editingItem.WilayaID.ToString();
        MunicipalityNameTextBox.Text = _editingItem.MunicipalityName;
        PostalCodeTextBox.Text = _editingItem.PostalCode;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(WilayaIdTextBox.Text, out var wilayaId))
        {
            MessageBox.Show("Valid Wilaya ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(MunicipalityNameTextBox.Text))
        {
            MessageBox.Show("Municipality Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Municipality { WilayaID = wilayaId, MunicipalityName = MunicipalityNameTextBox.Text.Trim(), PostalCode = PostalCodeTextBox.Text?.Trim() };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Municipality created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create municipality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.WilayaID = wilayaId;
                _editingItem.MunicipalityName = MunicipalityNameTextBox.Text.Trim();
                _editingItem.PostalCode = PostalCodeTextBox.Text?.Trim();
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Municipality updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update municipality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new MunicipalityListView());
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        WilayaIdTextBox.Clear();
        MunicipalityNameTextBox.Clear();
        PostalCodeTextBox.Clear();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new MunicipalityListView());
    }
}
