using System;
using System.Drawing;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


/*
    // - moving ponts generator
*/

namespace my
{
    public class myObj_180 : myObject
    {
        protected static List<myObject> listLive = null;
        protected static List<myObject> listDead = null;
        protected float x, y, dx, dy, acceleration = 1.0f;
        protected int cnt = 0, max = 0;

        protected static Color color;
        protected Color clr;
        protected bool isLive = false;
        protected static int x0 = 0, y0 = 0, drawMode = 0;
        protected static float Speed = 1;

        // -------------------------------------------------------------------------

        public myObj_180()
        {
            if (br == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.Black);
                colorPicker = new myColorPicker(Width, Height);
                listLive = new List<myObject>();
                listDead = new List<myObject>();

                x0 = Width  / 2;
                y0 = Height / 2;

                Log($"myObj_180");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            isLive = true;

            clr = color;

            x = X = rand.Next(Width);
            y = Y = rand.Next(Width);

            float speed = Speed;

            //speed += (rand.Next(11) - 5) * 0.05f;

            float dist = (float)(Math.Sqrt((X - x0) * (X - x0) + (Y - x0) * (Y - x0)));

            dx = (X - x0) * speed / dist;
            dy = (Y - x0) * speed / dist;

            x = X = x0;
            y = Y = y0;

            Size = 1;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            int size = 2;

            //size = Size;

            p.Color = clr;
            g.DrawRectangle(p, x - size, y - size, 2 * size, 2 * size);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (isLive)
            {
                x += dx;
                y += dy;

                //dx += 0.5f;

                Size++;

                if (x < 0 || x > Width || y < 0 || y > Height)
                {
                    isLive = false;
                    x = -11;
                    y = -11;
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int n = 50, cnt = 0, Cnt = 20, deadCnt = 0;

            int nnn = 0;

            for (int i = 0; i < 1000; i++)
            {
                listDead.Add(new myObj_180());
            }

            br.Color = Color.FromArgb(255, 0, 0, 0);
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                deadCnt = 0;

                g.FillRectangle(br, 0, 0, Width, Height);

                foreach (myObj_180 obj in listLive)
                {
                    obj.Show();
                    obj.Move();

                    if (obj.isLive == false)
                    {
                        listDead.Add(obj);
                        deadCnt++;
                    }
                }

                if (deadCnt > 0)
                {
                    // Remove the objects we just marked dead from live list (those will be deadCnt last objects in listDead)
                    for (int i = listDead.Count - deadCnt; i < listDead.Count; i++)
                    {
                        var obj = listDead[i] as myObj_180;

                        if (!obj.isLive)
                        {
                            listLive.Remove(obj);
                        }
                    }
                }

                if (cnt % 33 == 0)
                //if (cnt == Cnt)
                {
                    //cnt = 0;
                    //Cnt = 20 + rand.Next(20);
                    //Cnt = Cnt == 20 ? 10 : 20;

                    if (listDead.Count >= n)
                    {
                        // Set speed of the next wave
                        Speed = 1.0f + 0.1f * rand.Next(50);

                        //Speed = 1.0f + (float)Math.Sin(cnt) * 2.0f;
                        //Speed = 5.0f + 0.01f * rand.Next(100);
                        //Speed = 10.0f;

                        color = Color.FromArgb(rand.Next(100) + 150, rand.Next(256), rand.Next(256), rand.Next(256));

                        for (int i = 0; i < n; i++)
                        {
                            var obj = listDead[0] as myObj_180;

                            obj.generateNew();

                            listLive.Add(obj);
                            listDead.Remove(obj);
                        }
                    }
                }

                form.Invoke(new MethodInvoker(delegate
                {
                    form.Update();
                    form.Refresh();
                }));



                //form.Update();
                //form.Refresh();
                //form.Invalidate();
                System.Threading.Thread.Sleep(33);

                cnt++;
            }

            return;
        }

        // -------------------------------------------------------------------------
    }
};
