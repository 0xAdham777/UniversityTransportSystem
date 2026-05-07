using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Geography;

public partial class WilayaFormView : UserControl
{
    private BaseService<Wilaya> _service;
    private Wilaya? _editingItem;

    public WilayaFormView()
    {
        InitializeComponent();
        _service = new BaseService<Wilaya>(new WilayaRepository());
        Loaded += OnLoaded;
    }

    public void SetWilaya(Wilaya item)
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
        WilayaNameTextBox.Text = _editingItem.WilayaName;
        WilayaCodeTextBox.Text = _editingItem.WilayaCode;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(WilayaNameTextBox.Text))
        {
            MessageBox.Show("Wilaya Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(WilayaCodeTextBox.Text))
        {
            MessageBox.Show("Wilaya Code is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Wilaya { WilayaName = WilayaNameTextBox.Text.Trim(), WilayaCode = WilayaCodeTextBox.Text.Trim() };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Wilaya created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create wilaya.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.WilayaName = WilayaNameTextBox.Text.Trim();
                _editingItem.WilayaCode = WilayaCodeTextBox.Text.Trim();
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Wilaya updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update wilaya.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new WilayaListView());
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        WilayaNameTextBox.Clear();
        WilayaCodeTextBox.Clear();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new WilayaListView());
    }
}
