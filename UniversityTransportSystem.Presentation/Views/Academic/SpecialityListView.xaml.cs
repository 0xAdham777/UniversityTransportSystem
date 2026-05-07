using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Academic;

public partial class SpecialityListView : UserControl
{
    private BaseService<Speciality> _service;
    private List<Speciality> _allItems;

    public SpecialityListView()
    {
        InitializeComponent();
        _service = new BaseService<Speciality>(new SpecialityRepository());
        _allItems = new List<Speciality>();
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
            SpecialityDataGrid.ItemsSource = _allItems;
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
            SpecialityDataGrid.ItemsSource = _allItems;
        else
            SpecialityDataGrid.ItemsSource = _allItems.Where(x => x.SpecialityName?.ToLower().Contains(filter) ?? false).ToList();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new SpecialityFormView());
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (SpecialityDataGrid.SelectedItem is Speciality item)
        {
            var form = new SpecialityFormView();
            form.SetSpeciality(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a speciality to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (SpecialityDataGrid.SelectedItem is not Speciality item)
        {
            MessageBox.Show("Please select a speciality to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete {item.SpecialityName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.SpecialityID);
                if (success) LoadData();
                else MessageBox.Show("Failed to delete speciality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting speciality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
