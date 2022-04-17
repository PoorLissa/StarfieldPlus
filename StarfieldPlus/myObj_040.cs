using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - 
*/

namespace my
{
    public class myObj_040 : myObject
    {
        static List<myObject> list = null;
        static SolidBrush dimBrush = null;

        static int x0 = Width  / 2;
        static int y0 = Height / 2;
        static int shape = 0, moveType = 0, dimRate = 0, dimAlpha = 0, t = 0, maxSize = 0;

        float x, y, dx, dy, a, da, size;
        int R = -1, G = -1, B = -1;
        int oldX = 0, oldY = 0, initX = 0, initY = 0, repeaterCnt = 0;

        int rnd1 = 0, rnd2 = 0;

        public myObj_040()
        {
            if (p == null)
            {
                // Dimming behaviour
                switch (rand.Next(9))
                {
                    case 0:
                        dimRate = 1;
                        dimAlpha = 50;
                        break;

                    case 1:
                        dimRate = 1;
                        dimAlpha = 25;
                        break;

                    case 2:
                        dimRate = 2;
                        dimAlpha = 33;
                        break;

                    case 3:
                        dimRate = 3;
                        dimAlpha = 20;
                        break;

                    case 4:
                        dimRate = 4;
                        dimAlpha = 15;
                        break;

                    case 5:
                        dimRate = 5;
                        dimAlpha = 10;
                        break;

                    case 6:
                        dimRate = 5;
                        dimAlpha = 20;
                        break;

                    case 7:
                        dimRate = 10;
                        dimAlpha = 10;
                        break;

                    case 8:
                        dimRate = rand.Next(11) + 1;
                        dimAlpha = rand.Next(11) + 1;
                        break;
                }

                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                f = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
                dimBrush = new SolidBrush(Color.FromArgb(dimAlpha, 0, 0, 0));
                list = new List<myObject>();
                shape = rand.Next(4);
                moveType = rand.Next(5);
                t = 15 + rand.Next(11);

#if DEBUG
                moveType = 0;
                shape = 3;
                //t = 3;
#endif
                switch (shape)
                {
                    case 2:
                    case 3:
                        maxSize = 3;
                        break;

                    default:
                        maxSize = 5;
                        break;
                }
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            dx = 0;
            dy = 0;

            rnd1 = rand.Next(Width);
            rnd2 = rand.Next(Width);

            x = X = rand.Next(Width);
            y = Y = rand.Next(Height);

            oldX = initX = X;
            oldY = initY = Y;
            repeaterCnt = 0;

            float speed = 1.5f + 0.1f * rand.Next(30);
            float dist = (float)(Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0)));

            if (dimRate == 1)
                speed *= 2.0f;

            if (dimRate == 2)
                speed *= 1.33f;

            dx = (X - x0) * speed / dist;
            dy = (Y - y0) * speed / dist;

            size = Size = rand.Next(maxSize) + 1;

            a = 0;

            switch (moveType)
            {
                case 0:
                    da = 0.8f + 0.05f * rand.Next(8);
                    break;

                case 1:
                    da = 0.2f;
                    break;

                default:
                    da = 0.3f + 0.01f * rand.Next(33);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            switch (moveType)
            {
                case 0: move0(); break;
                case 1: move1(); break;
                case 2: move2(); break;
                case 3: move3(); break;
                case 4: move4(); break;
            }

            if (a < 255)
                a += da;

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            Size = (int)size;

            switch (shape)
            {
                case 0:
                    br.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    //g.FillRectangle(br, X, Y, Size, Size);
                    g.FillRectangle(br, x, y, Size, Size);
                    break;

                case 1:
                    br.Color = Color.FromArgb((int)a / 5, br.Color.R, br.Color.G, br.Color.B);
                    g.FillRectangle(br, x - 1, y - 1, Size + 1, Size + 1);

                    p.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, x, y, Size, Size);
                    break;

                case 2:
                    p.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawLine(p, X, Y, oldX, oldY);
                    g.DrawRectangle(p, x - Size, y - Size, Size * 2, Size * 2);
                    break;

                case 3:
                    p.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawLine(p, X, Y, oldX, oldY);
                    g.DrawEllipse(p, x - Size, y - Size, Size*2, Size*2);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;
            string strInfo = "";

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (myObject.isAlive)
            {
                foreach (myObj_040 s in list)
                {
                    s.Show();
                    s.Move();
                }

                if (cnt % dimRate == 0)
                {
                    g.FillRectangle(dimBrush, 0, 0, Width, Height);
                }

                // Display some info
                if (my.myObject.ShowInfo)
                {
                    if (cnt % 1 == 0 || strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_140\n shape = {shape}\n moveType = {moveType}\n dimRate = {dimRate}\n dimAlpha = {dimAlpha}\n t = {t}\n cnt = {cnt}";
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

                if (list.Count < Count)
                {
                    list.Add(new myObj_040());
                }

                if (++cnt > 1000)
                {
                    bool gotNewBrush = colorPicker.getNewBrush(br, cnt == 1001);

                    if (gotNewBrush)
                    {
                        cnt = 0;
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void move0()
        {
            repeaterCnt++;

            oldX = X;
            oldY = Y;

            X += (int)dx;
            Y += (int)dy;

            X += (int)dx + (int)(Math.Sin(Y) * 5);
            Y += (int)dy + (int)(Math.Sin(X) * 5);

            x = X;
            y = Y;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }

            // Eliminate stuck pieces
            if (repeaterCnt > 10 && X == initX && Y == initY)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move1()
        {
            x += dx;
            y += dy;

            x += dx + (float)(Math.Sin(y) * 5);
            y += dy + (float)(Math.Sin(x) * 5);

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                generateNew();
            }

            size += 0.05f;
        }

        // -------------------------------------------------------------------------

        private void move2()
        {
            x += dx;
            y += dy;

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move3()
        {
            x += dx + (float)(Math.Sin(y) * 2.33f);
            y += dy + (float)(Math.Sin(x) * 2.33f);

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move4()
        {
            x += dx;
            y += dy;

            x += (float)(Math.Sin(System.DateTime.Now.Second + Y) * 5);
            y += (float)(Math.Sin(System.DateTime.Now.Second + X) * 5);

            //x += (float)(Math.Sin(System.DateTime.Now.Second + rnd1) * 3);
            //y += (float)(Math.Sin(System.DateTime.Now.Second + rnd2) * 3);

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------
    };
};
