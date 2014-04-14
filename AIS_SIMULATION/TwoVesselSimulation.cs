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
using System.Data.OleDb; //used for database

namespace AIS_SIMULATION
{
    public partial class TwoVesselSimulation : Form
    {
        //each vessel is always sending beacons to other vessels containg its
        //pseudonym, course, speed and lat/long.
        //a request can be made for further information such as: nav info
        //when a request for nav info is made, then the requesting vessel will 
        //either receive the name of the other vessel or it will be denied.

        //========database===============
        public string connString;
        public string query;
        public string query2;
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        //================================
        Stopwatch stopwatch = new Stopwatch(); //stopwatch for GUI, seen on GUI at startup, keeps running time
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

        public static bool fvSending = false; //flag for changing beacon rvcd to FV vessel info rvcd on AIS2
        public static bool fvSending2 = false;//flag for changing beacon rvcd to FV vessel infor rcvd on AIS1

        public static bool fvAckSending = false; //flag for queueing animation for sending FV info on AIS2
        public static bool fvAckSending2 = false; //flag for queueing animation for sending FV info on AIS1

        public bool navBtn1 = false; //flag to check if NAV button has been pressed on AIS1
        public bool navBtn2 = false; //flag to check if NAV button has been pressed on AIS2
        public bool nefariousAct = false; //flag for nefarious vessel operations
        public bool isChecked = false;
        public bool failToSendNav = false;
        public bool forceNav = false;//flag for cg vessel countering Denial of Nav info
        public bool forceFVI = false;

        public bool broadcastUnsigedInfo = false;
        public bool spoofCGCertificate = false;

        public bool alterAISInfo = false;
        //Constructor
        public TwoVesselSimulation()
        {
            InitializeComponent(); 
            //==========database=================
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PseudonymsDB1.accdb";
            query = "SELECT * FROM PseudonymTable";
            

            connection = new OleDbConnection(connString);
            connection.Open();

            command = new OleDbCommand(query, connection);
          

            reader = command.ExecuteReader();
           
            reader.Read();
          

           
            
            //===================================
            timer2.Enabled = true;  //timer for both AIS clocks  
            //code for fixing size of GUI
            this.Width = 890;
            this.MaximizeBox = false; //disabled default maximize button
            this.MinimizeBox = false; //disabled default minimize button
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            testlabel.Show();
            //AIS1 page1
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
            //panel4.Enabled = false;

            button3.Enabled = false; //3-vessel button
            button2.Enabled = false; //stop button
            button44.Enabled = false; //reset button
            panel4.Enabled = false;
            
            //AIS2 page2
            label114.Hide();
            label115.Hide();
            label116.Hide();
            label117.Hide();
            label118.Hide();
            label119.Hide();
            label120.Hide();
            label121.Hide();
            label122.Hide();
            label123.Hide();
            label124.Hide();
            label126.Hide();
            label127.Hide();
            label128.Hide();
            label129.Hide();
            label130.Hide();
            label131.Hide();
            label132.Hide();
            label133.Hide();

            //AIS2 page 1
            label152.Hide();
            label153.Hide();
            label154.Hide();
            label155.Hide();
            label156.Hide();
            label157.Hide();
            label158.Hide();
            label159.Hide();
            label160.Hide();
            label161.Hide();
            label162.Hide();
            label163.Hide();
            label164.Hide();
            label165.Hide();
            label166.Hide();
            label167.Hide();
            label168.Hide();
            label169.Hide();
            label170.Hide();
            label171.Hide();
            label172.Hide();

            //AIS1 page2
            label125.Hide();
            label134.Hide();
            label135.Hide();
            label136.Hide();
            label137.Hide();
            label138.Hide();
            label139.Hide();
            label140.Hide();
            label141.Hide();
            label142.Hide();
            label143.Hide();
            label144.Hide();
            label145.Hide();
            label146.Hide();
            label147.Hide();
            label148.Hide();
            label149.Hide();
            label150.Hide();
            label151.Hide();


            testlabel.Hide();
        }
        
