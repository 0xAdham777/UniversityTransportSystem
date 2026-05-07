using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.Operations;

public partial class TransportSubscriptionFormView : UserControl
{
    private BaseService<TransportSubscription> _service;
    private TransportSubscription? _editingItem;

    public TransportSubscriptionFormView()
    {
        InitializeComponent();
        _service = new BaseService<TransportSubscription>(new TransportSubscriptionRepository());
        Loaded += OnLoaded;
    }

    public void SetSubscription(TransportSubscription item)
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

        txtStudentID.Text = _editingItem.StudentID.ToString();
        txtTransportLineID.Text = _editingItem.TransportLineID.ToString();
        dpStartDate.SelectedDate = _editingItem.StartDate;
        dpEndDate.SelectedDate = _editingItem.EndDate;
        cmbStatus.SelectedIndex = _editingItem.SubscriptionStatus ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new SubscriptionListView());
        if (MainWindow.CurrentInstance != null)
        {
            var title = MainWindow.CurrentInstance.FindName("PageTitle") as TextBlock;
            if (title != null) title.Text = "Transport Subscription Management";
        }
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var item = new TransportSubscription
            {
                StudentID = int.Parse(txtStudentID.Text.Trim()),
                TransportLineID = int.Parse(txtTransportLineID.Text.Trim()),
                StartDate = dpStartDate.SelectedDate ?? DateTime.Now,
                EndDate = dpEndDate.SelectedDate,
                SubscriptionStatus = cmbStatus.SelectedIndex == 0
            };

            var id = await _service.InsertAsync(item);
            if (id > 0)
            {
                MessageBox.Show("Transport subscription created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to create transport subscription.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _editingItem.StudentID = int.Parse(txtStudentID.Text.Trim());
            _editingItem.TransportLineID = int.Parse(txtTransportLineID.Text.Trim());
            _editingItem.StartDate = dpStartDate.SelectedDate ?? DateTime.Now;
            _editingItem.EndDate = dpEndDate.SelectedDate;
            _editingItem.SubscriptionStatus = cmbStatus.SelectedIndex == 0;

            var success = await _service.UpdateAsync(_editingItem);
            if (success)
            {
                MessageBox.Show("Transport subscription updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnBack_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to update transport subscription.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtStudentID.Text, out _))
        {
            MessageBox.Show("Valid StudentID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtTransportLineID.Text, out _))
        {
            MessageBox.Show("Valid TransportLineID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (dpStartDate.SelectedDate == null)
        {
            MessageBox.Show("StartDate is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbStatus.SelectedIndex < 0)
        {
            MessageBox.Show("SubscriptionStatus is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        BtnBack_Click(sender, e);
    }
}
