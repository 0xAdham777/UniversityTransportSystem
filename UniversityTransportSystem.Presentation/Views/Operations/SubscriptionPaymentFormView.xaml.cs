using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class SubscriptionPaymentFormView : UserControl
{
    private BaseService<SubscriptionPayment> _service;
    private SubscriptionPayment? _editingItem;

    public SubscriptionPaymentFormView()
    {
        InitializeComponent();
        _service = new BaseService<SubscriptionPayment>(new SubscriptionPaymentRepository());
        Loaded += OnLoaded;
    }

    public void SetPayment(SubscriptionPayment item)
    {
        _editingItem = item;
        PopulateFields();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var storyboard = TryFindResource("PageEnterStoryboard") as Storyboard;
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingItem == null) return;

        txtTransportSubscriptionID.Text = _editingItem.TransportSubscriptionID.ToString();
        txtAmount.Text = _editingItem.Amount.ToString("F2");
        dpPaymentDate.SelectedDate = _editingItem.PaymentDate;
        cmbPaymentStatus.SelectedIndex = _editingItem.PaymentStatus == true ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new PaymentListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Subscription Payment Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new SubscriptionPayment
            {
                TransportSubscriptionID = int.Parse(txtTransportSubscriptionID.Text.Trim()),
                Amount = decimal.Parse(txtAmount.Text.Trim()),
                PaymentDate = dpPaymentDate.SelectedDate ?? DateTime.Now,
                PaymentStatus = cmbPaymentStatus.SelectedIndex == 0
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Payment created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create payment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingItem == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingItem.TransportSubscriptionID = int.Parse(txtTransportSubscriptionID.Text.Trim());
            _editingItem.Amount = decimal.Parse(txtAmount.Text.Trim());
            _editingItem.PaymentDate = dpPaymentDate.SelectedDate ?? DateTime.Now;
            _editingItem.PaymentStatus = cmbPaymentStatus.SelectedIndex == 0;

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Payment updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update payment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtTransportSubscriptionID.Text, out _))
        {
            MessageBox.Show("Valid TransportSubscriptionID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!decimal.TryParse(txtAmount.Text, out _))
        {
            MessageBox.Show("Valid Amount is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpPaymentDate.SelectedDate == null)
        {
            MessageBox.Show("PaymentDate is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbPaymentStatus.SelectedIndex < 0)
        {
            MessageBox.Show("PaymentStatus is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
