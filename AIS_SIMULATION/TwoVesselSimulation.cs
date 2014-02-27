//=============================================================================
//
//    Program:    IEEE 1609 influenced AIS Capstone Simulation
//    File name:  TwoVesselSimulation.cs
//
//    Name:       Jordan Lee
//    Date:       25 Feb 14  
//
//
//    Purpose:    This program is a simulation of the our new AIS protocol. It
//                displays several use case scenerios and can be used to see 
//                how our protocol will interact in real time.
//
//=============================================================================
//    File History
//=============================================================================
//   Date   |  Vers  |  Programmer  |  Summary of Change  
//==========|========|==============|==========================================
// 00/00/00 | 00.00  |  Jordan Lee  | xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
//          |        |              |
//==========|========|==============|==========================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading; //used for pauses
using System.Timers;  //used for the multiple timers in the simulation

namespace AIS_SIMULATION
{
    public partial class TwoVesselSimulation : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        Actor actor = new Actor(); //new instance of class Actor, see Actor.cs
        CGCutter Cutter1 = new CGCutter();  //new instance of CGCutter
        CGCutter Cutter2 = new CGCutter();  //new instance of CGCutter
        
        public int targetsNum = 1; //number of other targets a vessel can see 
                                   //in simulation
        
        public bool FNC_pressed = false; //flag for FNC button on AIS1(right side)
        public bool FNC_pressed2 = false;//flag for FNC button on AIS2(right side)
        public bool oneSecDelayFlag = false;//flag for Delay function
        public bool button1_click = false;//flag for clicking button1 on AIS1
        public bool button1_2click = false;//flag for clicking button1 on AIS2

        //Constructor
        public TwoVesselSimulation()
        {
       
            InitializeComponent(); 
            timer2.Enabled = true;  //timer for both AIS clocks  
            this.Width = 890;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        
        //function for pressing the start button on the simulation
        public void startButton_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            //================Transmission status code=========================
            transmissionStatus1.Show(); //label for showing Class A transmission
            transmissionStatus2.Show(); //label for showing Class A transmission

            transDis1.Hide(); //label for showing Tx Disabled
            transDis2.Hide(); //label for showing Tx Disabled

            transmissionStatus2.Text = "Class A";
            transmissionStatus1.Text = "Class A";
            //=================================================================


            timer2.Start(); //timer to start clock again when start button is pressed

            //==============Initialization of the two vessels==================
            Cutter1.Name = "Bertholf";
            Cutter1.Pseudonym = "dang3r";
            Cutter1.MMSI = 234;

            Cutter1.Latitude = 41.3782;
            Cutter1.Longitude = -072.0947;
            Cutter1.Speed = 12.34;
            Cutter1.Course = 045;

            //Cutter1.Latitude = Convert.ToDouble(Lat1Box.Text);
            //Lat1Box.Text = Cutter1.Latitude.ToString();
            //Long1Box.Text = Cutter1.Longitude.ToString();
            
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
            //=================================================================

            //number targets displayed on AIS boxes
            targets1.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            targets2.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            
            //initial calc of distance between the two vessels
            double distance = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            Distance.Text = distance.ToString() + " miles";//display the dist on the GUI

            //if distance is less than 5 miles than send beacons
            if (distance < 5)
            {
                StartSim(sender, e);
                
            }//end if 
        }//end start_button

        //function to increment the top progress bar all the way
        public void StartSim(object sender, EventArgs e)
        {
            //Send first packet
            textBox1.AppendText("sending beacon...\r\n");
            PacketTimer.Start();
            //Initialize Timer to send Subsequent Packets
            Sendtimer.Start();
        }

        //timer for when to send beacons (every 15secs)
        public void Sendtimer_Tick(object sender, EventArgs e)
        {
            pBanimation1Send(); //function to run progressbar animation once
        }

        //function to run progessbar animiation once
        public void pBanimation1Send()
        {
            textBox1.AppendText("sending beacon...\r\n");
            PacketTimer.Start();
        }

