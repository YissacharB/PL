using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using NSubstitute;
using PL.Models;

namespace PL.ViewModels
{
    public sealed class PredictionAndRealFallsVM
    {
        public ObservableCollection<Location_> ReportLocation { get; set; }
        public ObservableCollection<Location_> AssessmentLocation { get; set; }
        public ObservableCollection<Location_> Falllocation { get; set; }

        public ManagePredictionReportAndRealFallsModel currentModel;
        private static PredictionAndRealFallsVM instance = null;
        public static PredictionAndRealFallsVM Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PredictionAndRealFallsVM();
                }
                return new PredictionAndRealFallsVM();
            }
        }

        private PredictionAndRealFallsVM()
        {
            currentModel = ManagePredictionReportAndRealFallsModel.Instance;
            ReportLocation = new ObservableCollection<Location_>(currentModel.ReportLocation);
            AssessmentLocation=new ObservableCollection<Location_>(currentModel.AssessmentLocation);
            Falllocation= new ObservableCollection<Location_>(currentModel.FallLocation);
            ReportLocation.CollectionChanged += ReportLocation_CollectionChanged;
            AssessmentLocation.CollectionChanged += AssessmentLocations_CollectionChanged;
            Falllocation.CollectionChanged += Falllocations_CollectionChanged;
        }
        private void ReportLocation_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add
                currentModel.ReportLocation.Add(e.NewItems[0] as Location_);
            }
        }
        private void AssessmentLocations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add
                currentModel.AssessmentLocation.Add(e.NewItems[0] as Location_);
            }
        }
        private void Falllocations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add
                currentModel.FallLocation.Add(e.NewItems[0] as Location_);
            }
        }
        public void AddReport(Location_ location)
        {
            Location_ temp = new Location_(location);
            currentModel.ReportLocation.Add(temp);
        }
        public void AddAssessment(Location_ location)
        {
            Location_ temp = new Location_(location);
            currentModel.AssessmentLocation.Add(temp);
        }
        public void AddFall(Location_ location)
        {
            Location_ temp = new Location_(location);
            currentModel.FallLocation.Add(temp);
        }

        //remove location ????
    }
}
