using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UniversityTransportSystem.Business.Models;
using UniversityTransportSystem.Business.Services;
using UniversityTransportSystem.DataAccess.Repositories;

namespace UniversityTransportSystem.Presentation.Views.People;

public partial class StudentFormView : UserControl
{
    private BaseService<Student> _studentService;
    private Student? _editingStudent;

    public StudentFormView()
    {
        InitializeComponent();
        _studentService = new BaseService<Student>(new StudentRepository());
    }

    public void SetStudent(Student student)
    {
        _editingStudent = student;
        PopulateFields();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)Resources["PageEnter"];
        storyboard?.Begin(this);
    }

    private void PopulateFields()
    {
        if (_editingStudent == null) return;

        txtPersonID.Text = _editingStudent.PersonID.ToString();
        txtSpecialityID.Text = _editingStudent.SpecialityID.ToString();
        cmbStudentStatus.SelectedIndex = _editingStudent.StudentStatus ? 0 : 1;

        btnSave.Visibility = Visibility.Collapsed;
        btnUpdate.Visibility = Visibility.Visible;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var isActive = cmbStudentStatus.SelectedIndex == 0;
        txtBadgeStatus.Text = isActive ? "Active" : "Inactive";
        txtPreviewStatus.Text = isActive ? "Active Student" : "Inactive Student";
        badgeStatus.Background = new SolidColorBrush(isActive ? Color.FromRgb(0x1D, 0xE5, 0xA8) : Color.FromRgb(0xE5, 0x4D, 0x6A));
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new StudentListView());
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var student = new Student
            {
                PersonID = int.Parse(txtPersonID.Text.Trim()),
                SpecialityID = int.Parse(txtSpecialityID.Text.Trim()),
                StudentStatus = cmbStudentStatus.SelectedIndex == 0
            };

            var id = await _studentService.InsertAsync(student);
            if (id > 0)
            {
                MessageBox.Show("Student created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new StudentListView());
            }
            else
            {
                MessageBox.Show("Failed to create student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_editingStudent == null) return;
        if (!ValidateInput()) return;

        try
        {
            _editingStudent.PersonID = int.Parse(txtPersonID.Text.Trim());
            _editingStudent.SpecialityID = int.Parse(txtSpecialityID.Text.Trim());
            _editingStudent.StudentStatus = cmbStudentStatus.SelectedIndex == 0;

            var success = await _studentService.UpdateAsync(_editingStudent);
            if (success)
            {
                MessageBox.Show("Student updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.CurrentInstance?.NavigateToPage(new StudentListView());
            }
            else
            {
                MessageBox.Show("Failed to update student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput()
    {
        if (!int.TryParse(txtPersonID.Text, out _))
        {
            MessageBox.Show("Valid Person ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(txtSpecialityID.Text, out _))
        {
            MessageBox.Show("Valid Speciality ID is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (cmbStudentStatus.SelectedIndex < 0)
        {
            MessageBox.Show("Student Status is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        return true;
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        txtPersonID.Clear();
        txtSpecialityID.Clear();
        cmbStudentStatus.SelectedIndex = -1;
        _editingStudent = null;
        btnSave.Visibility = Visibility.Visible;
        btnUpdate.Visibility = Visibility.Collapsed;
        UpdatePreview();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.CurrentInstance?.NavigateToPage(new StudentListView());
    }
}
