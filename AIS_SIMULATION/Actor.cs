using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AIS_SIMULATION
{
    public class Actor
    {
        public string Name;
        public double _Latitude1;
        public double _Longitude1;
        public double _Latitude2;
        public double _Longitude2;

        public void sendBeacon()
        {
            Trace.WriteLine("Packet sent...");
        }

        public void receiveBeacon()
        {
            throw new System.NotImplementedException();
        }

        public double getDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            //double theta = _Longitude1 -_Longitude2;
            
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515 * 0.8684;
            return (dist);
        }

        public double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        public double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}




    

