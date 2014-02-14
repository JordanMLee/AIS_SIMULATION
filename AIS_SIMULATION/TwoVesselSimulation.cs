using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace AIS_SIMULATION
{
    public partial class TwoVesselSimulation : Form
    {
        //ArrayList simActors = new ArrayList();
        //Actor actor = new Actor();
        //static public int BeaconSentCount = 0;
        //static public int _BeaconSentCount = 0;
        //public int timerCount = 0;
        
        //CGCutter Cutter1 = new CGCutter(); 
        //CGCutter Cutter2 = new CGCutter(); 
        
            
        public TwoVesselSimulation()
        {
            InitializeComponent();
            progressBarAnimation2();
            pBanimation1Send();
             
        }
        
        public void startButton_Click(object sender, EventArgs e)
        {

            //Cutter1.Name = "Bertholf";
            //Cutter1.MMSI = 234;
            //Cutter1.Latitude = 41.3782;
            //Cutter1.Longitude = -072.0947;

            //Cutter2.Name = "Yeaton";
            //Cutter2.MMSI = 456;
            //Cutter2.Latitude = 41.3778;
            //Cutter2.Longitude = -072.0944;
            
         
           
            ////textBox1.AppendText( "sending beacon...");
            //double distance = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            //Trace.WriteLine(distance);
            //if (distance < 5)
            //{
            //    textBox1.AppendText("sending beacon...");
            //    beaconSent(Cutter1.Name, Cutter2.Name);
            //    simActors.Add(Cutter1);

            //    this.PacketTimer.Start();
            //    this.timer1.Start();
            //}
            
        }

        public void pBanimation1Send()
        {
            PacketTimer.Start();
        }

        public void PacketTimer_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timerAfterSend1.Start();
            }
            //Cutter1.Name = "Bertholf";
            //Cutter1.MMSI = 234;
            //foreach (Actor a in simActors)
            //{
            //    ((Actor)a).sendBeacon(Cutter1.Name, Cutter1.MMSI, Cutter2.Name, Cutter2.MMSI);

            //    this.LoopTimer.Start();
            //    if (BeaconSentCount >= 5)
            //        button2_Click(sender, e);
            //    Trace.WriteLine(String.Format("BeaconSentCount = {0}",BeaconSentCount));
               
            //    BeaconSentCount++;
                
            //}
            
                //if (_BeaconSentCount > 0)
                //{
                //    textBox2.AppendText("\r\nbeacon rcvd");
                  
                //    textBox1.AppendText("\r\nsending beacon...");
                //}
                //else
                //    textBox2.Text = "beacon rcvd";
                //_BeaconSentCount++;
           
        }
        
        private void LoopTimer_Tick(object sender, EventArgs e)
        {

            //this.progressBar1.Increment(90);
            //if (progressBar1.Value == 100)
            //{
            //    LoopTimer.Stop();
            //    progressBar1.Value = 0;
            //}
           
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        public void button2_Click(object sender, EventArgs e)
        {
            this.PacketTimer.Stop();
          
        }
       
        public void beaconSent(string Vessel1, string Vessel2)
        {
           
            //Cutter1.beaconList.Add(Vessel2);
            //Cutter2.beaconList.Add(Vessel1);

        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            //timerCount++;
        }

        //function to increment the bottom progress bar all the way
        public void progressBarAnimation2()
        {
            this.PB2timer.Start(); 
        }
        
        private void timerAfterSend_Tick(object sender, EventArgs e)
        {
            timerAfterSend.Stop();
            progressBar2.Value = 0;
        }

        private void PB2timer_Tick(object sender, EventArgs e)
        {
            this.progressBar2.Increment(2);
            if (progressBar2.Value == 100)
            {
                //this.timer2.Stop();
                timerAfterSend.Start();
            }
        }

        private void timerAfterSend1_Tick(object sender, EventArgs e)
        {
            timerAfterSend1.Stop();
            progressBar1.Value = 0;
        }


    }
}
