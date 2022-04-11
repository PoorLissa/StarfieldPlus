﻿using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - Desktop: Ever fading away pieces
*/

namespace my
{
    public class myObj_160 : myObject
    {
        int lifeCnt = 0;
        bool doDraw = false;

        static int drawMode = 0, moveMode = 0, t = 0, size = 0;
        static List<myObject> list = null;
        static Rectangle rect;

        // -------------------------------------------------------------------------

        public myObj_160()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                f = new Font("Segoe UI", 8, FontStyle.Regular, GraphicsUnit.Point);
                list = new List<myObject>();

                t = rand.Next(10) + 1;

                // Solid color is ('0') the default mode
                drawMode = 0;

                // But when colorPicker has an image, the mode is set to '1' with the probability of 2/3
                if (colorPicker.getMode() < 2)
                    if (rand.Next(3) > 0)
                        drawMode = 1;

                moveMode = rand.Next(2);

                rect.Width  = 50;
                rect.Height = 50;
                size = rand.Next(66) + 5;

                Log($"myObj_160: colorPicker({colorPicker.getMode()})");
            }
            else
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            if (moveMode == 1)
            {
                size = rand.Next(66) + 5;
            }

            lifeCnt = rand.Next(100) + 100;
            doDraw = true;

            X = rand.Next(Width);
            Y = rand.Next(Height);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            rect.X = X;
            rect.Y = Y;

            if (moveMode == 1)
            {
                rect.Width = size;
                rect.Height = size;
            }

            if (--lifeCnt == 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            // Draw only once per cell's life time
            if (doDraw)
            {
                switch (drawMode)
                {
                    // Solid color from color picker
                    case 0:
                        colorPicker.getColor(p, X, Y);
                        colorPicker.getColor(br, X, Y, 150 + rand.Next(50));

                        g.FillRectangle(br, X - size/2, Y - size/2, size, size);
                        g.DrawRectangle( p, X - size/2, Y - size/2, size, size);
                        break;

                    // Piece of an image
                    case 1:
                        g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);
                        break;
                }
            }

            doDraw = false;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            string strInfo = "";
            int Cnt = 666, cnt = 0;
            var dimBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            // Main loop
            while (isAlive)
            {
                foreach (myObj_160 s in list)
                {
                    s.Show();
                    s.Move();
                }

                // View some info (need to press Tab)
                if (my.myObject.ShowInfo)
                {
                    if (strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_160\n drawMode = {drawMode}\n moveMode = {moveMode}\n colorMode = {colorPicker.getMode()}\n";
                    }

                    if (cnt % 3 == 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 100);
                        g.DrawString(strInfo, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (strInfo.Length > 0)
                    {
                        strInfo = "";
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 100);
                    }
                }

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_160());
                }

                // Dim the screen
                if (cnt % 10 == 0)
                {
                    g.FillRectangle(dimBrush, 0, 0, Width, Height);
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
                cnt++;
            }

            return;
        }

        // -------------------------------------------------------------------------
    }
};
