using System.Drawing;
using System.Collections.Generic;

/*
    - Desktop: Diminishing pieces
*/

namespace my
{
    public class myObj_170 : myObject
    {
        int lifeCnt = 0;
        float size = 0, dSize = 0;
        bool doDraw = false, isSizeChanged = false;

        static int drawMode = 0, t = 0, maxSize = 66;
        static List<myObject> list = null;
        static Rectangle rect;
        static bool doLeaveTrace = false, doUseCells = false;
        static string info = string.Empty;
        static float largeDSize = 0.0f;

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

                doUseCells = rand.Next(3) == 0;
                doLeaveTrace = rand.Next(2) == 0;

doUseCells = true;

                // With the probability of 1/5 we'll have static dSize > 1.0 (which will cause it to leave concentric traces)
                {
                    largeDSize = rand.Next(5);

                    if (largeDSize == 0.0f)
                    {
                        largeDSize = 1.1f + 0.1f * rand.Next(11);
                        t += 13;
                    }
                    else
                    {
                        largeDSize = 0.0f;
                    }
                }

                switch (rand.Next(3))
                {
                    case 0: maxSize = 066; break;
                    case 1: maxSize = 111; break;
                    case 2: maxSize = 166; break;
                }

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
            lifeCnt = rand.Next(23) + 10;
            doDraw = true;

            int iSize = rand.Next(maxSize) + 5;

            if (largeDSize == 0.0f)
            {
                dSize = 0.1f + 0.1f * rand.Next(10);
            }
            else
            {
                dSize = largeDSize;
            }

            X = iSize + rand.Next(Width  - 2 * iSize);
            Y = iSize + rand.Next(Height - 2 * iSize);

            rect.X = X - iSize;
            rect.Y = Y - iSize;

            rect.Width  = iSize * 2;
            rect.Height = iSize * 2;

            size = iSize;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            int iSize = (int)size;

            rect.X = X - iSize;
            rect.Y = Y - iSize;

            rect.Width = iSize * 2;
            rect.Height = iSize * 2;

            if (size <= 0)
            {
                if (--lifeCnt == 0)
                {
                    generateNew();
                }
            }

            size -= dSize;

            // To draw the black rectangle only once
            isSizeChanged = (int)(size) != iSize;
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
                        br.Color = Color.FromArgb(150 + rand.Next(50), p.Color.R, p.Color.G, p.Color.B);

                        g.FillRectangle(br, rect);
                        g.DrawRectangle( p, rect);
                        break;

                    // Piece of an image
                    case 1:
                        g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);
                        break;
                }

                if (!doLeaveTrace)
                    size++;
            }
            else
            {
                if (size >= 0)
                {
                    if (isSizeChanged)
                    {
                        g.DrawRectangle(Pens.Black, rect);

/*
                        if (size > 3)
                        {
                            g.FillRectangle(Brushes.Black, X - size + 1, Y - size + 1, 1, 1);
                            g.FillRectangle(Brushes.Black, X - size + 1, Y + size - 1, 1, 1);
                            g.FillRectangle(Brushes.Black, X + size - 1, Y - size + 1, 1, 1);
                            g.FillRectangle(Brushes.Black, X + size - 1, Y + size - 1, 1, 1);
                        }
*/
                    }

                    if (size < 1)
                    {
                        g.FillRectangle(Brushes.Black, X, Y, 1, 1);
                    }
                }
            }

            doDraw = false;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int Cnt = 666, cnt = 0;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            // Main loop
            while (myObject.isAlive)
            {
                foreach (myObj_170 s in list)
                {
                    s.Move();
                    s.Show();
                }

                // View some info (need to press Tab)
                if (my.myObject.ShowInfo)
                {
                    if (info.Length == 0)
                    {
                        info = $" obj = myObj_170\n drawMode = {drawMode}\n colorMode = {colorPicker.getMode()}\n Trace = {doLeaveTrace}\n t = {t}";
                    }

                    if (cnt % 3 == 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 160, 111);
                        g.DrawString(info, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (info.Length > 0)
                    {
                        info = string.Empty;
                        g.FillRectangle(Brushes.Black, 30, 33, 160, 111);
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
