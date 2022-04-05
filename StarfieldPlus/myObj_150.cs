using System;
using System.Drawing;

/*
    - Cellular Automaton: Conway's Life
*/

namespace my
{
    public class myObj_150 : myObject
    {
        static int step = 0, startX = 0, startY = 0, drawMode = 0;
        static System.Collections.Generic.List<myObj_150> list = null;
        static Rectangle rect;

        bool alive = false;
        bool clear = false;
        int liveCnt = 0, lifeSpanCnt = 0;

        // -------------------------------------------------------------------------

        public myObj_150()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                f = new Font("Segoe UI", 8, FontStyle.Regular, GraphicsUnit.Point);
                list = new System.Collections.Generic.List<myObj_150>();

                step = 25;
                step = 35;
                step = 40;

                rect.Width  = step - 3;
                rect.Height = step - 3;

                // Exclude drawing mode which draws parts of an image, when the colorPicker does not taget any image
                drawMode = colorPicker.getMode() < 2 ? rand.Next(3) : rand.Next(2);

                Log($"myObj_150: colorPicker({colorPicker.getMode()})");
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            X += startX - (X % step);
            Y += startY - (Y % step);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (liveCnt == -1)
            {
                // 1st iteration: observe neighbours
                liveCnt = alive ? -1 : 0;
                int x = -1, y = -1;

                getCoords(ref x, ref y);

                for (int i = x - 1; i < x + 2; i++)
                {
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        var obj = getObj(i, j);

                        if (obj != null && getObj(i, j).alive)
                        {
                            liveCnt++;
                        }
                    }
                }
            }
            else
            {
                // 2nd iteration: make life decision
                if (alive)
                {
                    alive = false;

                    if (liveCnt == 2 || liveCnt == 3)
                    {
                        alive = true;
                        lifeSpanCnt++;
                    }
                }
                else
                {
                    if (liveCnt == 3)
                    {
                        alive = true;
                        lifeSpanCnt = 0;
                    }
                }

                liveCnt = -1;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            if (alive)
            {
                switch (drawMode)
                {
                    // Solid color
                    case 0:
                        g.FillRectangle(Brushes.LightGray, X + 2, Y + 2, step - 3, step - 3);
                        g.DrawRectangle(Pens.Red, X + 2, Y + 2, step - 4, step - 4);
                        break;

                    // Solid color from colorPicker
                    case 1:
                        colorPicker.getColor(br, X < 0 ? 1 : X + 3, Y < 0 ? 1 : Y + 3, 200);
                        g.FillRectangle(br, X + 2, Y + 2, step - 3, step - 3);
                        break;

                    // Part of an image
                    case 2:
                        rect.X = X + 2;
                        rect.Y = Y + 2;
                        g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);
                        break;
                }

                //g.DrawString(liveCnt.ToString(), f, Brushes.Red, X + 2, Y + 2);
                //g.DrawString(lifeSpanCnt.ToString(), f, Brushes.Red, X + 2, Y + 2);

                clear = false;
            }
            else
            {
                if (!clear)
                {
                    g.FillRectangle(Brushes.White, X + 2, Y + 2, step - 3, step - 3);
                }

                clear = true;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            string strInfo = "";

            int t = 666, Cnt = 0;
            int w = Width  / step + 1;
            int h = Height / step + 1;

            drawGrid();
            System.Threading.Thread.Sleep(t);

            for (int j = startY - step; j < step * h; j += step)
            {
                for (int i = startX - step; i < step * w; i += step)
                {
                    if (!isAlive)
                        break;

                    var obj = new myObj_150();

                    obj.X = i;
                    obj.Y = j;
                    obj.alive = false;
                    obj.clear = false;

                    obj.liveCnt = -1;
                    obj.lifeSpanCnt = 0;

                    list.Add(obj);
                }
            }

            Cnt = list.Count / 7;
            Cnt = rand.Next(Cnt/2) + Cnt;

            for (int i = 0; i < Cnt; i++)
            {
                if (!isAlive)
                    break;

                int index = rand.Next(list.Count);

                if (!list[index].alive)
                {
                    list[index].alive = true;
                    list[index].Show();
                    form.Invalidate();
                    System.Threading.Thread.Sleep(3);
                }
            }

            if (isAlive)
            {
                // Display the first generation
                foreach (var s in list)
                    s.Show();

                System.Threading.Thread.Sleep(t * 3);

                while (isAlive)
                {
                    foreach (var s in list)
                        s.Move();

                    foreach (var s in list)
                    {
                        s.Move();
                        s.Show();
                    }

                    // View some info (need to press Tab)
                    if (my.myObject.ShowInfo)
                    {
                        if (strInfo.Length == 0)
                        {
                            strInfo = $" obj = myObj_150\n step = {step}\n drawMode = {drawMode}\n colorMode = {colorPicker.getMode()}\n";
                        }

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

                    form.Invalidate();
                    System.Threading.Thread.Sleep(t);
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawGrid()
        {
            g.FillRectangle(Brushes.White, 0, 0, Width, Height);

            startX = (Width  % step) / 2;
            startY = (Height % step) / 2;

            for (int i = startX; i < Width; i += step)
                g.DrawLine(Pens.Black, i, 0, i, Height);

            for (int i = startY; i < Height; i += step)
                g.DrawLine(Pens.Black, 0, i, Width, i);

            return;
        }

        // -------------------------------------------------------------------------

        private myObj_150 getObj(int x, int y)
        {
            int w = Width  / step + 2;
            int h = Height / step + 2;

            int index = y * w + x;

            if (index >= 0 && index < list.Count)
            {
                return list[index];
            }

            return null;
        }

        // -------------------------------------------------------------------------

        private void getCoords(ref int x, ref int y)
        {
            x = X / step + 1;
            y = Y / step + 1;

            return;
        }

        // -------------------------------------------------------------------------
    }
};


