using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.Models
{
    public class ManageFallReportModel
    {
        readonly IBL ibl = FactoryBL.GetBL();
        public ManageFallReportModel()
        {
        }

        public void RemoveFallReport(int idToDelete)
        {
            ibl.RemoveReport(idToDelete);
        }
        public void AddFallReport(Report fallReport)
        {
            ibl.AddReport(fallReport);
        }
        public ObservableCollection<Report> AllFallReports()
        {
            List<Report> temp = ibl.GetAllReports().ToList();
            ObservableCollection<Report> theCollection = new ObservableCollection<Report>();
            foreach (Report item in temp)
                theCollection.Add(item);
            return theCollection;

        }
    }
}
