using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DALimp : IDAL
    {
        //private static Project_Context db ;

        #region Report functions
        public void AddReport(Report report)
        {
            var ToAdd = GetReport(report.id);
            if (ToAdd != null)
            {
                throw new Exception("An Assessment with an identical id already exists...");
            }
            using (var db = new Project_Context())
            {
                try
                {
                    AddLocation(report.location);
                    db.SaveChanges();
                    db.Reports.Add(report);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.InnerException.Message);
                }
            }
        }

        public void RemoveReport(int id)
        {
            Report ToRemove = GetReport(id);
            if (ToRemove == null)
                throw new Exception("An report with this ID does not exist..");
            using (var db = new Project_Context())
            {
                db.Reports.Remove(ToRemove);
                db.SaveChanges();
            }
        }

        public void UpdateReport(Report report)
        {
            var ToUpdate = GetReport(report.id);
            if (ToUpdate == null)
                throw new Exception("the report to update not found");
            using (var db = new Project_Context())
            {
                db.Entry(report);
                db.Reports.AddOrUpdate(report);
                db.SaveChanges();
            }
        }

        public Report GetReport(int id)
        {
            using (var db = new Project_Context())
            {
                return db.Reports.SingleOrDefault(r => r.id == id);
            }
        }

        public List<Report> GetAllReports()
        {
            using (var db = new Project_Context())
            {
                if (db.Reports == null)
                    throw new Exception("No reports exist in the system");
                return db.Reports.ToList();
            }
        }
        #endregion

        #region Assessment functions
        public void AddAssessment(Assessment assessment)
        {
            var ToAdd = GetAssessment(assessment.id);
            if (ToAdd != null)
            {
                throw new Exception("An Assessment with an identical id already exists...");
            }
            using (var db = new Project_Context())
            {
                db.Assessments.Add(assessment);
                db.SaveChanges();
            }
        }

        public void RemoveAssessment(int _id)
        {
            Assessment ToRemove = GetAssessment(_id);
            if (ToRemove == null)
                throw new Exception("An Assessment with this ID does not exist..");
            using (var db = new Project_Context())
            {
                db.Assessments.Remove(ToRemove);
                db.SaveChanges();
            }
        }

        public void UpdateAssessment(Assessment assessment)
        {
            var ToUpdate = GetAssessment(assessment.id);
            
            if (ToUpdate == null)
                throw new Exception("the assessment to update not found");
            using (var db = new Project_Context())
            {
                db.Entry(assessment);
                db.Assessments.AddOrUpdate(assessment);
                db.SaveChanges();
            }
        }

        public Assessment GetAssessment(int _id)
        {
            using (var db = new Project_Context())
            {
                //  var r=db.Assessments.FirstOrDefault(e => e.id == _id);
                //    r.reports=from a in db.Reports.ToList()
                //            where a.
                return db.Assessments.FirstOrDefault(e => e.id == _id);
            }
        }

        public List<Assessment> GetAllAassessments()
        {
            using (var db = new Project_Context())
            {
                if (db.Assessments == null)
                    throw new Exception("No assessments exist in the system");
                return db.Assessments.ToList();
            }
        }
        #endregion

        #region Fall functions
        public void AddFall(Fall fall)
        {
            var ToAdd = GetFall(fall.id);
            if (ToAdd != null)
            {
                throw new Exception("An Fall with an identical id already exists...");
            }
            using (var db = new Project_Context())
            {
                db.Falls.Add(fall);
                db.SaveChanges();
            }
        }

        public void RemoveFall(int id)
        {
            Fall ToRemove = GetFall(id);
            if (ToRemove == null)
                throw new Exception("An Assessment with this ID does not exist..");
            using (var db = new Project_Context())
            {
                db.Falls.Remove(ToRemove);
                db.SaveChanges();
            }

        }

        public void UpdateFall(Fall fall)
        {
            var ToUpdate = GetFall(fall.id);
            if (ToUpdate == null)
                throw new Exception("the assessment to update not found");
            using (var db = new Project_Context())
            {
                db.Entry(fall);
                db.Falls.AddOrUpdate(fall);
                db.SaveChanges();
            }
        }

        public Fall GetFall(int id)
        {
            using (var db = new Project_Context())
            {
                return db.Falls.FirstOrDefault(f => f.id == id);
            }
        }

        public List<Fall> GetAllFalls()
        {
            using (var db = new Project_Context())
            {
                if (db.Falls == null)
                    throw new Exception("No falls exist in the system");
                var d = db.Falls;
                return db.Falls.ToList();
            }
        }
        #endregion


        #region Fall functions
        public void AddLocation(Location_ location)
        {
            var ToAdd = GetLocation(location.id);
            if (ToAdd != null)
            {
                throw new Exception("An Location with an identical id already exists...");
            }
            using (var db = new Project_Context())
            {
                db.Locations.Add(ToAdd);
                db.SaveChanges();
            }
        }

        public void RemoveLocation(int id)
        {
            Location_ ToRemove = GetLocation(id);
            if (ToRemove == null)
                throw new Exception("An Assessment with this ID does not exist..");
            using (var db = new Project_Context())
            {
                db.Locations.Remove(ToRemove);
                db.SaveChanges();
            }

        }

        public Location_ GetLocation(int id)
        {
            using (var db = new Project_Context())
            {
                return db.Locations.FirstOrDefault(r => id == id);
            }
        }

        public List<Location_> GetAllLocation()
        {
            using (var db = new Project_Context())
            {
                if (db.Locations == null)
                    throw new Exception("No falls exist in the system");
                return db.Locations.ToList();
            }
        }

        public List<Location_> GetLocationOfAssessment(Assessment a)
        {
           // List<Location_> tmp = new List<Location_>;
            return null;
          
        }
        #endregion


    }
}
