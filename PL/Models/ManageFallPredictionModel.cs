using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL.Models
{
    public class ManageFallPredictionModel
    {
        readonly IBL ibl = FactoryBL.GetBL();

        public ManageFallPredictionModel()
        {
        }
        public void Remove(int idToDelete)
        {
            ibl.RemoveAssessment(idToDelete);
        }

        public void Add(Assessment assessment)
        {
            ibl.AddAssessment(assessment);
        }
        public ObservableCollection<Assessment> AllAssessments()
        {
            List<Assessment> temp = ibl.GetAllAassessments().ToList();
            ObservableCollection<Assessment> theCollection = new ObservableCollection<Assessment>();
            foreach (Assessment item in temp)
                theCollection.Add(item);
            return theCollection;


        }

    }
}


