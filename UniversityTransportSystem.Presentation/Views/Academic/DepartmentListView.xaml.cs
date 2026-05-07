using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Academic;

public partial class DepartmentListView : UserControl
{
    private BaseService<Department> _service;
    private List<Department> _allItems;

    public DepartmentListView()
    {
        InitializeComponent();
        _service = new BaseService<Department>(new DepartmentRepository());
        _allItems = new List<Department>();
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
            DepartmentDataGrid.ItemsSource = _allItems;
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
            DepartmentDataGrid.ItemsSource = _allItems;
        else
            DepartmentDataGrid.ItemsSource = _allItems.Where(x => x.DepartmentName?.ToLower().Contains(filter) ?? false).ToList();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DepartmentFormView());
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (DepartmentDataGrid.SelectedItem is Department item)
        {
            var form = new DepartmentFormView();
            form.SetDepartment(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a department to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (DepartmentDataGrid.SelectedItem is not Department item)
        {
            MessageBox.Show("Please select a department to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete {item.DepartmentName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.DepartmentID);
                if (success) LoadData();
                else MessageBox.Show("Failed to delete department.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
