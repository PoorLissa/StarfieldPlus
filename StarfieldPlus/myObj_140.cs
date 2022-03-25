using System;
using System.Drawing;

/*
    - Grid with moving rectangle lenses -- test, looks strange
*/

namespace my
{
    public class myObj_140 : myObject
    {
        static int step = 0, startX = 0, startY = 0;

        private int gridX = 0, gridY = 0, gridXOld = -1111, gridYOld = -1111, cnt = 0;
        private int dx, dy;

        // -------------------------------------------------------------------------

        public myObj_140()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height, 1);
                step = 25;

                Log($"myObj_140: colorPicker({colorPicker.getMode()})");
            }

            int maxSpeed = 10;

            dx = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

            X = rand.Next(Width);
            Y = rand.Next(Height);
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (cnt++ < 150)
            {
                return;
            }

            cnt = rand.Next(25);

#if true
            X = rand.Next(Width);
            Y = rand.Next(Height);
#else
            X += dx;
            Y += dy;

            if (X < 0 || X > Width)
            {
                dx *= -1;
            }

            if (Y < 0 || Y > Height)
            {
                dy *= -1;
            }
#endif
            gridXOld = gridX;
            gridYOld = gridY;

            gridX = X;
            gridY = Y;

            if (gridX >= startX)
            {
                gridX -= startX;
                gridX -= gridX % step;
                gridX += startX;
            }
            else
            {
                gridX = startX - step;
            }

            if (gridY >= startY)
            {
                gridY -= startY;
                gridY -= gridY % step;
                gridY += startY;
            }
            else
            {
                gridY = startY - step;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            int offset = step;

            // restore old part of the image
            var rect = new Rectangle(gridXOld - offset, gridYOld - offset, step + 2 * offset, step + 2 * offset);
            g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);

            //br.Color = Color.FromArgb(100, 200, 33, 33);
            //g.FillRectangle(Brushes.White, X-2, Y-2, 5, 5);
            //g.FillRectangle(br, gridX + 1, gridY + 1, step - 1, step - 1);

            rect.X = gridX - offset;
            rect.Y = gridY - offset;

            var rect2 = new Rectangle(gridX + 1, gridY + 1, step - 1, step - 1);

            g.DrawImage(colorPicker.getImg(), rect, rect2, GraphicsUnit.Pixel);

            rect.Width--;
            rect.Height--;

            g.DrawRectangle(Pens.Red, rect);
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            drawGrid();
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            int t = 33, Cnt = 5;

            var list = new System.Collections.Generic.List<myObj_140>();

            while (isAlive)
            {
                foreach (var s in list)
                {
                    s.Move();
                    s.Show();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_140());
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawGrid()
        {
            startX = (Width  % step) / 2;
            startY = (Height % step) / 2;

            for (int i = startX; i < Width; i += step)
                colorPicker.GetGraphics().DrawLine(Pens.Black, i, 0, i, Height);

            for (int i = startY; i < Height; i += step)
                colorPicker.GetGraphics().DrawLine(Pens.Black, 0, i, Width, i);

            return;
        }

        // -------------------------------------------------------------------------
    }
};


