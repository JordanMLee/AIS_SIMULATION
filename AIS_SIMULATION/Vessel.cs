using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AIS_SIMULATION
{
    public class Vessel : Actor
    {
        //vessel name is location in the Actor class

        public string Pseudonym; //pseudonym of the vessel

        public double Speed; //speed of the vessel

        public int MMSI; //MMSI of the vessel

        public int Course; //trackline of the vessel

        public double Latitude; //latitude of the vessel's location

        public double Longitude; //longitude of the vessel's location

        public List<string> beaconList = new List<string>(); //list containing beacons from other vessels

        

        public void getNavigationInfo()
        {
            throw new System.NotImplementedException();
        }

        public void getAmplifiedInfo()
        {
            throw new System.NotImplementedException();
        }

       
    }
}
