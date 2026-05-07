using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class PaymentListView : UserControl
{
    private BaseService<SubscriptionPayment> _service;
    private List<SubscriptionPayment> _allItems;

    public PaymentListView()
    {
        InitializeComponent();
        _service = new BaseService<SubscriptionPayment>(new SubscriptionPaymentRepository());
        _allItems = new List<SubscriptionPayment>();
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
                s.PaymentID.ToString().Contains(filter) ||
                s.TransportSubscriptionID.ToString().Contains(filter)).ToList();
    }

    private void DgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new SubscriptionPaymentFormView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Add Payment";
        }
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (dgItems.SelectedItem is SubscriptionPayment item)
        {
            var form = new SubscriptionPaymentFormView();
            form.SetPayment(item);
            MainWindow.CurrentInstance?.NavigateToPage(form);
            if (MainWindow.CurrentInstance != null)
            {
                var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
                if (title != null) title.Text = "Edit Payment";
            }
        }
        else
        {
            MessageBox.Show("Please select a payment to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (dgItems.SelectedItem is not SubscriptionPayment item)
        {
            MessageBox.Show("Please select a payment to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete Payment #{item.PaymentID}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _service.DeleteAsync(item.PaymentID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete payment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
