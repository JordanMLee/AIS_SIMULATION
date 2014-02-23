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
        Actor actor = new Actor();
        CGCutter Cutter1 = new CGCutter(); 
        CGCutter Cutter2 = new CGCutter();
        AIS_box ais_box = new AIS_box();
        public int targetsNum = 1;
        public bool FNC_pressed = false;
        public bool oneSecDelayFlag = false;

        public TwoVesselSimulation()
        {
            InitializeComponent();
            
            timer2.Enabled = true;



                   
        }
        
        public void startButton_Click(object sender, EventArgs e)
        {
            //progressBarAnimation2();
            transmissionStatus1.Show();
            transmissionStatus2.Show();
            transDis1.Hide();
            transDis2.Hide();
            transmissionStatus2.Text = "Class A";
            timer2.Start();
            transmissionStatus1.Text = "Class A";
            transmissionStatus2.Text = "Class A";
            Cutter1.Name = "Bertholf";
            Cutter1.Pseudonym = "dang3r";

            Cutter1.MMSI = 234;
            Cutter1.Latitude = 41.3782;
            Cutter1.Speed = 12.34;
            Cutter1.Course = 045;

            //

            //Cutter1.Latitude = Convert.ToDouble(Lat1Box.Text);
            Lat1Box.Text = Cutter1.Latitude.ToString();
            Cutter1.Longitude = -072.0947;
            Long1Box.Text = Cutter1.Longitude.ToString();
            
            
           

            Cutter2.Name = "Yeaton";
            Cutter2.Pseudonym = "iee160";
            Cutter2.MMSI = 456;
            Cutter2.Latitude = 41.3778;
            Cutter2.Longitude = -072.0944;
            Cutter2.Speed = 20.06;
            Cutter2.Course = 212;
            //Lat2Box.Text = Cutter2.Latitude.ToString();
            //Long2Box.Text = Cutter2.Longitude.ToString();
            //nameLabel2.Text = Cutter1.Name;
            //nameLabel1.Text = Cutter2.Name;

            
            targets1.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            targets2.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            
            double distance = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            Distance.Text = distance.ToString() + " miles";
            //if distance is less than 5 miles than send beacons
            if (distance < 5)
            {
                StartSim(sender, e);
                
            } 
        }
       
     

        //function to increment the bottom progress bar all the way
        

        public void StartSim(object sender, EventArgs e)
        {
            //Send first packet
            textBox1.AppendText("sending beacon...\r\n");
            PacketTimer.Start();
            //Initialize Timer to send Subsequent Packets
            Sendtimer.Start();
        }

        public void Sendtimer_Tick(object sender, EventArgs e)
        {
            pBanimation1Send();
        }

        public void pBanimation1Send()
        {
            textBox1.AppendText("sending beacon...\r\n");
            PacketTimer.Start();
        }

        public void PacketTimer_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timerAfterSend1.Start();
             
            }
            

        }
        private void timerAfterSend1_Tick(object sender, EventArgs e)
        {
            timerAfterSend1.Stop();
            progressBar1.Value = 0;
            

            PacketTimer.Stop();
            double dist2 = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            if (dist2 < 5)
            {
                packRec();
                Distance.Text = dist2.ToString() + " miles";
                Cutter1.Latitude++;
               
            }
            else
            {
                packDen();
                Distance.Text = dist2.ToString() + " miles";
                Cutter1.Latitude--;
                
            }

        }
       
        //stop button
        public void button2_Click(object sender, EventArgs e)
        {
            this.PacketTimer.Stop();
            timerAfterSend1.Stop();
            Sendtimer.Stop();
            timer2.Stop();
            transmissionStatus1.Hide();
            transmissionStatus2.Hide();
            transDis1.Show();
            transDis2.Show();

            OtherSendTimer.Stop();
            timerAfterSend.Stop();
            PB2timer.Stop();

        }

        public void packRec()
        {
            textBox2.AppendText("beacon rcvd\r\n");
            nameLabel2.Text = Cutter1.Pseudonym;
            nameLabel1.Text = Cutter2.Pseudonym;
            cseLbl1.Text = Cutter2.Course.ToString()+ "°";
            speedLbl1.Text = " " + Cutter2.Speed.ToString();
            cseLbl2.Text = "0" + Cutter1.Course.ToString() + "°";
            speedLbl2.Text = " " + Cutter1.Speed.ToString();
            StartRAn();
        }

        public void packRec2()
        {
            textBox1.AppendText("beacon rcvd\r\n");
            
            
        }

        public void packDen()
        {
            textBox2.AppendText("beacon denied\r\n");
            //nameLabel2.Text = "unknown";
            //nameLabel1.Text = "unknown";
        }
        public void packDen2()
        {
            textBox1.AppendText("beacon denied\r\n");
            //nameLabel2.Text = "unknown";
            //nameLabel1.Text = "unknown";
        }

/********************************************************************************************
 * */




        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void TwoVesselSimulation_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Show 3 vessels
            if (targetsNum == 1)
            {
                targetsNum = 2;
                button3.Text = "2-vessel";
            }
            else if (targetsNum == 2)
            {
                targetsNum = 1;
                button3.Text = "3-vessel";
            }

        }
        public void progressBarAnimation2()
        {
            this.PB2timer.Start();
            textBox2.AppendText("sending beacon...\r\n");
        }

        private void timerAfterSend_Tick(object sender, EventArgs e)
        {
            timerAfterSend.Stop();
            progressBar2.Value = 0;
            PB2timer.Stop();
            double dist3 = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            if (dist3 < 5)
            {
                packRec2();
                //Distance.Text = dist3.ToString() + " miles";
                //Cutter1.Latitude++;

            }
            else
            {
                packDen2();
                //Distance.Text = dist3.ToString() + " miles";
                //Cutter1.Latitude--;

            }


        }

        private void PB2timer_Tick(object sender, EventArgs e)
        {
            this.progressBar2.Increment(2);
            if (progressBar2.Value == 100)
            {
                
                timerAfterSend.Start();
            }
        }


        //clock
        private void timer2_Tick(object sender, EventArgs e)
        {
            time1a.Text = DateTime.Now.ToString("HH:mm:ss") + " R";
            time1b.Text = DateTime.Now.ToString("HH:mm:ss") + " I";
            time2a.Text = DateTime.Now.ToString("HH:mm:ss") + " R";
            time2b.Text = DateTime.Now.ToString("HH:mm:ss") + " I";
            date1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            date2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }

        private void OtherSendTimer_Tick(object sender, EventArgs e)
        {
            progressBarAnimation2();
        }
        public void StartRAn()
        {
            OtherSendTimer.Start();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            FNCstatus.Visible = false;
            if (FNC_pressed == true)
            {
                textBox1.AppendText("sending nav info query...\r\n");
                pBanimation1Send();
                oneSecDelay.Start();
                //if (oneSecDelayFlag == true)
                //{
                //    textBox1.AppendText("request for nav info sent\r\n");
                //    textBox1.AppendText("awaiting response...\r\n");
                //}
            }
        }

        private void FNCbutton_Click(object sender, EventArgs e)
        {
            FNC_pressed = true;
            FNCstatus.Visible = true;
        }

        private void oneSecDelay_Tick(object sender, EventArgs e)
        {
            oneSecDelay.Stop();
            //oneSecDelayFlag = true;
            textBox1.AppendText("request for nav info sent\r\n");
            textBox1.AppendText("awaiting response...\r\n");

        }

    }
}
