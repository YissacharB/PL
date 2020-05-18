using BE;
using LiveCharts;
using LiveCharts.Wpf;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Interaction logic for GraphUC.xaml
    /// </summary>
    public partial class GraphUC : UserControl
    {
        public AssessmentVM CurrentFallPredictionVM { get; set; }
        public FallsVM CurrentFallVM { get; set; }

        List<Assessment> FallPredictionList;

        public GraphUC()
        {
            InitializeComponent();
            DataContext = this;
            CurrentFallPredictionVM = new AssessmentVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<Assessment>)(CurrentFallPredictionVM.Assessments)).ToList<Assessment>();
            InitializeGraph();

        }
        public void Load()
        {
            CurrentFallPredictionVM = new AssessmentVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<Assessment>)(CurrentFallPredictionVM.Assessments)).ToList<Assessment>();
            InitializeGraph();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        private double GetDinstance(Assessment l1, Fall l2)
        {

            List<Fall> fall = (from f in CurrentFallVM.CurrentModel.AllFalls()
                               where f.id == l2.id
                               select f).ToList();
            if (fall.Count == 0)
            {
                return -1;
            }
            else
            {
                double min = double.MaxValue;
                GeoCoordinate location2 = new GeoCoordinate(l2.location.latitude, l2.location.longitude);
                foreach (Location_ location1 in l1.Locations)
                {
                    double cur = new GeoCoordinate(location1.latitude, location1.longitude).GetDistanceTo(location2);
                    if (cur < min)
                    {
                        min = cur;
                    }
                }
                return min;
            }

        }
        private double SumErrors(List<Assessment> assessments)
        {
            Fall currentFall = new Fall();
            double currentDinstance = 0;

            if (FallPredictionList == null)
                return 1;
            foreach (Assessment item in assessments)
            {
                currentFall = CurrentFallVM.GetFall(item.id);
                if (currentFall == null)
                {
                    currentDinstance += 10;
                }
                else
                    currentDinstance += GetDinstance(item, currentFall);
            }
            return currentDinstance;
        }
        public void InitializeGraph()
        {
            Labels = new string[24];
            string currentValue = "";
            for (int i = 0; i < 24; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString() + ":00";
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<Assessment> predictionList = new List<Assessment>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();



            for (int i = 0; i < 24; i++)
            {
                predictionList.Clear();
                foreach (Assessment item in FallPredictionList)
                {
                    if ((item.start.Day == DateTime.Today.Day) && (item.start.Hour == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
        }
        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            Labels = new string[24];
            string currentValue = "";
            for (int i = 0; i < 24; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString() + ":00";
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<Assessment> predictionList = new List<Assessment>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();



            for (int i = 0; i < 24; i++)
            {
                predictionList.Clear();
                foreach (Assessment item in FallPredictionList)
                {
                    if ((item.start.Day == DateTime.Today.Day) && (item.start.Hour == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void ThisMounthButton_Click(object sender, RoutedEventArgs e)
        {
            int days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            Labels = new string[days];
            string currentValue = "";
            for (int i = 0; i < days; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString();
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<Assessment> predictionList = new List<Assessment>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = 0; i < days; i++)
            {
                predictionList.Clear();
                foreach (Assessment item in FallPredictionList)
                {
                    if ((item.start.Month == DateTime.Today.Month) && (item.start.Day == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void THisYearButton_Click(object sender, RoutedEventArgs e)
        {
            Labels = DateTimeFormatInfo.CurrentInfo.MonthNames;

            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<Assessment> assessmentsList = new List<Assessment>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = 0; i < 12; i++)
            {
                assessmentsList.Clear();
                foreach (Assessment item in FallPredictionList)
                {
                    if ((item.start.Year == DateTime.Today.Year) && (item.start.Month == i))
                        assessmentsList.Add(item);
                }
                currentVal = SumErrors(assessmentsList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void AllTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int minYear = DateTime.Now.Year;
            foreach (Assessment item in FallPredictionList)
                if (item.start.Year <= minYear)
                    minYear = item.start.Year;
            int maxYear = DateTime.Now.Year;
            if (maxYear - minYear == 0)
            {
                THisYearButton_Click(sender, e);
                return;
            }
            Labels = new string[maxYear - minYear];
            for (int i = 0; i < maxYear - minYear; i++)
                Labels[i] = (i + minYear).ToString();

            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<Assessment> predictionList = new List<Assessment>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = minYear; i <= maxYear; i++)
            {
                predictionList.Clear();
                foreach (Assessment item in FallPredictionList)
                {
                    if (item.start.Year == i)
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }
    }
}