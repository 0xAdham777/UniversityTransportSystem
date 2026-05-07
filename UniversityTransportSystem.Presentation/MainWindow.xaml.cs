using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using UniversityTransportSystem.Presentation.Views.People;
using UniversityTransportSystem.Presentation.Views.Academic;
using UniversityTransportSystem.Presentation.Views.Geography;
using UniversityTransportSystem.Presentation.Views.Transport;
using UniversityTransportSystem.Presentation.Views.Operations;

namespace UniversityTransportSystem.Presentation;

public partial class MainWindow : Window
{
    public static MainWindow? CurrentInstance { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
        CurrentInstance = this;
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var storyboard = (Storyboard)FindResource("WindowEnterStoryboard");
        if (storyboard != null)
            BeginStoryboard(storyboard);

        NavigateToPage(new PersonListView());
        PageTitle.Text = "Person Management";
    }

    public void NavigateToPage(UIElement page)
    {
        if (page is not FrameworkElement fe) return;

        if (page is System.Windows.Controls.Page p)
        {
            MainFrame.Navigate(p);
            return;
        }

        var host = new System.Windows.Controls.Page
        {
            Content = fe,
            Background = null
        };
        host.Loaded += (s, e) =>
        {
            var slide = (Storyboard)FindResource("PageSlideStoryboard");
            if (slide != null)
            {
                var clone = slide.Clone();
                clone.Begin(host);
            }
        };
        MainFrame.Navigate(host);
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
        if (e.Content is System.Windows.Controls.Page page)
        {
            page.Loaded += (s, _) =>
            {
                var slide = (Storyboard)FindResource("PageSlideStoryboard");
                if (slide != null)
                {
                    var clone = slide.Clone();
                    clone.Begin(page);
                }
            };
        }
    }

    private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
    }

    private void BtnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void SetPageTitle(string title)
    {
        PageTitle.Text = title;
    }

    // PEOPLE
    private void BtnPerson_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new PersonListView());
        SetPageTitle("Person Management");
    }

    private void BtnStudent_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new StudentListView());
        SetPageTitle("Student Management");
    }

    private void BtnEmployee_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new EmployeeListView());
        SetPageTitle("Employee Management");
    }

    private void BtnDriver_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new DriverListView());
        SetPageTitle("Driver Management");
    }

    // ACADEMIC
    private void BtnDepartment_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new DepartmentListView());
        SetPageTitle("Department Management");
    }

    private void BtnSpeciality_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new SpecialityListView());
        SetPageTitle("Speciality Management");
    }

    // GEOGRAPHY
    private void BtnWilaya_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new WilayaListView());
        SetPageTitle("Wilaya Management");
    }

    private void BtnMunicipality_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new MunicipalityListView());
        SetPageTitle("Municipality Management");
    }

    private void BtnStation_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new StationListView());
        SetPageTitle("Station Management");
    }

    // TRANSPORT
    private void BtnBusModel_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new BusModelListView());
        SetPageTitle("Bus Model Management");
    }

    private void BtnBus_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new BusListView());
        SetPageTitle("Bus Management");
    }

    private void BtnTransportLine_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new TransportLineListView());
        SetPageTitle("Transport Line Management");
    }

    private void BtnLineStation_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new LineStationListView());
        SetPageTitle("Line Station Management");
    }

    private void BtnSchedule_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new ScheduleListView());
        SetPageTitle("Schedule Management");
    }

    // OPERATIONS
    private void BtnSubscription_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new SubscriptionListView());
        SetPageTitle("Subscription Management");
    }

    private void BtnPayment_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new PaymentListView());
        SetPageTitle("Payment Management");
    }

    private void BtnBusAssignment_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new BusAssignmentListView());
        SetPageTitle("Bus Assignment Management");
    }

    private void BtnTrip_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new TripListView());
        SetPageTitle("Trip Management");
    }

    private void BtnAttendance_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new AttendanceListView());
        SetPageTitle("Attendance Management");
    }

    private void BtnIncidentType_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new IncidentTypeListView());
        SetPageTitle("Incident Type Management");
    }

    private void BtnIncident_Click(object sender, RoutedEventArgs e)
    {
        NavigateToPage(new IncidentListView());
        SetPageTitle("Incident Management");
    }
}
