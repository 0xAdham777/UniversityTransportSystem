using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class IncidentListView : UserControl
{
    private BaseService<Incident> _service;
    private List<Incident> _allItems;

    public IncidentListView()
    {
        InitializeComponent();
        _service = new BaseService<Incident>(new IncidentRepository());
        _allItems = new List<Incident>();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var storyboard = TryFindResource("PageEnterStoryboard") as Storyboard;
        storyboard?.Begin(this);
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            _allItems = await _service.GetAllAsync();
            dgItems.ItemsSource = _allItems;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = txtSearch.Text?.ToLower() ?? "";
        if (string.IsNullOrWhiteSpace(filter))
            dgItems.ItemsSource = _allItems;
        else
            dgItems.ItemsSource = _allItems.Where(s =>
                s.IncidentID.ToString().Contains(filter) ||
                s.TripID.ToString().Contains(filter)).ToList();
    }

    private void DgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new IncidentFormView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Add Incident";
        }
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (dgItems.SelectedItem is Incident item)
        {
            var form = new IncidentFormView();
            form.SetIncident(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
            if (MainWindow.CurrentInstance != null)
            {
                var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
                if (title != null) title.Text = "Edit Incident";
            }
        }
        else
        {
            MessageBox.Show("Please select an incident to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (dgItems.SelectedItem is not Incident item)
        {
            MessageBox.Show("Please select an incident to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete Incident #{item.IncidentID}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.IncidentID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete incident.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting incident: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
