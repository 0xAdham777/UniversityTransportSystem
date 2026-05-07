using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class StudentListView : UserControl
{
    private BaseService<Student> _studentService;
    private List<Student> _allStudents;

    public StudentListView()
    {
        InitializeComponent();
        _studentService = new BaseService<Student>(new StudentRepository());
        _allStudents = new List<Student>();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            _allStudents = await _studentService.GetAllAsync();
            dgStudents.ItemsSource = _allStudents;
            txtTotalStudents.Text = _allStudents.Count.ToString();
            txtActiveStudents.Text = _allStudents.Count(s => s.StudentStatus).ToString();
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
            dgStudents.ItemsSource = _allStudents;
        else
            dgStudents.ItemsSource = _allStudents.Where(s =>
                s.StudentID.ToString().Contains(filter)).ToList();
    }

    private void DgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new StudentFormView());
    }

    private void BtnEditStudent_Click(object sender, RoutedEventArgs e)
    {
        if (dgStudents.SelectedItem is Student student)
        {
            var form = new StudentFormView();
            form.SetStudent(student);
            MainWindow.CurrentInstance?.NavigateToPage(form);
        }
        else
        {
            MessageBox.Show("Please select a student to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void BtnDeleteStudent_Click(object sender, RoutedEventArgs e)
    {
        if (dgStudents.SelectedItem is not Student student)
        {
            MessageBox.Show("Please select a student to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to delete Student #{student.StudentID}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _studentService.DeleteAsync(student.StudentID);
                if (success)
                    LoadData();
                else
                    MessageBox.Show("Failed to delete student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}
