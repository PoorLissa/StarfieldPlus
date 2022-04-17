using System;
using System.Drawing;
using System.Collections.Generic;


/*
    - Rain Drops
*/


namespace my
{
    public class myObj_030 : myObject
    {
        static List<myObject> list = null;
        static SolidBrush dimBrush = null;
        static int dimAlpha = 0, t = 33;

        protected float x, y, dx, dy;
        protected int lifeCounter = -1;
        protected int A = 0, R = 0, G = 0, B = 0;

        protected bool isSlow = false;

        public myObj_030()
        {
            if (colorPicker == null)
            {
                dimAlpha = (rand.Next(2) == 0) ? 255 : rand.Next(200) + 10;

                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                f = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
                dimBrush = new SolidBrush(Color.FromArgb(dimAlpha, 0, 0, 0));
                list = new List<myObject>();
                colorPicker = new myColorPicker(Width, Height);

                Log($"myObj_030: colorPicker({colorPicker.getMode()})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            isSlow = false;

            if (lifeCounter == -1)
            {
                lifeCounter = rand.Next(100);
            }
            else
            {
                lifeCounter = rand.Next(100) + 100;
            }

            int rnd = rand.Next(1000);
            int maxSize = 13;

            if (rnd < 100)
                maxSize = 27;

            if (rnd == 666)
                maxSize = 123;

            if (rnd > 750)
            {
                isSlow = true;
                maxSize = 2;
                lifeCounter = rand.Next(5) + 1;
            }

            Size = rand.Next(maxSize) + 1;
            A = rand.Next(256);
            X = rand.Next(Width);
            Y = isSlow ? rand.Next(Height) : 1;

            x = X;
            y = Y;

            dx = 0;
            dy = 0;

            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (isSlow)
            {
                if (Y % 5 == 0)
                {
                    x += rand.Next(3) - 1;
                }

                y += dy;
                X = (int)x;
                Y = (int)y;

                dy += (0.01f + Size / 20.0f);

                if (Y >= Height)
                {
                    generateNew();
                }
            }
            else
            {
                if (lifeCounter == 0)
                {
                    x += dx;
                    y += dy;
                    X = (int)x;
                    Y = (int)y;

                    dy += (1.0f + Size / 10.0f);

                    if (Y >= Height)
                    {
                        dy *= -0.025f * (rand.Next(11) + 1);

                        dx += 0.5f * (rand.Next(10)) * (rand.Next(3) - 1);

                        if (dy > -1)
                        {
                            generateNew();
                        }
                    }
                }
                else
                {
                    if (--lifeCounter == 0)
                    {
                        dy = -0.1f * (rand.Next(20) + 10);
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);

            if (Size < 3)
            {
                g.FillRectangle(br, X, Y, Size, Size);
            }
            else
            {
                if (Y == 1)
                {
                    g.FillEllipse(br, X, Y - Size / 2, Size, Size);
                }
                else
                {
                    g.FillEllipse(br, X, Y, Size, Size);

                    p.Color = Color.FromArgb((A < 225 ? A + 30 : A), R, G, B);
                    g.DrawEllipse(p, X, Y, Size, Size);
                }
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;
            string strInfo = "";

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                g.FillRectangle(dimBrush, 0, 0, Width, Height);

                foreach (myObj_030 s in list)
                {
                    s.Show();
                    s.Move();
                }

                // Display some info
                if (my.myObject.ShowInfo)
                {
                    //if (cnt % 100 == 0 || strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_030\n colorMode = {colorPicker.getMode()}\n dimAlpha = {dimAlpha}\n t = {t}\n cnt = {cnt}";
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 150);
                        g.DrawString(strInfo, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (strInfo.Length > 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 150);
                        strInfo = string.Empty;
                    }
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Count + 150)
                {
                    list.Add(new myObj_030());
                }

                cnt++;
            }

            return;
        }
    };
};
