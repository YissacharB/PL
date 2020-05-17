using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BE;
using BL;
using sun.awt;
using static BE.Location_;

namespace PL.Models
{
    public sealed class ManagePredictionReportAndRealFallsModel
    {
        readonly IBL ibl = FactoryBL.GetBL();
        public List<Location_> ReportLocation { get; set; }
        public List<Location_> FallLocation { get; set; }
        public List<Location_> AssessmentLocation { get; set; }

        private static ManagePredictionReportAndRealFallsModel instance = null;
        public static ManagePredictionReportAndRealFallsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagePredictionReportAndRealFallsModel();
                }
                return new ManagePredictionReportAndRealFallsModel();
            }
        }

        private ManagePredictionReportAndRealFallsModel()
        {
            ReportLocation = new List<Location_>();
            FallLocation = new List<Location_>();
            AssessmentLocation = new List<Location_>();
            
            FallLocation = (from fall in ibl.GetAllFalls()
                            select fall.location).ToList();
            foreach(Assessment a in ibl.GetAllAassessments().ToList())
            {
                foreach(Location_ l in a.locations)
                {
                    AssessmentLocation.Add(l);
                }
            }

        


            ReportLocation = (from r in ibl.GetAllReports()
                              select r.location).ToList();
        }
    }
}