        //function for pressing the start button on the simulation
        public void startButton_Click(object sender, EventArgs e)
        {
            //panel4.Enabled = true;
            PseudonymTimer.Start();
            fileToolStripMenuItem.Enabled = false;
            aboutToolStripMenuItem.Enabled = false;
            helpToolStripMenuItem.Enabled = false;
            startButton.Enabled = false;
            button2.Enabled = true;
            //lat and long become readonly on left
            Lat1Box.ReadOnly = true;
            Long1Box.ReadOnly = true;
            Course1Box.ReadOnly = true;
            SpeedBox1.ReadOnly = true;
            //lat and long become read only on right
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            CourseBox2.ReadOnly = true;
            SpeedBox2.ReadOnly = true;

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
            SimulationStartDelay.Start(); //starts the movement of the vessels
            //==============Initialization of the two vessels==================

            
            Cutter1.Name = "Bertholf";
            //Cutter1.Pseudonym = "conta1a";
            Cutter1.Pseudonym = reader[1].ToString();

            Cutter1.MMSI = 234;
            Cutter1.vesselCategory = "CG";
            Cutter1.IMO = 9638527;
            Cutter1.CSgn = "WMSL750";
            Cutter1.Dest = "New London";
            Cutter1.ETA = "unk";

            Cutter1.Latitude = Convert.ToDouble(Lat1Box.Text); 
            Cutter1.Longitude = Convert.ToDouble(Long1Box.Text);
            Cutter1.Speed = Convert.ToDouble(SpeedBox1.Text);
            Cutter1.Course = Convert.ToInt16(Course1Box.Text);

            Cutter1.length = 420;
            Cutter1.tonnage = 4500;
            Cutter1.cargoType = "unk";
            

            Cutter2.Name = "Yeaton";
            //Cutter2.Pseudonym = "conta2a"; // should be displayed on AIS box 1
            
            Cutter2.Pseudonym = reader[2].ToString();
     
            Cutter2.MMSI = 456;
            Cutter2.IMO = 1472583;
            Cutter2.CSgn = "WCV2342";
            Cutter2.Dest = "New York";
            Cutter2.ETA = "unk";

            Cutter2.length = 1300;
            Cutter2.tonnage = 15000;
            Cutter2.cargoType = "bk";

            
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
            distance = Math.Round(distance, 4);
            Distance.Text = distance.ToString() + " miles";//display the dist on the GUI

            //if distance is less than 5 miles than send beacons
            //if (distance < 5)
            //{
                StartSim(sender, e);
                //StartRAn(); // start animation for second vessel beacon sending
                
            //}//end if 
        }//end start_button

        //function to increment the top progress bar all the way
        public void StartSim(object sender, EventArgs e)
        {
            //Send first packet
            if (beaconCheckBox.Checked == true)
            {
                textBox1.AppendText("sending beacon...\r\n");
            }
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
            if (navAckSending2 == false && fvAckSending2 == false)
            {
                if (beaconCheckBox.Checked == true)
                {
                    textBox1.AppendText("sending beacon...\r\n");
                }
            }
            else if (navAckSending2 == true && fvAckSending2 == false)
            {
                textBox1.AppendText("sending nav information...\r\n");
                navAckSending2 = false;
                
            }
            else if (fvAckSending2 == true && navAckSending2 == false)
            {
                textBox1.AppendText("sending full vessel information...\r\n");
                fvAckSending2 = false;
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
                dist2 = Math.Round(dist2, 4);
                Distance.Text = dist2.ToString() + " miles"; //display the dist on GUI
                //Cutter1.Latitude++;   
            }
            else
            {
                packFail(); //call packet deny
                dist2 = Math.Round(dist2, 4);
                Distance.Text = dist2.ToString() + " miles"; //display the dist on GUI
                //Cutter1.Latitude--;
            }
        }
       