        //timer to increment the top progress bar
        public void PacketTimer_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timerAfterSend1.Start();
             
            }
        }

        //timer to add delay after the progress bar is full and before it resets
        private void timerAfterSend1_Tick(object sender, EventArgs e)
        {
            timerAfterSend1.Stop();
            progressBar1.Value = 0; //clear the progress bar
            PacketTimer.Stop();
            //check for dist before receiving or denying the beacon
            double dist2 = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            
            //if distance is still within 5nm then receive packet
            //else deny it
            if (dist2 < 5)
            {
                packRec(); //call packet receive
                Distance.Text = dist2.ToString() + " miles"; //display the dist on GUI
                //Cutter1.Latitude++;   
            }
            else
            {
                packDen(); //call packet deny
                Distance.Text = dist2.ToString() + " miles"; //display the dist on GUI
                //Cutter1.Latitude--;
            }
        }
       
        //stop button functionality
        public void button2_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            //stops all timers and turns of transmission
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

        //receive function for Receiving packets for AIS2
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

        //receive function for Receiving packets for AIS1
        public void packRec2()
        {
            textBox1.AppendText("beacon rcvd\r\n");  
        }

        //deny function for denying packets for AIS2
        public void packDen()
        {
            textBox2.AppendText("beacon denied\r\n");
            //nameLabel2.Text = "unknown";
            //nameLabel1.Text = "unknown";
        }

        //deny function for denying packets for AIS1
        public void packDen2()
        {
            textBox1.AppendText("beacon denied\r\n");
            //nameLabel2.Text = "unknown";
            //nameLabel1.Text = "unknown";
        }

        //3-vessel button: to switch between 2 vessel simulation
        //and three vessel simulation
        private void button3_Click(object sender, EventArgs e)
        {
            //Show 3 vessels
            if (targetsNum == 1)
            {
                this.Width = 1496;
                targetsNum = 2;
                button3.Text = "2-vessel";
                targets1.Text = targetsNum.ToString() + "-3 of 8 Tgts";
                targets2.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            }
            else if (targetsNum == 2)
            {
                this.Width = 890;
                targetsNum = 1;
                button3.Text = "3-vessel";
                targets1.Text = targetsNum.ToString() + "-3 of 8 Tgts";
                targets2.Text = targetsNum.ToString() + "-3 of 8 Tgts";
            }

        }

        //animation for lower progress bar
        public void progressBarAnimation2()
        {
            this.PB2timer.Start();
            textBox2.AppendText("sending beacon...\r\n");
        }

        //timer for delay after the second progress bar is loaded but before it is cleared
        private void timerAfterSend_Tick(object sender, EventArgs e)
        {
            timerAfterSend.Stop();
            progressBar2.Value = 0; //clear lower progress bar
            PB2timer.Stop();
            double dist3 = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            //again checking the distance to ensure that the vessel is still
            //in the 5nm range
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

        //timer for incrementing the second progress bar
        private void PB2timer_Tick(object sender, EventArgs e)
        {
            this.progressBar2.Increment(2);
            if (progressBar2.Value == 100)
            {
                
                timerAfterSend.Start();
            }
        }


        //clock functionatility timer
        private void timer2_Tick(object sender, EventArgs e)
        {
            label25.Text = stopwatch.Elapsed.ToString("hh\\:mm\\:ss"); //display stopwatch
            time1a.Text = DateTime.Now.ToString("HH:mm:ss") + " R";
            time1b.Text = DateTime.Now.ToString("HH:mm:ss") + " I";
            time2a.Text = DateTime.Now.ToString("HH:mm:ss") + " R";
            time2b.Text = DateTime.Now.ToString("HH:mm:ss") + " I";
            date1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            date2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }

        //timer for sending lower progress bar beacon every 15 sec
        private void OtherSendTimer_Tick(object sender, EventArgs e)
        {
            progressBarAnimation2();
        }
        
        //function for lower progress bar animation
        public void StartRAn()
        {
            OtherSendTimer.Start();
        }

//=======================Version 2 functionality===============================
       //function for clicking button 1 on AIS box 1
        private void button1_Click(object sender, EventArgs e)
        {
            //if simulation is running
            //if (transmissionStatus1.Visible == true)
            //{
                button1_click = true;
                FNCstatus.Visible = false;
                if (FNC_pressed == true)
                {
                    textBox1.AppendText("sending nav info query...\r\n");
                    pBanimation1Send();
                    oneSecDelay.Start();
                }
            //}
        }

        //function for FNC button on AIS 1
        private void FNCbutton_Click(object sender, EventArgs e)
        {
            FNC_pressed = true;
            FNCstatus.Visible = true;
        }

        //delay
        private void oneSecDelay_Tick(object sender, EventArgs e)
        {
            oneSecDelay.Stop();
            //oneSecDelayFlag = true;
            textBox1.AppendText("request for nav info sent\r\n");
            textBox1.AppendText("awaiting response...\r\n");

        }

        //accept NAV beacon and send name;
        public void navACK()
        {
            textBox2.AppendText("ACK of nav info sent\r\n");
            textBox1.AppendText("request for nav info accepted\r\n");


        }
        public void navACK2()
        {
            textBox1.AppendText("ACK of nav info sent\r\n");
            textBox2.AppendText("request for nav info accepted\r\n");


        }

        //deny NAV beacon and do not send name;

        public void navDEN()
        {
            textBox2.AppendText("DENIAL of nav info sent\r\n");
            textBox1.AppendText("request for nav info DENIED\r\n");
        }
        public void navDEN2()
        {
            textBox1.AppendText("DENIAL of nav info sent\r\n");
            textBox2.AppendText("request for nav info DENIED\r\n");
        }
        //functionality for FNC button on AIS 2
        private void FNCbutton2_Click(object sender, EventArgs e)
        {
            FNC_pressed2 = true;
            FNCStatus2.Visible = true;
        }

        //functionality for MSG button on AIS 2
        private void message2_Click(object sender, EventArgs e)
        {
            if ( button1_click== true)
            {
                navACK();
                nameLabel1.Text = Cutter2.Name;
            }
        }

        //functionality for CAN button on AIS 2
        private void button61_Click(object sender, EventArgs e)
        {
            if (button1_click == true)
            {
                navDEN();
            }

        }

        //function for clicking button 1 on AIS box 2
        private void button1_2_Click(object sender, EventArgs e)
        {
            //if simulation is running
            //if (transmissionStatus1.Visible == true)
            //{
            button1_2click = true;
            FNCStatus2.Visible = false;
            if (FNC_pressed2 == true)
            {
                textBox2.AppendText("sending nav info query...\r\n");
                progressBarAnimation2();
                oneSecDelay2.Start();
            }
            //}
        }

        private void oneSecDelay2_Tick(object sender, EventArgs e)
        {
            oneSecDelay2.Stop();
            
            textBox2.AppendText("request for nav info sent\r\n");
            textBox2.AppendText("awaiting response...\r\n");
        }
        //functionality for MSG button on AIS 1
        private void button15_Click(object sender, EventArgs e)
        {
            
                navACK2();
                nameLabel2.Text = Cutter1.Name;
            
        }

        //functionality for CAN button on AIS 2
        private void button16_Click(object sender, EventArgs e)
        {
            navDEN2();
        }

        private void TwoVesselSimulation_Load(object sender, EventArgs e)
        {

        }

        //function to get the vessels locale
        public void trackline(int course, double speed, DateTime time)
        {
            
        }





//=========================================end of code=========================
    } // end of partial class
} // AIS simulation
