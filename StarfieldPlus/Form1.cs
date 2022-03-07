﻿using System;
using System.Drawing;
using System.Windows.Forms;

// https://sites.harding.edu/fmccown/screensaver/screensaver.html

// What graphisc api to choose for C#
// https://www.reddit.com/r/csharp/comments/5uvyw9/what_can_i_use_for_basic_fast_2d_graphics/

namespace StarfieldPlus
{
    public partial class Form1 : Form
    {
        private bool isAlive = false;
        private Point oldMouseLocation;

        // -------------------------------------------------------------------

        public Form1(Rectangle bounds)
        {
            InitializeComponent();

            Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Bounds = bounds;
            //this.TopMost = true;
            this.DoubleBuffered = true;

            my.myObject.Height = this.Height;
            my.myObject.Width = this.Width;

            this.MouseMove += Form1_MouseMove;
            this.KeyDown += Form1_KeyDown;

            RunScreensaver();
        }

        // -------------------------------------------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            appExit();
        }

        // -------------------------------------------------------------------

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int dist = 50;

            if (!oldMouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(oldMouseLocation.X - e.X) > dist || Math.Abs(oldMouseLocation.Y - e.Y) > dist)
                {
                    appExit();
                }
            }

            // Update mouse location
            oldMouseLocation = e.Location;
        }

        // -------------------------------------------------------------------

        private void RunScreensaver()
        {
            // Starfield has slight priority over the others
            int id = new Random().Next(8);

            //id = 6;

            isAlive = true;
            my.myObject.Count = 333;

            new System.Threading.Tasks.Task(() => {

                switch (id)
                {
                    // Stars
                    case 0:
                        my.myObj_000.Process(this, ref isAlive);
                        break;

                    // Random roaming
                    case 1:
                        my.myObj_001.Process(this, ref isAlive);
                        break;

                    // Circles
                    case 2:
                        my.myObj_002.Process(this, ref isAlive);
                        break;

                    // Rain Drops
                    case 3:
                        my.myObj_003.Process(this, ref isAlive);
                        break;

                    // Lines
                    case 4:
                        my.myObj_004.Process(this, ref isAlive);
                        break;

                    // Circle
                    case 5:
                        my.myObj_100.Process(this, ref isAlive);
                        break;

                    // Desktop
                    case 6:
                        my.myObj_101.Process(this, ref isAlive);
                        break;

                    default:
                        my.myObj_000.Process(this, ref isAlive);
                        break;
                }

            }).Start();

            return;
        }

        // -------------------------------------------------------------------

        private void appExit()
        {
            this.MouseMove -= Form1_MouseMove;
            this.KeyDown -= Form1_KeyDown;

            isAlive = false;

            while (isAlive == false)
                ;

            Application.Exit();
        }

        // -------------------------------------------------------------------
    }
}
