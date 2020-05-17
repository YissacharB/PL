using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Assessment : INotifyPropertyChanged
    {
        #region Properties
        private int Id;
        private DateTime Start;
        private DateTime End;
        private ICollection<Report> Reports;
        private ICollection<Fall> Falls;
        //  According to KMeans algorithm
        private List<Location_> Locations;

        #endregion


        #region public get/set
        public int id
        {
            get
            {
                return Id;
            }
            set
            {
                if (Id != value)
                {
                    Id = value;
                    OnPropertyChanged("id");
                }
            }
        }
        public DateTime start
        {
            get
            {
                return Start;
            }
            set
            {
                if (Start != value)
                {
                    Start = value;
                    OnPropertyChanged("start");
                }
            }
        }
        public DateTime end
        {
            get
            {
                return End;
            }
            set
            {
                if (End != value)
                {
                    End = value;
                    OnPropertyChanged("end");
                }
            }
        }
     //   [ForeignKey("id")]
        
        public virtual ICollection<Report> reports
        {
            get
            {
                return Reports;
            }
            set
            {
                if (Reports != value)
                {
                    Reports = value;
                    OnPropertyChanged("reports");
                }
            }
        }
        //[ForeignKey("id")]
        public virtual ICollection<Fall> falls
        {
            get
            {
                return Falls;
            }
            set
            {
                if (Falls != value)
                {
                    Falls = value;
                    OnPropertyChanged("falls");
                }
            }
        }
        public virtual List<Location_> locations
        {
            get
            {
                return Locations;
            }
            set
            {
                if (Locations != value)
                {
                    Locations = value;
                    OnPropertyChanged("locations");
                }
            }
        }

        #endregion

        #region Constructor
        public Assessment(Report report)
        {
           // Start = new DateTime(report.time.Day, report.time.Hour, report.time.Minute);
            Start = new DateTime(report.time.Ticks);
            End = start.AddMinutes(10);
            reports = new List<Report>();
            reports.Add(report);
            locations = new List<Location_>();
        }


        public Assessment()
        {
            Start = new DateTime();
            End = start.AddMinutes(10);
            reports = new List<Report>();
            locations = new List<Location_>();
        }
        #endregion
        
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