        //stop button functionality
        public void button2_Click(object sender, EventArgs e)
        {
            radioButton4.Checked = false;
            panel4.Enabled = false;
            PseudonymTimer.Stop();
            fileToolStripMenuItem.Enabled = true;
            aboutToolStripMenuItem.Enabled = true;
            helpToolStripMenuItem.Enabled = true;
            button2.Enabled = false;
            startButton.Enabled = true;
            button44.Enabled = true;
            Lat1Box.ReadOnly = false;
            Long1Box.ReadOnly = false;
            Course1Box.ReadOnly = false;
            SpeedBox1.ReadOnly = false;

            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            CourseBox2.ReadOnly = false;
            SpeedBox2.ReadOnly = false;

            
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

            Lat1Box.Text = label69.Text;
            Long1Box.Text = label70.Text;
            textBox5.Text = label73.Text;
            textBox6.Text = label74.Text;
            



        }
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //receive function for Receiving packets for AIS2
        public void packRec(bool _navSending)
        {
            _navSending = navSending;
            if (_navSending == false && fvSending == false)
            {
                if (beaconCheckBox.Checked == true)
                textBox2.AppendText("beacon rcvd\r\n");
            }
            else if (_navSending == true && fvAckSending == false)
            {
                textBox2.AppendText("nav info request rcvd\r\n");
                navSending = false;
            }
            else if (_navSending == false && fvSending == true)
            {
                textBox2.AppendText("full vessel info request rvd\r\nverifying authenticity of certificate");
                
                fvSending = false;//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                WaitAnimation2.Start();
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
           
                //insert code for adding values to textboxes in new part of scren

                if (Convert.ToDouble(label69.Text) >= 0)
                {
                    label162.Text = "N " + label69.Text;
                }
                else if (Convert.ToDouble(label69.Text) < 0)
                {
                    label162.Text = "S " + label69.Text;
                }
                if (Convert.ToDouble(label70.Text) >= 0)
                {
                    label159.Text = "E " + label70.Text;
                }
                else if (Convert.ToDouble(label70.Text) < 0)
                {
                    label159.Text = "W " + (Convert.ToDouble(label70.Text) * (-1)).ToString();
                }


                label163.Text = Cutter1.MMSI.ToString();
                //label165.Text = Cutter1.Name;
                label157.Text = Cutter1.Course.ToString() + "°";
                label155.Text = Cutter1.Speed.ToString() + " Kn";


            
           

           
            
            //StartRAn(); // start animation for second vessel beacon sending
        }

        //receive function for Receiving packets for AIS1
        public void packRec2()
        {
            if (navSending2 == false && fvSending2 == false)
            {
                if (broadcastUnsigedInfo == false)
                {
                    if (beaconCheckBox.Checked == true)
                    {
                        textBox1.AppendText("beacon rcvd\r\n");
                    }
                }
                else if (broadcastUnsigedInfo == true)
                {
                    textBox1.AppendText("WARNING! unsigned beacon rcvd");
                    
                }
            }
            else if (navSending2 == true)
            {
                textBox1.AppendText("nav info request rcvd\r\n");
                navSending2 = false;
            }
            else if (fvSending2 == true)
            {
                fvSending2 = false;
                textBox1.AppendText("full vessel info request rvd\r\nverifying authenticity of certificate");
                
                
                 WaitAnimation.Start();
                

                //textBox1.AppendText("full vessel info request rcvd\r\n");
               // textBox1.AppendText("****authentic US Coast Guard signature****\r\nSending full vessel info to requesting authority\r\n");
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
            if (Convert.ToDouble(label73.Text) >= 0)
            {
                label81.Text = "N " + label73.Text;
            }
            else if (Convert.ToDouble(label73.Text) < 0)
            {
                label81.Text = "S " + label73.Text;
            }
            if (Convert.ToDouble(label74.Text) >= 0)
            {
                label86.Text = "E " + label74.Text;
            }
            else if (Convert.ToDouble(label74.Text) < 0)
            {
                label86.Text = "W " + (Convert.ToDouble(label74.Text) * (-1)).ToString();
            }
            //label86.Text = "E " + label74.Text;
            label94.Text = Cutter2.Course.ToString() + "°";
            label95.Text = Cutter2.Speed.ToString() + " Kn";
            
            
            


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
            cseLbl1.Text = "---°";
            speedLbl1.Text = "---.--";
            label81.Text = "------";
            label86.Text = "------";
            label94.Text = "---";
            label95.Text = "---";
            
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
            if (navAckSending == false && fvSending2 == false)
            {
                if (beaconCheckBox.Checked == true)
                textBox2.AppendText("sending beacon...\r\n");
            }
            else if (navAckSending == true && fvSending2 == false)
            {
                textBox2.AppendText("sending nav information...\r\n");
                navAckSending = false;
            }

            if (fvSending2 == true && navAckSending == false)
            {
            

             
               




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
                dist3 = Math.Round(dist3, 4);
                Distance.Text = dist3.ToString() + " miles";
                //Cutter1.Latitude++;

            }
            else
            {
                packFail2();
                dist3 = Math.Round(dist3, 4);
                Distance.Text = dist3.ToString() + " miles";
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

            double distance = actor.getDistance(Cutter1.Latitude, Cutter1.Longitude, Cutter2.Latitude, Cutter2.Longitude);
            distance = Math.Round(distance, 4);
            Distance.Text = distance.ToString() + " miles";//display the dist on the GUI
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

        public void navNACK()
        {
            textBox2.AppendText("Prevented nav info transmission\r\n");
            textBox1.AppendText("request for nav info DENIED\r\n");
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
                if (failToSendNav == false)
                {
                    navACK();
                    
                    knownName1 = true;
                    navAckSending = true;
                    progressBarAnimation2();
                    Delaytimerfornav1.Start();
                }
                else if (failToSendNav == true)
                {
                    //failToSendNav = false;
                    navNACK();
                    
                    knownName1 = false;
                    navAckSending = false;
                    progressBarAnimation2();
                    //Delaytimerfornav1.Start();
                    if (Cutter1.vesselCategory == "CG")
                    {

                        textBox1.AppendText("Target DENIED Nav request\r\n");
                        textBox1.AppendText("Press \"9\" to retrieve Nav Info\r\n");
                        forceNav = true;
                    }


                }

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

            //if(fvSending2 ==true)
            //{

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
            label91.Text = Cutter2.Name;
            label90.Text = Cutter2.MMSI.ToString(); ;
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
        //full vessel info request to AIS one from AIS 2
        private void button49_Click(object sender, EventArgs e)
        {
            if (FNC_pressed2 == true)
            {
                fvSending2 = true;
                FNCStatus2.Hide();
                textBox2.AppendText("sending full vessel info query...\r\n");
                progressBarAnimation2();
                //oneSecDelay2.Start();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is a simulation of the our\nnew AIS protocol. It displays several use\ncase scenerios and can be used to see\nhow our protocol will interact in real time.", "About");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To start the simulation press the \"START\" button.\n\nTo stop the simulation in order to make changes to\nthe initial vessel settings press the \"STOP\" button.\n\nTo start the movement of the vessels,\npress \"2\" button on AIS #2.\n\nTo send a navigation beacon press \"FNC\", followed\nby \"1\" on either AIS box.\n\nTo accept a navigational message request, press  \"MSG\".\n\nTo deny a navigation request, press \"CAN\".\n\nIn order to make changes to the simulation\nyou must stop the simulation first.  ", "Help");
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

        
        //AIS 1 nav button pressed
        private void button23_Click(object sender, EventArgs e)
        {
            navBtn1 = true;
            label98.Hide();
            label99.Hide();
            label100.Hide();
            label101.Hide();
            label102.Hide();
            label103.Hide();
            label104.Hide();
            label105.Hide();

            //textBox3.Hide();
            transmissionStatus1.Hide();
            nameLabel1.Hide();
            speedLbl1.Hide();
            cseLbl1.Hide();
            targets1.Hide();
            date1.Hide();
            time1a.Hide();
            time1b.Hide();

            //textBox11.Show();
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
            panel4.Enabled = false;
            nefariousAct = false;
        }

        private void nefariousVesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources.Chinese;
            label2.Text = "NEFARIOUS VESSEL";
            Cutter2.vesselCategory = "CV";
            panel4.Enabled = true;
            nefariousAct = true;
            
        }

        private void shoreSideUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources._300px_BostonCoastGuard;
            label2.Text = "SHORE SIDE UNIT";
            panel4.Enabled = false;
            nefariousAct = false;
        }

        private void label78_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            navBtn1 = false;
            label98.Show();
            label99.Show();
            label100.Show();
            label101.Show();
            label102.Show();
            label103.Show();
            label104.Show();
            label105.Show();
            //textBox11.Hide();
            //AIS1page1
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
            //AIS1 page2
            label125.Hide();
            label134.Hide();
            label135.Hide();
            label136.Hide();
            label137.Hide();
            label138.Hide();
            label139.Hide();
            label140.Hide();
            label141.Hide();
            label142.Hide();
            label143.Hide();
            label144.Hide();
            label145.Hide();
            label146.Hide();
            label147.Hide();
            label148.Hide();
            label149.Hide();
            label150.Hide();
            label151.Hide();


            textBox3.Show();
            transmissionStatus1.Show(); //label for showing Class A transmission
            

            nameLabel1.Show();
            speedLbl1.Show();
            cseLbl1.Show();
            targets1.Show();
            date1.Show();
            time1a.Show();
            time1b.Show();
        }

        private void pleasureCraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = AIS_SIMULATION.Properties.Resources.pleasureCraft;
            label2.Text = "PLEASURE CRAFT";
            panel4.Enabled = false ;
            nefariousAct = false;
        }

        private void pleasureCraftToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = AIS_SIMULATION.Properties.Resources.pleasureCraft;
            label1.Text = "PLEASURE CRAFT";
        }

        private void SimulationStartDelay_Tick(object sender, EventArgs e)
        {
            TestTimer.Start();
            SimulationStartDelay.Stop();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {
            button44.Enabled = false;
            button2.Enabled = false;
            //InitializeComponent();
            //enter reset button code
            this.Hide();
            TwoVesselSimulation newForm = new TwoVesselSimulation();
            newForm.Show();
            

            
        }

        private void WaitAnimation_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                textBox1.AppendText(".");
                Thread.Sleep(100);
            }

            if (spoofCGCertificate == false)
            {
                textBox1.AppendText("\u221A\r\n");
                WaitAnimation.Stop();
                textBox1.AppendText("****authentic US Coast Guard signature****\r\nSending full vessel info to requesting authority\r\n");
                pBanimation1Send();
                fvAckSending2 = true;
            //data field
            label163.Text = Cutter1.MMSI.ToString();
            label165.Text = Cutter1.Name;
            label116.Text = Cutter1.Name;
            label126.Text = Cutter1.length.ToString();
            label130.Text = Cutter1.tonnage.ToString();
            label127.Text = Cutter1.vesselCategory;
            label129.Text = Cutter1.cargoType;
            label128.Text = Cutter1.operatingMission;
            label131.Text = Cutter1.IMO.ToString();
            label132.Text = Cutter1.Dest;
            label133.Text = Cutter1.ETA;






            }
            else if (spoofCGCertificate == true)
            {
                textBox1.AppendText("X\r\n");
                WaitAnimation.Stop();
                textBox1.AppendText("                     ****WARNING****\r\n****unauthentic US Coast Guard signature****\r\n");
                textBox1.AppendText("Prevented FVI transmission\r\n");
                fvAckSending2 = false;

               
            }


     
        }

        private void label121_Click(object sender, EventArgs e)
        {

        }

        //ais2 rightbutton
        private void button64_Click(object sender, EventArgs e)
        {
            if (navBtn2 == true)
            {
                //ais2 page2
                label114.Show();
                label115.Show();
                label116.Show();
                label117.Show();
                label118.Show();
                label119.Show();
                label120.Show();
                label121.Show();
                label122.Show();
                label123.Show();
                label124.Show();
                label126.Show();
                label127.Show();
                label128.Show();
                label129.Show();
                label130.Show();
                label131.Show();
                label132.Show();
                label133.Show();
                //AIS2 page 1
                label152.Hide();
                label153.Hide();
                label154.Hide();
                label155.Hide();
                label156.Hide();
                label157.Hide();
                label158.Hide();
                label159.Hide();
                label160.Hide();
                label161.Hide();
                label162.Hide();
                label163.Hide();
                label164.Hide();
                label165.Hide();
                label166.Hide();
                label167.Hide();
                label168.Hide();
                label169.Hide();
                label170.Hide();
                label171.Hide();
                label172.Hide();
            }


        }

        private void label130_Click(object sender, EventArgs e)
        {

        }

        private void transDis1_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (navBtn1 == true)
            {
                //AIS1 page 1
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

                //AIS1 page 2
                label125.Show();
                label134.Show();
                label135.Show();
                label136.Show();
                label137.Show();
                label138.Show();
                label139.Show();
                label140.Show();
                label141.Show();
                label142.Show();
                label143.Show();
                label144.Show();
                label145.Show();
                label146.Show();
                label147.Show();
                label148.Show();
                label149.Show();
                label150.Show();
                label151.Show();

            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (navBtn1 == true)
            {
                //AIS1 page 1
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

                //AIS1 page 2
                label125.Hide();
                label134.Hide();
                label135.Hide();
                label136.Hide();
                label137.Hide();
                label138.Hide();
                label139.Hide();
                label140.Hide();
                label141.Hide();
                label142.Hide();
                label143.Hide();
                label144.Hide();
                label145.Hide();
                label146.Hide();
                label147.Hide();
                label148.Hide();
                label149.Hide();
                label150.Hide();
                label151.Hide();
            }
        }

        //AIS2 navbutton
        private void button46_Click(object sender, EventArgs e)
        {
            navBtn2 = true;
            //AIS2 page 1
            label152.Show();
            label153.Show();
            label154.Show();
            label155.Show();
            label156.Show();
            label157.Show();
            label158.Show();
            label159.Show();
            label160.Show();
            label161.Show();
            label162.Show();
            label163.Show();
            label164.Show();
            label165.Show();
            label166.Show();
            label167.Show();
            label168.Show();
            label169.Show();
            label170.Show();
            label171.Show();
            label172.Show();
            
            //AIS2 dpage
            transmissionStatus2.Hide();
            label106.Hide();
            transDis2.Hide();
            nameLabel2.Hide();
            label107.Hide();
            speedLbl2.Hide();
            cseLbl2.Hide();
            label108.Hide();
            label109.Hide();
            label110.Hide();
            label111.Hide();
            label112.Hide();
            label113.Hide();
            targets2.Hide();
            date2.Hide();
            time2a.Hide();
            time2b.Hide();




        }

        //AIS2 escbutton
        private void button47_Click(object sender, EventArgs e)
        {
            navBtn2 = false;

            //AIS2 dpage
            transmissionStatus2.Show();
            label106.Show();
            
            nameLabel2.Show();
            label107.Show();
            speedLbl2.Show();
            cseLbl2.Show();
            label108.Show();
            label109.Show();
            label110.Show();
            label111.Show();
            label112.Show();
            label113.Show();
            targets2.Show();
            date2.Show();
            time2a.Show();
            time2b.Show();

            //AIS2 page 1
            label152.Hide();
            label153.Hide();
            label154.Hide();
            label155.Hide();
            label156.Hide();
            label157.Hide();
            label158.Hide();
            label159.Hide();
            label160.Hide();
            label161.Hide();
            label162.Hide();
            label163.Hide();
            label164.Hide();
            label165.Hide();
            label166.Hide();
            label167.Hide();
            label168.Hide();
            label169.Hide();
            label170.Hide();
            label171.Hide();
            label172.Hide();

            //AIS2 page2
            label114.Hide();
            label115.Hide();
            label116.Hide();
            label117.Hide();
            label118.Hide();
            label119.Hide();
            label120.Hide();
            label121.Hide();
            label122.Hide();
            label123.Hide();
            label124.Hide();
            label126.Hide();
            label127.Hide();
            label128.Hide();
            label129.Hide();
            label130.Hide();
            label131.Hide();
            label132.Hide();
            label133.Hide();





        }


        private void button45_Click(object sender, EventArgs e)
        {
            if (navBtn2 == true)
            {

                //ais2 page2
                label114.Hide();
                label115.Hide();
                label116.Hide();
                label117.Hide();
                label118.Hide();
                label119.Hide();
                label120.Hide();
                label121.Hide();
                label122.Hide();
                label123.Hide();
                label124.Hide();
                label126.Hide();
                label127.Hide();
                label128.Hide();
                label129.Hide();
                label130.Hide();
                label131.Hide();
                label132.Hide();
                label133.Hide();
                //AIS2 page 1
                label152.Show();
                label153.Show();
                label154.Show();
                label155.Show();
                label156.Show();
                label157.Show();
                label158.Show();
                label159.Show();
                label160.Show();
                label161.Show();
                label162.Show();
                label163.Show();
                label164.Show();
                label165.Show();
                label166.Show();
                label167.Show();
                label168.Show();
                label169.Show();
                label170.Show();
                label171.Show();
                label172.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (FNC_pressed == true)
            {
                fvSending = true;
                FNCstatus.Hide();
                textBox1.AppendText("sending full vessel info query...\r\n");
                pBanimation1Send();
                //oneSecDelay.Start();
            }

        }

        private void beaconCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WaitAnimation2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                textBox2.AppendText(".");
                Thread.Sleep(100);
            }
            textBox2.AppendText("\u221A\r\n");
            WaitAnimation2.Stop();
            textBox2.AppendText("****authentic US Coast Guard signature****\r\nSending full vessel info to requesting authority\r\n");
          
          

            progressBarAnimation2();
            fvAckSending = true;


            //data field

            if (alterAISInfo == false)
            {
                label91.Text = Cutter2.MMSI.ToString();
                label90.Text = Cutter2.Name;
                label149.Text = Cutter2.Name;
                label141.Text = Cutter2.length.ToString();
                label136.Text = Cutter2.tonnage.ToString();
                label125.Text = Cutter2.vesselCategory;
                label139.Text = Cutter2.cargoType;
                label134.Text = Cutter2.operatingMission;
                label140.Text = Cutter2.IMO.ToString();
                label137.Text = Cutter2.Dest;
                label135.Text = Cutter2.ETA;
            }
            else if (alterAISInfo == true)
            {
                textBox1.AppendText("Target has transmissed altered AIS data\r\n");
                alterAISInfo = false;
                label91.Text = "324125";
                label90.Text = "sdfdsgsdg";
                label149.Text = "sdfdsgsdg";
                label141.Text = "100001";
                label136.Text = "unk";
                label125.Text = "unk";
                label139.Text = "unk";
                label134.Text = "unk";
                label140.Text = "unk";
                label137.Text = "unk";
                label135.Text = "unk";
                if (Cutter1.vesselCategory == "CG")
                {
                    forceFVI = true;
                    textBox1.AppendText("Press \"8\" to retrieve FVI\r\n");

                }


            }
        }

