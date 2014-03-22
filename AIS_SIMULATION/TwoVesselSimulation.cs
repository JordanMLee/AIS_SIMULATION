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
//
//want to move two labels around a form and add a database with pseudonyms
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
        //each vessel is always sending beacons to other vessels containg its
        //pseudonym, course, speed and lat/long.
        //a request can be made for further information such as: nav info
        //when a request for nav info is made, then the requesting vessel will 
        //either receive the name of the other vessel or it will be denied.

        Stopwatch stopwatch = new Stopwatch();
        Actor actor = new Actor(); //new instance of class Actor, see Actor.cs
        CGCutter Cutter1 = new CGCutter();  //new instance of CGCutter
        CGCutter Cutter2 = new CGCutter();  //new instance of CGCutter

        public static bool knownName1 = false; //flag for accepting nav request from vessel 2
        public static bool knownName2 = false; //flag for accepting nav request from vessel 1

        public int targetsNum = 1; //number of other targets a vessel can see 
                                   //in simulation
        
        public static bool FNC_pressed = false; //flag for FNC button on AIS1(right side)
        public static bool FNC_pressed2 = false;//flag for FNC button on AIS2(right side)
        public static bool oneSecDelayFlag = false;//flag for Delay function
        public static bool button1_click = false;//flag for clicking button1 on AIS1
        public static bool button1_2click = false;//flag for clicking button1 on AIS2


        //for animation of nav info sent from vessel 2 to vessel 1
        public static bool navSending = false; //flag for changing beacon rcvd to nav info request rcvd on AIS 2
        public static bool navSending2 = false; //flag for changing beacon rcvd to nav info request rcvd on AIS 1

        public static bool navAckSending = false; //flag for queueing animation for sending nav info on AIS 2
        public static bool navAckSending2 = false; //flag for queueing animation for sending nav infor on AIS 1

        //Constructor
        public TwoVesselSimulation()
        {
       
            InitializeComponent(); 
            timer2.Enabled = true;  //timer for both AIS clocks  
            //code for fixing size of GUI
            this.Width = 890;
            this.MaximizeBox = false; //disabled default maximize button
            this.MinimizeBox = false; //disabled default minimize button
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            textBox11.Hide();
            label77.Hide();
            label78.Hide();
            label79.Hide();
            label80.Hide();
            label81.Hide();
            label82.Hide();
            label83.Hide();
            label84.Hide();
            label85.Hide();
            label86.Hide();
            label87.Hide();
            label88.Hide();
            label89.Hide();
            label90.Hide();
            label91.Hide();
            label92.Hide();
            label93.Hide();
            label94.Hide();
            label95.Hide();
            label96.Hide();
            label97.Hide();
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
            Cutter1.vesselCategory = "CG";
            
            Cutter1.Latitude = Convert.ToDouble(Lat1Box.Text); 
            Cutter1.Longitude = Convert.ToDouble(Long1Box.Text);
            Cutter1.Speed = Convert.ToDouble(SpeedBox1.Text);
            Cutter1.Course = Convert.ToInt16(Course1Box.Text);

           
            
            Cutter2.Name = "Yeaton";
            Cutter2.Pseudonym = "iee160"; // should be displayed on AIS box 1
            Cutter2.MMSI = 456;

            
            Cutter2.Latitude = Convert.ToDouble(textBox5.Text);
            Cutter2.Longitude = Convert.ToDouble(textBox6.Text);
            Cutter2.Speed = Convert.ToDouble(SpeedBox2.Text);
            Cutter2.Course = Convert.ToInt16(CourseBox2.Text);
            Cutter2.vesselCategory = "CV";
          
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
                //StartRAn(); // start animation for second vessel beacon sending
                
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
            StartRAn(); // start animation for second vessel beacon sending
        }

        //timer for when to send beacons (every 15secs)
        public void Sendtimer_Tick(object sender, EventArgs e)
        {
            pBanimation1Send(); //function to run progressbar animation once
        }

        //function to run progessbar animiation once
        public void pBanimation1Send()
        {
            if (navAckSending2 == false)
            {
                textBox1.AppendText("sending beacon...\r\n");
            }
            else if (navAckSending2 == true)
            {
                textBox1.AppendText("sending nav information...\r\n");
                navAckSending2 = false;
            }
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
                packRec(navSending); //call packet receive
                Distance.Text = dist2.ToString() + " miles"; //display the dist on GUI
                //Cutter1.Latitude++;   
            }
            else
            {
                packFail(); //call packet deny
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
            TestTimer.Stop();


        }
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //receive function for Receiving packets for AIS2
        public void packRec(bool _navSending)
        {
            _navSending = navSending;
            if (_navSending == false)
            {
                textBox2.AppendText("beacon rcvd\r\n");
            }
            else if (_navSending == true)
            {
                textBox2.AppendText("nav info request rcvd\r\n");
                navSending = false;
            }
            if (knownName2 == false)
            {
                nameLabel2.Text = Cutter1.Pseudonym;
            }
            else if(knownName2 == true)
            {
                nameLabel2.Text = Cutter1.Name;
            }
            
            cseLbl2.Text = Cutter1.Course.ToString() + "°";
            speedLbl2.Text = " " + Cutter1.Speed.ToString();
            
            //StartRAn(); // start animation for second vessel beacon sending
        }

        //receive function for Receiving packets for AIS1
        public void packRec2()
        {
            if (navSending2 == false)
            {
                textBox1.AppendText("beacon rcvd\r\n");
            }
            else if (navSending2 == true)
            {
                textBox1.AppendText("nav info request rcvd\r\n");
                navSending2 = false;
            }
            if (knownName1 == false)
            {
                nameLabel1.Text = Cutter2.Pseudonym;
            }
            else if (knownName1 == true)
            {
                nameLabel1.Text = Cutter2.Name;
            }
            
            cseLbl1.Text = Cutter2.Course.ToString() + "°";
            speedLbl1.Text = " " + Cutter2.Speed.ToString();
        }

        //deny function for denying packets for AIS2
        public void packDen()
        {
            textBox2.AppendText("beacon denied\r\n");
            //nameLabel2.Text = "unknown";
            //nameLabel1.Text = "unknown";
        }

        public void packFail()
        {
            textBox2.AppendText("beacon failed..out of range\r\n");
           
        }
        //deny function for denying packets for AIS1
        public void packDen2()
        {
            textBox1.AppendText("beacon denied\r\n");
           
        }

        public void packFail2()
        {
            textBox1.AppendText("beacon failed..out of range\r\n");
            
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
            if (navAckSending == false)
            {
                textBox2.AppendText("sending beacon...\r\n");
            }
            else if (navAckSending == true)
            {
                textBox2.AppendText("sending nav information...\r\n");
                navAckSending = false;
            }


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
                packFail2();
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
                    navSending = true;
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
            //navAckSending = true;

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
                //nameLabel1.Text = Cutter2.Name;
                knownName1 = true;
                navAckSending = true;
                progressBarAnimation2();
                Delaytimerfornav1.Start();
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
                navSending2 = true;
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
                //nameLabel2.Text = Cutter1.Name;
                knownName2 = true;
                navAckSending2 = true;

                
                pBanimation1Send();
                Delaytimerfornav2.Start();
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
        public double[] trackline(int course, double speed, double init_lat, double init_long)
        {
            double R = 3443.9; //radius of the earth in nautical miles
            double _course = course; //
            double brng = actor.deg2rad(_course); 
            double time = Convert.ToDouble(stopwatch.ElapsedMilliseconds);
            time = time / 1000; //seconds
            time = time / 60; //minutes
            time = time / 60; //hours
            double distnce = speed * time; //in nautical miles

            double lat1 = actor.deg2rad(init_lat);  //Current lat point converted to radians
            double lon1 = actor.deg2rad(init_long);//Current long point converted to radians

            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(distnce / R) + Math.Cos(lat1) * Math.Sin(distnce / R) * Math.Cos(brng));

            double lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(distnce / R) * Math.Cos(lat1), Math.Cos(distnce / R) - Math.Sin(lat1) * Math.Sin(lat2));

            lat2 = actor.rad2deg(lat2);
            lon2 = actor.rad2deg(lon2);
            lat2 = Math.Round(lat2, 4);
            lon2 = Math.Round(lon2, 4);
            double[] coord = new double[]{lat2,lon2};


            return coord;
             
          
            
        }

        private void Delaytimerfornav1_Tick(object sender, EventArgs e)
        {
            Delaytimerfornav1.Stop();
            //navACK();
            nameLabel1.Text = Cutter2.Name;
            navAckSending = false;
        }

        private void Delaytimerfornav2_Tick(object sender, EventArgs e)
        {
            Delaytimerfornav2.Stop();
            //navACK();
            nameLabel2.Text = Cutter1.Name;
            navAckSending2 = false;
        }

        private void SpeedBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TestTimer_Tick(object sender, EventArgs e)
        {
         
            
            double[] PosVes1 = trackline(Cutter1.Course, Cutter1.Speed, Cutter1.Latitude,Cutter1.Longitude);
            
            double[] PosVes2 = trackline(Cutter2.Course, Cutter2.Speed, Cutter2.Latitude, Cutter2.Longitude);
            label69.Text = PosVes1[0].ToString();
            label70.Text = PosVes1[1].ToString();
            label73.Text = PosVes2[0].ToString();
            label74.Text = PosVes2[1].ToString();
            
            Cutter1.Latitude = PosVes1[0];
            Cutter1.Longitude = PosVes1[1];
            Cutter2.Latitude = PosVes2[0];
            Cutter2.Longitude = PosVes2[1];




        }

        private void button49_Click(object sender, EventArgs e)
        {
            TestTimer.Start();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is a simulation of the our\nnew AIS protocol. It displays several use\ncase scenerios and can be used to see\nhow our protocol will interact in real time.", "About");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To start the simulation press the \"START\" button.\n\nTo stop the simulation in order to make changes to\nthe initial vessel settings press the \"STOP\" button.\n\nTo start the movement of the vessels,\npress \"2\" button on AIS #2.\n\nTo send a navigation beacon press \"FNC\", followed\nby \"1\" on either AIS box.\n\nTo accept a navigational message request, press  \"MSG\".\n\nTo deny a navigation request, press \"CAN\".  ", "Help");
        }

        private void coastGuardVesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = AIS_SIMULATION.Properties.Resources.cg_ais;
            label1.Text = "COAST GUARD VESSEL";
            Cutter1.vesselCategory = "CG";

        }

        private void civilianVesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = AIS_SIMULATION.Properties.Resources.tanker_ais;
            label1.Text = "CIVILIAN VESSEL";
            Cutter1.vesselCategory = "CV";
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox3.Hide();
            transDis1.Hide();
            nameLabel1.Hide();
            speedLbl1.Hide();
            cseLbl1.Hide();
            targets1.Hide();
            date1.Hide();
            time1a.Hide();
            time1b.Hide();

            textBox11.Show();
            label77.Show();
            label78.Show();
            label79.Show();
            label80.Show();
            label81.Show();
            label82.Show();
            label83.Show();
            label84.Show();
            label85.Show();
            label86.Show();
            label87.Show();
            label88.Show();
            label89.Show();
            label90.Show();
            label91.Show();
            label92.Show();
            label93.Show();
            label94.Show();
            label95.Show();
            label96.Show();
            label97.Show();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            
                            
        }

        private void civilianVesselToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources.tanker_ais;
            label2.Text = "CIVILIAN VESSEL";
            Cutter2.vesselCategory = "CV";
        }

        private void nefariousVesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources.Chinese;
            label2.Text = "NEFARIOUS VESSEL";
            Cutter2.vesselCategory = "CV";
        }

        private void shoreSideUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources._300px_BostonCoastGuard;
            label2.Text = "SHORE SIDE UNIT";

        }

        private void label78_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox11.Hide();
            label77.Hide();
            label78.Hide();
            label79.Hide();
            label80.Hide();
            label81.Hide();
            label82.Hide();
            label83.Hide();
            label84.Hide();
            label85.Hide();
            label86.Hide();
            label87.Hide();
            label88.Hide();
            label89.Hide();
            label90.Hide();
            label91.Hide();
            label92.Hide();
            label93.Hide();
            label94.Hide();
            label95.Hide();
            label96.Hide();
            label97.Hide();


            textBox3.Show();
            transDis1.Show();
            nameLabel1.Show();
            speedLbl1.Show();
            cseLbl1.Show();
            targets1.Show();
            date1.Show();
            time1a.Show();
            time1b.Show();
        }





//=========================================end of code=========================
    } // end of partial class
} // AIS simulation
