using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AIS_SIMULATION
{
    public partial class TwoVesselSimulation : Form
    {
        ArrayList simActors = new ArrayList();
        static public int BeaconSentCount = 0;

        public TwoVesselSimulation()
        {
            InitializeComponent();
        }
        
        public void startButton_Click(object sender, EventArgs e)
        {
            CGCutter Cutter1 = new CGCutter();

            Cutter1.Name = "Bertholf";
           // Cutter1.Latitude = 41.3785;
            //Cutter1.Longitude = -072.0952;
            //PacketTimer.Enabled = true;
            //LoopTimer.Enabled = true;

            //Place all objects in a ListArray
            
            simActors.Add(Cutter1);
            
            this.PacketTimer.Start();
            //this.LoopTimer.Start();
            textBox1.Text = "sending beacon...";
            
        }

        private void PacketTimer_Tick(object sender, EventArgs e)
        {
            foreach (Actor a in simActors)
            {
                ((Actor)a).sendBeacon();
                if (BeaconSentCount >= 3)
                    button2_Click(sender, e);
                Trace.WriteLine(String.Format("BeaconSentCount = {0}",BeaconSentCount));
                //MessageBox.Show(temp);
                BeaconSentCount++;
                
            }
        }

        private void LoopTimer_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                textBox2.Text = "beacon rcvd";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        public void button2_Click(object sender, EventArgs e)
        {
            this.PacketTimer.Stop();
            MessageBox.Show("STOPPED");
        }


    }
}
