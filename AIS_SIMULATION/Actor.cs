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
        
        public string Name; // name of the node in the network
        //TwoVesselSimulation sim = new TwoVesselSimulation();
       
        public void sendBeacon(string transmitName1, int testNum1, string transmitName12, int testNum2)
        {
            Trace.WriteLine("Packet sent...");
            

            
        }

        public void receiveBeacon()
        {
            throw new System.NotImplementedException();
        }

        //function used calculate the distance between two vessels
        //outputs a value in statute miles
        public double getDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515 * 0.8684;
            return (dist);
            
        }
        
        //function used in getDistance to convert degrees to radians
        public double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //function used in getDistance to convert radians to degrees
        public double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}




    

