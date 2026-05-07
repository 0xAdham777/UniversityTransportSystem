using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Geography;

public partial class WilayaListView : UserControl
{
    private BaseService<Wilaya> _service;
    private List<Wilaya> _allItems;

    public WilayaListView()
    {
        InitializeComponent();
        _service = new BaseService<Wilaya>(new WilayaRepository());
        _allItems = new List<Wilaya>();
        Loaded += OnLoaded;
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

        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            _allItems = await _service.GetAllAsync();
            WilayaDataGrid.ItemsSource = _allItems;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"DB Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = txtSearch.Text?.ToLower() ?? "";
        if (string.IsNullOrWhiteSpace(filter))
            WilayaDataGrid.ItemsSource = _allItems;
        else
            WilayaDataGrid.ItemsSource = _allItems.Where(x => x.WilayaName?.ToLower().Contains(filter) ?? false).ToList();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new WilayaFormView());
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (WilayaDataGrid.SelectedItem is Wilaya item)
        {
            var form = new WilayaFormView();
            form.SetWilaya(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a wilaya to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (WilayaDataGrid.SelectedItem is not Wilaya item)
        {
            MessageBox.Show("Please select a wilaya to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete {item.WilayaName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.WilayaID);
                if (success) LoadData();
                else MessageBox.Show("Failed to delete wilaya.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting wilaya: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
