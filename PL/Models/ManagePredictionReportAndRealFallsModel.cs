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
            var list = ibl.GetAllAassessments().ToList();
           
           
            foreach (Assessment a in list)
            {
                foreach (Location_ l in a.Locations)
                {
                    AssessmentLocation.Add(l);
                }
            }




            ReportLocation = (from r in ibl.GetAllReports()
                              select r.location).ToList();
        }

        private void initForTheFirstTime()
        {
            ibl.AddReport(new Report() { intensity = 1, numOfExplosions = 3, time = DateTime.Now, location = new Location_() { AssessmentId = 1, latitude = 40.668980, longitude = -73.942840 } });
            ibl.AddReport(new Report() { intensity = 1, numOfExplosions = 2, time = DateTime.Now, location = new Location_() { AssessmentId = 1, latitude = 31.768318, longitude = 35.213711 } });
            ibl.AddReport(new Report() { intensity = 1, numOfExplosions = 8, time = DateTime.Now, location = new Location_() { AssessmentId = 1, latitude = 31.992010, longitude = 34.849190 } });

        }
    }
}
