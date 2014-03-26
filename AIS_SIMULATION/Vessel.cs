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

        public string Pseudonym; //pxxxxxx: 6-character pseudonym, starting with “p”;

        public double Speed; //XX: speed over ground in kts

        public int MMSI; //MMSI of the vessel

        public int IMO;

        public string CSgn;

        public string Dest;

        public string ETA;

        public int Course; //XXX: course in degrees true

        public double Latitude; //XX.XXXXi: latitude in decimal degrees (4 decimals)

        public double Longitude; //XXX.XXXXi: longitude in decimal degrees (4 decimals)

        public List<string> beaconList = new List<string>(); //list containing beacons from other vessels

        //full vessel information below

        public double length; //lXXX: length in meters, proceeded by “l”

        public double tonnage; //tXXX: tonnage in tons, proceeded “t”

        public string cargoType; //ctxx: cargo type in 2-character identifier, proceeded by “ct”
        

        public string vesselCategory; //vcXX: vessel category in 2-number identifier, proceeded by “vc”
        //vcCV: is a civilian vessel
        //vcCG: is a coast guard vessel

        public string operatingMission; //omXXX: operating mission in 3-number identifier, proceeded by “om”

        

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
