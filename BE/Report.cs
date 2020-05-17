using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Text;

namespace BE
{
    public class Report: INotifyPropertyChanged
    {
        #region Properties
        
        private int ID;
        private DateTime Time;
        private string ReporterName;
        //public string reporterAddress { get; set; }
        private Location_ Location;
        private int Intensity;
        private int NumOfExplosions;
       // private int ClusterId;

        #endregion

        #region Public get/set
      //  [Key]
        public int id
        {
            get
            {
                return ID;
            }
            set
            {
                if (ID != value)
                {
                    ID = value;
                    OnPropertyChanged("id");
                }
            }
        }
        public DateTime time
        {
            get
            {
                return Time;
            }
            set
            {
                if (Time != value)
                {
                    Time = value;
                    OnPropertyChanged("time");
                }
            }
        }
        public string reporterName
        {
            get
            {
                return ReporterName;
            }
            set
            {
                if (ReporterName != value)
                {
                    ReporterName = value;
                    OnPropertyChanged("reportername");
                }
            }
        }
        public Location_ location
        {
            get
            {
                return Location;
            }
            set
            {
                if (Location != value)
                {
                    Location = value;
                    OnPropertyChanged("location");
                }
            }
        }
        public int intensity
        {
            get
            {
                return Intensity;
            }
            set
            {
                if (Intensity != value)
                {
                    Intensity = value;
                    OnPropertyChanged("intensity");
                }
            }
        }
        public int numOfExplosions
        {
            get
            {
                return NumOfExplosions;
            }
            set
            {
                if (NumOfExplosions != value)
                {
                    NumOfExplosions = value;
                    OnPropertyChanged("numOfExplosions");
                }
            }
        }
        public int clusterId { get; set; }

        #endregion

        #region Constructor
        public Report(Location_ report_location)
        {
            time = DateTime.Now;
            reporterName = "";
            //   reporterAddress = "";
            intensity = 0;
            numOfExplosions = 0;
            location = new Location_(report_location);
        }

        public Report(DateTime report_time, string reporter_name, Location_ report_location, int report_intensity, int report_numOfExplosions)
        {
            Time = report_time;
            ReporterName = reporter_name;
            Location = report_location;
            Intensity = report_intensity;
            NumOfExplosions = report_numOfExplosions;
        }

        public Report()
        {
            Time = DateTime.Now;
            ReporterName = "";
            //   reporterAddress = "";
            Intensity = 0;
            NumOfExplosions = 0;
            Location = new Location_();
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
