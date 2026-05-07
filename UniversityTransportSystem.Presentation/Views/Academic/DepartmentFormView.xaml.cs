using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Academic;

public partial class DepartmentFormView : UserControl
{
    private BaseService<Department> _service;
    private Department? _editingItem;

    public DepartmentFormView()
    {
        InitializeComponent();
        _service = new BaseService<Department>(new DepartmentRepository());
        Loaded += OnLoaded;
    }

    public void SetDepartment(Department item)
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
        DepartmentNameTextBox.Text = _editingItem.DepartmentName;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DepartmentNameTextBox.Text))
        {
            MessageBox.Show("Department Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Department { DepartmentName = DepartmentNameTextBox.Text.Trim() };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Department created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create department.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.DepartmentName = DepartmentNameTextBox.Text.Trim();
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Department updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update department.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DepartmentListView());
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        DepartmentNameTextBox.Clear();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new DepartmentListView());
    }
}