        private void PseudonymTimer_Tick(object sender, EventArgs e)
        {
            if (reader.Read())
            {
             
            
                Cutter1.Pseudonym = reader[1].ToString();
                Cutter2.Pseudonym = reader[2].ToString();
                   reader.Read();
                
            }
            else
            {
                reader.Close();


                reader = command.ExecuteReader();
            }


            

        }

        //nefarious vessel code
        //Fail to Send nav Info
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //isChecked = radioButton1.Checked;
            if (nefariousAct == true)
            {
                failToSendNav = true;  
                
               


            }
            

        }

        //private void radioButton1_Click(object sender, EventArgs e)
        //{
        //    if (radioButton1.Checked && !isChecked)
        //        radioButton1.Checked = false;
        //    else
        //    {
        //        radioButton1.Checked = true;
        //        isChecked = false;
        //    }
        //}

        //alter AIS info
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (nefariousAct == true)
            {
                alterAISInfo = true;
            }
        }

        //broadcast unsigned info
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (nefariousAct == true)
            {
                broadcastUnsigedInfo = true;
            }
        }

        //try to spoof CG certificate
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (nefariousAct == true)
            {
                spoofCGCertificate = true;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (forceNav == true)
            {
                forceNav = false;
                textBox1.AppendText("Retrieving Nav Info...\r\n");
                navSending = true;
                pBanimation1Send();
                oneSecDelay.Start();
                failToSendNav = false;
                message2_Click(sender, e);


            }
            else if (forceNav == false)
            {
                textBox1.AppendText("unknown operation\r\n");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (forceFVI == true)
            {
                textBox1.AppendText("Retrieving FVI by force...\r\n");
                alterAISInfo = false;
                WaitAnimation2_Tick(sender, e);


            }
            else if (forceFVI == false)
            {
                textBox1.AppendText("unknown operation\r\n");
            }

        }

        private void button48_Click(object sender, EventArgs e)
        {
            radioButton4.Checked = false;
            radioButton3.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            failToSendNav = false;
            forceNav = false;//flag for cg vessel countering Denial of Nav info
            forceFVI = false;

            broadcastUnsigedInfo = false;
            spoofCGCertificate = false;

            alterAISInfo = false;

        }





//=========================================end of code=========================
    } // end of partial class
} // AIS simulation
