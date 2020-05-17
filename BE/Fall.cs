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
    public class Fall
    {
        #region Properties : INotifyPropertyChanged
       // [Key]
        int Id;
        DateTime Time;
        Location_ Location;
        string Image;
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
        public string image
        {
            get
            {
                return Image;
            }
            set
            {
                if (Image != value)
                {
                    Image = value;
                    OnPropertyChanged("image");
                }
            }
        }
        #endregion

        #region Constructor
        //string or location class??
        public Fall(DateTime fall_Time, string fall_Location, string fall_Image)
        {
            time = new DateTime(fall_Time.Ticks);
            location = new Location_(fall_Location);
            image = fall_Image;
        }

        public Fall(DateTime fall_Time, string fall_Image)
        {
            time = new DateTime(fall_Time.Ticks);
            image = fall_Image;
        }
        public Fall()
        {
            time = new DateTime();
            image = "";
            location = new Location_(26.548152, 4.416960);
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
