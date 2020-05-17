using BE;
using com.sun.jdi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ExtraFieldsFall.xaml
    /// </summary>
    public partial class ExtraFieldsFall : UserControl
    {
        public string fallLocation { get; set; }
        public string CalculatedLocation { get; set; }
        public int ReportsNumber { get; set; }
        public FallReportVM CurrentFallReportVM { get; set; }
        public AssessmentVM CurrentFallPredictionVM { get; set; }
        public ExtraFieldsFall()
        {
            InitializeComponent();
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new AssessmentVM();
            DataContext = this;
            fallLocation = "";
            CalculatedLocation = "";
            ReportsNumber = 0;
        }
        public ExtraFieldsFall(Fall CurrentFall)
        {
            InitializeComponent();
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new AssessmentVM();
            Location_ Assessmentlocation = GetAssessmentLocationOfFall(CurrentFall);
            DataContext = this;
            fallLocation = CurrentFall.location.latitude.ToString() + " " + "," + " " + CurrentFall.location.longitude.ToString();
            CalculatedLocation = Assessmentlocation.latitude.ToString() + " " + "," + " " + Assessmentlocation.longitude.ToString();
            ReportsNumber = CalculateReportNumber(CurrentFall);
        }
        private int CalculateReportNumber(Fall CurrentFall)
        {
            List<Assessment> assessments = CurrentFallPredictionVM.Assessments.ToList();
            Assessment currentAssessment = (from a in assessments
                                            from f in a.falls
                                            where f.id == CurrentFall.id
                                            select a).ToList()[0];//if null test


            return (currentAssessment.reports.Count());

        }
        private Location_ GetAssessmentLocationOfFall(Fall CurrentFall)
        {
            List<Assessment> assessments = CurrentFallPredictionVM.Assessments.ToList();
            // List<Report> report = CurrentFallReportVM.Reports.ToList();
            List<Assessment> currentAssessment = (from a in assessments
                                                  from f in a.falls
                                                  where f.id == CurrentFall.id
                                                  select a).ToList();
            if (currentAssessment.Count == 0)
            {
                return null;
            }

            GeoCoordinate location2 = new GeoCoordinate(CurrentFall.location.latitude, CurrentFall.location.longitude);
            double min = double.MaxValue;
            Location_ returnLocation = new Location_();
            
            foreach (Location_ location1 in currentAssessment[0].locations)//getalllocation
            {
                double cur = new GeoCoordinate(location1.latitude, location1.longitude).GetDistanceTo(location2);
                if (cur < min)
                {
                    min = cur;
                    returnLocation = location1;
                }
            }
            return returnLocation;

        }
        public void Update(Fall CurrentFall)
        {
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new AssessmentVM();
            Location_ assessmentLocation = GetAssessmentLocationOfFall(CurrentFall);
            DataContext = this;
            if (fallLocation != null)
                fallLocation = CurrentFall.location.latitude.ToString() + " " + "," + " " + CurrentFall.location.longitude.ToString();
            if (CalculatedLocation != "")
                CalculatedLocation = assessmentLocation.latitude.ToString() + " " + "," + " " + assessmentLocation.longitude.ToString();
            ReportsNumber = CalculateReportNumber(CurrentFall);

        }



    }
}
