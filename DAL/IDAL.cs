using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {

        #region Report Functions
        void AddReport(Report report);
        void RemoveReport(int id);
        void UpdateReport(Report report);
        Report GetReport(int id);
        List<Report> GetAllReports();
        #endregion

        #region Assessment Functions
        void AddAssessment(Assessment assessment);
        void RemoveAssessment(int id);
        void UpdateAssessment(Assessment assessment);
        Assessment GetAssessment(int id);
        List<Assessment> GetAllAassessments();
        #endregion

        #region Fall Functins
        void AddFall(Fall fall);
        void RemoveFall(int id);
        void UpdateFall(Fall fall);
        Fall GetFall(int id);
        List<Fall> GetAllFalls();
        #endregion

      
      
    }
}
