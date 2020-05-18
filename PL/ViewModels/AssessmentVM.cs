using BE;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class AssessmentVM 
    {
        public ObservableCollection<Assessment> Assessments { get; set; }
        public ManageFallPredictionModel CurrentModel { get; set; }
        public PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }

        public AssessmentVM()
        {
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
            CurrentModel = new ManageFallPredictionModel();
            Assessments = new ObservableCollection<Assessment>(CurrentModel.AllAssessments());
            // FallPredictions = CurrentModel.AllFallPrediction();
            Assessments.CollectionChanged += Assessment_CollectionChanged;
        }

        private void Assessment_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add
                CurrentModel.Add(e.NewItems[0] as Assessment);
                PredictionAndRealFalls.AddAssessment(e.NewItems[0] as Location_);
                    //????
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //remove
                var oldId = (e.OldItems[0] as Assessment).id;
                CurrentModel.Remove(oldId);

            }

        }
    }
}
