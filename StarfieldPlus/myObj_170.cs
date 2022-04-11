using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - Desktop: Ever fading away pieces
*/

namespace my
{
    public class myObj_170 : myObject
    {
        int lifeCnt = 0, size = 0;
        bool doDraw = false;

        static int drawMode = 0, moveMode = 0, t = 0;
        static List<myObject> list = null;
        static Rectangle rect;

        // -------------------------------------------------------------------------

        public myObj_170()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                f = new Font("Segoe UI", 8, FontStyle.Regular, GraphicsUnit.Point);
                list = new List<myObject>();

                t = rand.Next(50) + 1;

                // Solid color is ('0') the default mode
                drawMode = 0;

                // But when colorPicker has an image, the mode is set to '1' with the probability of 2/3
                if (colorPicker.getMode() < 2)
                    if (rand.Next(3) > 0)
                        drawMode = 1;

                moveMode = rand.Next(2);

drawMode = 0;

                rect.Width  = 50;
                rect.Height = 50;
                size = rand.Next(66) + 5;

                Log($"myObj_170: colorPicker({colorPicker.getMode()})");
            }
            else
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            size = rand.Next(66) + 5;

            lifeCnt = rand.Next(100) + 100;
            doDraw = true;

            lifeCnt *= 2;

            X = rand.Next(Width);
            Y = rand.Next(Height);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            size--;

            rect.X = X;
            rect.Y = Y;

            if (moveMode == 1)
            {
                rect.Width = size * 2;
                rect.Height = size * 2;
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

                        g.FillRectangle(br, X - size, Y - size, size*2, size*2);
                        g.DrawRectangle( p, X - size, Y - size, size*2, size*2);
                        break;

                    // Piece of an image
                    case 1:
                        g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);
                        break;
                }

                size++;
            }
            else
            {
                if (size >= 0)
                {
                    g.DrawRectangle(Pens.Black, X - size, Y - size, size * 2, size * 2);
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
                foreach (myObj_170 s in list)
                {
                    s.Show();
                    s.Move();
                }

                // View some info (need to press Tab)
                if (my.myObject.ShowInfo)
                {
                    if (strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_170\n drawMode = {drawMode}\n moveMode = {moveMode}\n colorMode = {colorPicker.getMode()}\n";
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
                    list.Add(new myObj_170());
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
