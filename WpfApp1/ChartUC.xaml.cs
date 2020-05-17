using BE;
using PL.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Device.Location;
using LiveCharts;

using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using BL;
using javax.swing.text.html;
using com.sun.corba.se.impl.ior.iiop;

namespace PL
{
    /// <summary>
    /// Interaction logic for ChartUC.xaml
    /// </summary>
    public partial class ChartUC : UserControl
    {
        public AssessmentVM CurrentAssessmentVM { get; set; }
        public FallsVM CurrentFallVM { get; set; }
        List<Assessment> Assessments;
        private SeriesCollection _seriesCol;
        public virtual SeriesCollection seriesCol
        {
            get { return _seriesCol; }
            set
            {
                _seriesCol = value;
            }
        }
        public ChartUC()
        {
            InitializeComponent();
            DataContext = this;
            CurrentAssessmentVM = new AssessmentVM();
            CurrentFallVM = new FallsVM();
            Assessments = ((IEnumerable<Assessment>)(CurrentAssessmentVM.Assessments)).ToList<Assessment>();
            InitData();

        }
        public void Load()
        {
            CurrentAssessmentVM = new AssessmentVM();
            CurrentFallVM = new FallsVM();
            Assessments = ((IEnumerable<Assessment>)(CurrentAssessmentVM.Assessments)).ToList<Assessment>();
            InitData();
        }
        private void MakeData()
        {
            MyPieChart.Series.Clear();
            List<string> allValues = new List<string>();
            var NameLables = new List<string>();
            double precent = Precent();
            

            int precent1 = (int)(precent * 100);

            int temp = 100 - precent1;

            allValues.Add((temp).ToString());
            NameLables.Add((temp).ToString() + "%");

            allValues.Add((precent1).ToString());
            NameLables.Add(precent1.ToString() + "%");


            seriesCol = new SeriesCollection();

            MyPieChart.Series.AddRange(Enumerable.Range(0, allValues.Count).Select(x => new PieSeries { Title = NameLables[x], Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(allValues[x])) } }));
        }
        private void InitData()
        {

            List<string> allValues = new List<string>();
            var NameLables = new List<string>();
            double precent = Precent();
            int precent1 = (int)(precent * 100);


            int temp = 100 - precent1;

            allValues.Add((temp).ToString());
            NameLables.Add((temp).ToString() + "%");
            allValues.Add((precent1).ToString());
            NameLables.Add(precent1.ToString() + "%");

            seriesCol = new SeriesCollection();

            seriesCol.AddRange(Enumerable.Range(0, allValues.Count).Select(x => new PieSeries { Title = NameLables[x], Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(allValues[x])) } }));
        }

        private double Precent()
        {
            Fall currentFall = new Fall();
            double currentDinstance = 0;
            int errorConter = 0;
            if (Assessments == null)
                return 1;
            foreach (Assessment item in Assessments)
            {
                currentFall = CurrentFallVM.GetFall(item.id);
                if (currentFall == null)
                {
                    errorConter++;
                }
                else
                {
                    currentDinstance = GetDinstance(item, currentFall);
                    double temp = Double.Parse(RangeLable.Content.ToString().Substring(0, RangeLable.Content.ToString().Length - 1));
                    if (currentDinstance > temp)
                        errorConter++;
                }

            }
            if (Assessments.Count() == 0) return 0;
            double retValue = (double)errorConter / ((double)Assessments.Count());
            return retValue;
        }
       
        private double GetDinstance( Assessment l1, Fall l2)
        {
           
            List<Fall> fall = (from f in CurrentFallVM.CurrentModel.AllFalls()
                               where f.id==l2.id
                               select f).ToList();
            if(fall.Count==0)
            {
                return -1;
            }
            else
            {
                double min = double.MaxValue;
                GeoCoordinate location2 = new GeoCoordinate(l2.location.latitude, l2.location.longitude);
                foreach (Location_ location1 in l1.locations)
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

        private void ErrorRangeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MakeData();
        }
    }
}
