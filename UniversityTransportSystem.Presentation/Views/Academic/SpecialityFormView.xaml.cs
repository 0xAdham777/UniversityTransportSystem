using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Academic;

public partial class SpecialityFormView : UserControl
{
    private BaseService<Speciality> _service;
    private Speciality? _editingItem;

    public SpecialityFormView()
    {
        InitializeComponent();
        _service = new BaseService<Speciality>(new SpecialityRepository());
        Loaded += OnLoaded;
    }

    public void SetSpeciality(Speciality item)
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
        DepartmentIdTextBox.Text = _editingItem.DepartmentID.ToString();
        SpecialityNameTextBox.Text = _editingItem.SpecialityName;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(DepartmentIdTextBox.Text, out var deptId))
        {
            MessageBox.Show("Valid Department ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(SpecialityNameTextBox.Text))
        {
            MessageBox.Show("Speciality Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            if (_editingItem == null)
            {
                var item = new Speciality { DepartmentID = deptId, SpecialityName = SpecialityNameTextBox.Text.Trim() };
                var id = await _service.InsertAsync(item);
                if (id > 0)
                {
                    MessageBox.Show("Speciality created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to create speciality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _editingItem.DepartmentID = deptId;
                _editingItem.SpecialityName = SpecialityNameTextBox.Text.Trim();
                var success = await _service.UpdateAsync(_editingItem);
                if (success)
                {
                    MessageBox.Show("Speciality updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    BackButton_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to update speciality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new SpecialityListView());
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        DepartmentIdTextBox.Clear();
        SpecialityNameTextBox.Clear();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new SpecialityListView());
    }
}
