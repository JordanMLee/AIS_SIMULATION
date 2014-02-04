using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AIS_SIMULATION
{
    public partial class TwoVesselSimulation : Form
    {
        public TwoVesselSimulation()
        {
            InitializeComponent();
        }
        
        public void startButton_Click(object sender, EventArgs e)
        {
            CGCutter Cutter1 = new CGCutter();

            Cutter1.Name = "Bertholf";
            Cutter1.Latitude = 41.3785;
            Cutter1.Longitude = -072.0952;
            //PacketTimer.Enabled = true;
            //LoopTimer.Enabled = true;

            this.PacketTimer.Start();
            this.LoopTimer.Start();
            textBox1.Text = "sending beacon...";
            
        }

        private void PacketTimer_Tick(object sender, EventArgs e)
        {
            
                this.progressBar1.Increment(10);
                if (progressBar1.Value == 100)
                {
                    this.PacketTimer.Stop();
                    progressBar1.Value = 0;
                    textBox2.Text = "beacon rcvd";

                }
            
        }

        private void LoopTimer_Tick(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }


    }
}
