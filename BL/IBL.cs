using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Report Functions
        void AddReport(Report report);
        void RemoveReport(int id);
        void UpdateReport(Report report);
        Report GetReport(int id);
        IEnumerable<Report> GetAllReports(Func<Report, bool> pFunc = null);
        #endregion

        #region Assessment Functions
        void AddAssessment(Assessment assessment);
        void RemoveAssessment(int id);
        void UpdateAssessment(Assessment assessment);
        Assessment GetAssessment(int id);
        IEnumerable<Assessment> GetAllAassessments(Func<Assessment, bool> pFunc = null);
        #endregion

        #region Fall Functins
        void AddFall(Fall fall);
        void RemoveFall(int id);
        void UpdateFall(Fall fall);
        Fall GetFall(int id);
        IEnumerable<Fall> GetAllFalls(Func<Fall, bool> pFunc = null);
        #endregion

    }
}
