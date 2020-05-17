using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using ExifLib;
using BE;
using DAL;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Runtime.InteropServices;

namespace BL
{
    public class BLimp : IBL
    {
        private DAL.IDAL dal = DAL.FactoryDAL.GetDAL();
        //  BL.BLimp bl;

        #region Report Functions
        public void AddReport(Report report)
        {
            try
            {
                dal.AddReport(report);

                //Add a link between report and assessment
                List<Assessment> possible = (from a in GetAllAassessments()
                                             where a.start <= report.time &&
                                             a.end >= report.time
                                             select a).ToList();
                Assessment asses;
                if (possible.Count() == 0)
                {
                    asses = new Assessment(report);
                    AddAssessment(asses);
                    //GetAllAassessments().Last().reports.Add(report);
                  

                }
                else//(possible.Count() == 1)
                {
                    asses = possible[0];
                    possible[0].reports.Add(report);
                }
                //update Kmeans
                UpdateAssessment(asses);

            }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
            }


        }
        public void RemoveReport(int id)
        {
            try
            {
                dal.RemoveReport(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateReport(Report report)
        {
            try
            {
                Report r = dal.GetReport(report.id);
                dal.UpdateReport(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public Report GetReport(int id)
        {

            try
            {
                Report r = dal.GetReport(id);
                return r;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public IEnumerable<Report> GetAllReports(Func<Report, bool> pFunc = null)
        {

            if (pFunc == null)
            {
                return dal.GetAllReports();
            }
            else
            {
                return (from report in dal.GetAllReports()
                        where pFunc(report)
                        select report).ToList();

            }
        }
        #endregion

        #region Assessment Functions
        public void AddAssessment(Assessment assessment)
        {
            try
            {
                dal.AddAssessment(assessment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void RemoveAssessment(int id)
        {
            try
            {
                dal.RemoveAssessment(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateAssessment(Assessment assessment)
        {
            try
            {
               // Assessment asses =GetAssessment(assessment.id);

                //remove last assessments location
                assessment.locations.Clear();

                int k = (int)assessment.reports.Average(r => r.numOfExplosions);
                List<GeoCoordinate> clusters = kMeans(assessment.reports.ToList(), k);
                foreach (GeoCoordinate c in clusters)
                {
                    Location_ temp = new Location_(c.Latitude, c.Longitude);
                    assessment.locations.Add(temp);
                }

                dal.UpdateAssessment(assessment);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Assessment GetAssessment(int id)
        {
            return dal.GetAssessment(id);
        }
        public IEnumerable<Assessment> GetAllAassessments(Func<Assessment, bool> pFunc = null)
        {
            try
            {
                if (pFunc == null)
                {
                    return dal.GetAllAassessments();
                }
                else
                {
                    return (from asses in dal.GetAllAassessments()
                            where pFunc(asses)
                            select asses).ToList();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion

        #region Fall Functins
        public void AddFall(Fall fall)
        {
            try
            {
                fall.location = new Location_(GetLatLongFromImage(fall.image.ToString().Substring(8)));
                //fall.ad
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        public void RemoveFall(int id)
        {
            try
            {
                dal.RemoveFall(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateFall(Fall fall)
        {
            try
            {
                Fall fal = dal.GetFall(fall.id);
                dal.UpdateFall(fall);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Fall GetFall(int id)
        {
            return dal.GetFall(id);
        }
        public IEnumerable<Fall> GetAllFalls(Func<Fall, bool> pFunc = null)
        {
            try
            {
                if (pFunc == null)
                {
                    return dal.GetAllFalls();
                }
                else
                {
                    return (from fall in dal.GetAllFalls()
                            where pFunc(fall)
                            select fall).ToList();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion

        #region Get GeoTag from image Functions

        // The following functions were taken from the following link:"https://stackoverflow.com/questions/20801314/how-to-fetch-the-geotag-details-of-the-captured-image-or-stored-image-in-windows"

        public Location_ GetLatLongFromImage(string imagePath)
        {
            imagePath.Replace(@"\", "/");

            ExifReader reader = new ExifReader(imagePath);

            // EXIF lat/long tags stored as [Degree, Minute, Second]
            double[] latitudeComponents;
            double[] longitudeComponents;

            string latitudeRef; // "N" or "S" ("S" will be negative latitude)
            string longitudeRef; // "E" or "W" ("W" will be a negative longitude)

            if (reader.GetTagValue(ExifTags.GPSLatitude, out latitudeComponents)
                && reader.GetTagValue(ExifTags.GPSLongitude, out longitudeComponents)
                && reader.GetTagValue(ExifTags.GPSLatitudeRef, out latitudeRef)
                && reader.GetTagValue(ExifTags.GPSLongitudeRef, out longitudeRef))
            {

                var latitude = ConvertDegreeAngleToDouble(latitudeComponents[0], latitudeComponents[1], latitudeComponents[2], latitudeRef);
                var longitude = ConvertDegreeAngleToDouble(longitudeComponents[0], longitudeComponents[1], longitudeComponents[2], longitudeRef);
                return new Location_(latitude, longitude);
            }

            return null;
        }

        //Auxiliary functions for  GetLatLongFromImage Function
        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds, string latLongRef)
        {
            double result = ConvertDegreeAngleToDouble(degrees, minutes, seconds);
            if (latLongRef == "S" || latLongRef == "W")
            {
                result *= -1;
            }
            return result;
        }

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }
        #endregion

        #region Kmeans

        List<GeoCoordinate> kMeans(List<Report> reportList, int k)
        {
            if (reportList.Count == 0)
                return null;

            List<GeoCoordinate> clusterID = CGenerate(reportList, k);

            bool isChanged;
            int counter = 0;
            do
            {
                isChanged = false;

                for (int i = 0; i < reportList.Count; i++)
                {
                    reportList[i].clusterId = 0;
                    double minimum = reportList[i].location.GetCoordinate().GetDistanceTo(clusterID[0]);


                    for (int j = 1; j < clusterID.Count; j++)
                    {
                        double tmp = reportList[i].location.GetCoordinate().GetDistanceTo(clusterID[j]);
                        if (tmp < minimum)
                        {
                            minimum = tmp;
                            reportList[i].clusterId = j;
                            isChanged = true;
                        }
                    }

                }

                counter++;
                reportList = reportList.OrderBy(c => c.clusterId).ToList();

                double clustersLongitudeSum = 0;
                int id = 0;
                double clustersLatitudeSum = 0;
                int tmpCounter = 0;
                for (int i = 0; i < reportList.Count; i++)
                {
                    if (reportList[i].clusterId == id)
                    {
                        clustersLatitudeSum += reportList[i].location.GetCoordinate().Latitude;
                        clustersLongitudeSum += reportList[i].location.GetCoordinate().Longitude;
                        tmpCounter++;
                    }
                    else if (reportList[i].clusterId != id)
                    {
                        clusterID[id].Latitude = clustersLatitudeSum / tmpCounter;
                        clusterID[id].Longitude = clustersLongitudeSum / tmpCounter;
                        tmpCounter = 0;
                        clustersLongitudeSum = 0;
                        clustersLatitudeSum = 0;
                        i--;
                        id++;
                    }
                }
                clusterID[id].Latitude = clustersLatitudeSum / tmpCounter;
                clusterID[id].Longitude = clustersLongitudeSum / tmpCounter;

                if (counter == 150)
                {
                    break;
                }
            } while (isChanged);

            return clusterID;
        }

        List<GeoCoordinate> CGenerate(List<Report> reportList, int k)
        {

            List<GeoCoordinate> clustersIdList = new List<GeoCoordinate>();

            double minLatitude = reportList.Min(r => r.location.latitude);
            double maxLatitude = reportList.Max(r => r.location.latitude);
            double minLongitude = reportList.Min(r => r.location.longitude);
            double maxLongitude = reportList.Max(r => r.location.longitude);

            for (int i = 0; i < k; i++)
            {
                Random rand = new Random(DateTime.Today.Ticks.GetHashCode());
                double latitude = minLatitude + rand.NextDouble() * (maxLatitude - minLatitude);
                double longitude = minLongitude + rand.NextDouble() * (maxLongitude - minLongitude);
                GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                clustersIdList.Add(coordinate);
            }

            return clustersIdList;
        }

        #endregion
    }


}

