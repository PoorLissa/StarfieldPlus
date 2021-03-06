using System;
using System.Drawing;

/*
    - Growing shapes -- rain drops alike
*/

namespace my
{
    public class myObj_131 : myObject
    {
        static int max_dSize = 0;
        static int shape = 0;

        protected int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, dA_Filling = 0;
        float time;

        // -------------------------------------------------------------------------

        public myObj_131()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);

                shape = rand.Next(5);
                max_dSize = rand.Next(15) + 3;

                Log($"myObj_131: colorPicker({colorPicker.getMode()}), shape({shape}), max_dSize({max_dSize})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            A = rand.Next(250) + 6;
            colorPicker.getColor(X, Y, ref R, ref G, ref B);

            if (shape == 1 || shape == 3)
            {
                // Max size is lower for the shapes that are being filled with color
                maxSize = rand.Next(222) + 33;
            }
            else
            {
                maxSize = rand.Next(333) + 33;
            }

            Size = 1;
            dSize = rand.Next(max_dSize) + 1;
            dA = rand.Next(5) + 1;
            dA_Filling = rand.Next(5) + 2;
            time = 0.0f;

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (shape == 4)
            {
                move_4();
            }
            else
            {
                Size += dSize;

                // Increase disappearing speed when max size is reached
                if (Size > maxSize)
                    dA++;

                // Decrease opacity until fully invisible
                A -= dA;
            }

            if (A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move_4()
        {
            Size += dSize;

            A -= dA;

            time += 0.1f;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            p.Color = Color.FromArgb(A, R, G, B);

            switch (shape)
            {
                case 0:
                    g.DrawEllipse(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 1:
                    br.Color = Color.FromArgb(A/dA_Filling, R, G, B);
                    g.FillEllipse(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawEllipse(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 2:
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 3:
                    br.Color = Color.FromArgb(A / dA_Filling, R, G, B);
                    g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 4: {

                        g.DrawLine(p, X - Size, Y - Size, 2 * Size, 2 * Size);    // orig
                        //g.DrawLine(p, X - Size, Y - Size, 2 * Size, Height - 2 * Size);
                        //g.DrawLine(p, X - Size, Y - Size, Width - 2 * Size, 2 * Size);
                        //g.DrawLine(p, X - Size, Y - Size, Width - 2 * Size, Height - 2 * Size);
                    }
                    break;

                case 5:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 50;
            int num = rand.Next(333) + 33;

            var list = new System.Collections.Generic.List<myObj_131>();

            list.Add(new myObj_131());

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                // Gradually increase the number of objects
                if (list.Count < num)
                {
                    list.Add(new myObj_131());
                }
            }

            return;
        }
    }
};
