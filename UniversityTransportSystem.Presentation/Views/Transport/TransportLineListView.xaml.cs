using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Transport;

public partial class TransportLineListView : UserControl
{
    private BaseService<TransportLine> _service;
    private List<TransportLine> _allItems;

    public TransportLineListView()
    {
        InitializeComponent();
        _service = new BaseService<TransportLine>(new TransportLineRepository());
        _allItems = new List<TransportLine>();
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
            dgTransportLines.ItemsSource = _allItems;
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
            dgTransportLines.ItemsSource = _allItems;
        else
            dgTransportLines.ItemsSource = _allItems.Where(x =>
                (x.LineName?.ToLower().Contains(filter) ?? false)).ToList();
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new TransportLineFormView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Add Transport Line";
        }
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (dgTransportLines.SelectedItem is TransportLine item)
        {
            var form = new TransportLineFormView();
            form.SetTransportLine(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
            if (MainWindow.CurrentInstance != null)
            {
                var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
                if (title != null) title.Text = "Edit Transport Line";
            }
        }
        else
        {
            MessageBox.Show("Please select a transport line to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (dgTransportLines.SelectedItem is not TransportLine item)
        {
            MessageBox.Show("Please select a transport line to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete {item.LineName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.TransportLineID);
                if (success) LoadData();
                else MessageBox.Show("Failed to delete transport line.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting transport line: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
