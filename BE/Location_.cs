using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Geocoding;
using Geocoding.Microsoft.Json;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Device.Location;
using Location = Geocoding.Microsoft.Json.Location;
using ExifLib;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class Location_ 
    {
        #region Properties
     //   [Key]
        private int Id;
        private double Latitude;
        private double Longitude;
        private string Address;
        public int assessmentId { get; set; }
        public int fallid { get; set; }
        public int reportid { get; set; }
        #endregion


        #region public set/get

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
                  
                }
            }
        }
        public double latitude
        {
            get
            {
                return Latitude;
            }
            set
            {
                if (Latitude != value)
                {
                    Latitude = value;
                }
            }
        }
        public double longitude
        {
            get
            {
                return Longitude;
            }
            set
            {
                if (Longitude != value)
                {
                    Longitude = value;
                   
                }
            }
        }
        public string address
        {
            get
            {
                return Address;
            }
            set
            {
                if (Address != value)
                {
                    Address = value;
                  
                }
            }
        }
        #endregion
        #region Constructor
        public Location_()
        {


        }
        public Location_(string _Address)
        {
            Address = _Address;
            //need to convert from Address to Latitude and Longitude
            try
            {

                string url = "http://dev.virtualearth.net/REST/v1/Locations?query=" + _Address + "&key=I0VLWblEtzGy2oxBtvf7~JJj7TSmhuiA7hT0hN_W6jw~Ake5bPnub0u2657LhMfe3WiODJ6sfV-05Ugn7CclWhVJej75KU4f4j0XqW_dXvaH";

                using (var client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                    using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                    {
                        var mapResponse = (ser.ReadObject(es) as Response); //Response is one of the Bing Maps DataContracts
                        Location location = (Location)mapResponse.ResourceSets.First().Resources.First();

                        Latitude = location.Point.Coordinates[0];
                        Longitude = location.Point.Coordinates[1];

                    }
                }

            }
            catch (Exception)
            {

                throw new Exception("Youre Address is incorrect!");
            }

        }

        public Location_(double _Latitude, double _Longitude)
        {
            Latitude = _Latitude;
            Longitude = _Longitude;

            //need to convert from Latitude and Longitude to Address
            try
            {
                using (var client = new WebClient())
                {
                    var queryString = "http://dev.virtualearth.net/REST/v1/Locations/" + Latitude.ToString() + "," + Longitude.ToString() + "?key=I0VLWblEtzGy2oxBtvf7~JJj7TSmhuiA7hT0hN_W6jw~Ake5bPnub0u2657LhMfe3WiODJ6sfV-05Ugn7CclWhVJej75KU4f4j0XqW_dXvaH";

                    string response = client.DownloadString(queryString);
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                    using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                    {
                        var mapResponse = (ser.ReadObject(es) as Response);
                        Location location = (Location)mapResponse.ResourceSets.First().Resources.First();
                        Address =  location.Address.AddressLine + ", " + location.Address.Locality + ", " + location.Address.CountryRegion;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("invalid location");

            }

        }
        public Location_(Location_ location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            Address = location.Address;

        }
        #endregion

        public GeoCoordinate GetCoordinate()
        {
            return new GeoCoordinate(Latitude, Longitude);
        }

      

    }



    //* &key=AIzaSyA9BTov625RYpGhBcm4Nta44sg515trF3s*/
}

