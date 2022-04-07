using System;
using System.Drawing;

/*
    - Prime numbers visualization -- test
*/

namespace my
{
    public class myObj_080 : myObject
    {
        protected int A = 0, R = 0, G = 0, B = 0;
        protected int x0 = 0, y0 = 0, N = 0;

        int step = 1, dir = 0, cnt = 0, gl_Count = 0;

        // -------------------------------------------------------------------------

        public myObj_080()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.Black);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                f = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);

                Log($"myObj_080: colorPicker({colorPicker.getMode()})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            x0 = Width  / 2;
            y0 = Height / 2;

            gl_Count = 1;

            p.Color = Color.FromArgb(33, 233, 33, 33);
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            if (gl_Count == 1)
            {
                br.Color = Color.Red;
                g.FillRectangle(br, x0, y0, 1, 1);
            }

            if (isPrime(gl_Count))
            {
                //br.Color = Color.FromArgb(gl_Count % 255, 255, 255, 255);

                br.Color = Color.FromArgb((gl_Count % 255) / 13, 233, 33, 33);
                p.Color = Color.FromArgb((gl_Count % 255)/2, 233, 33, 33);

                //if (gl_Count % 17 == 13)
                //if (step % 13 == 7)
                {
                    g.FillRectangle(Brushes.White, x0, y0, 1, 1);

                    //g.DrawRectangle(p, x0 - 10, y0 - 10, 20, 20);

/*
                    Size = 33;
                    g.FillEllipse(br, x0 - Size, y0 - Size, 2* Size, 2* Size);
                    g.DrawEllipse(p, x0 - Size, y0 - Size, 2 * Size, 2 * Size);
*/
                }
            }
        }

        // -------------------------------------------------------------------------

        private bool isPrime(int num)
        {
            if (num > 1)
            {
                // Don't want to calculate sqrt for smaller numbers
                int maxDivider = num < 1024 ? num : (int)(Math.Sqrt(num)) + 1;

                for (int i = 2; i < maxDivider; i++)
                    if (num % i == 0)
                        return false;

                return true;
            }

            return false;
        }

        // -------------------------------------------------------------------------

        private void getdXdY(int dir, ref int dx, ref int dy)
        {
            switch (dir % 4)
            {
                case 0:
                    dx = 1;
                    dy = 0;
                    break;

                case 1:
                    dx = 0;
                    dy = 1;
                    break;

                case 2:
                    dx = -1;
                    dy = 0;
                    break;

                case 3:
                    dx = 0;
                    dy = -1;
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            int dx = 0, dy = 0;

            getdXdY(dir, ref dx, ref dy);

            if (cnt < step)
            {
                cnt++;
                x0 += dx;
                y0 += dy;
            }
            else
            {
                if (dx == 0)
                {
                    step += 1;
                }

                getdXdY(++dir, ref dx, ref dy);

                cnt = 1;
                x0 += dx;
                y0 += dy;
            }

            gl_Count++;

#if false
            int dx = 0, dy = 0;

            getdXdY(dir, ref dx, ref dy);

            if (cnt < step)
            {
                cnt++;
                x0 += dx;
                y0 += dy;
            }
            else
            {
                getdXdY(++dir, ref dx, ref dy);
                cnt = 1;
                x0 += dx;
                y0 += dy;

                if (dx == 0)
                {
                    step += 2;
                }
            }
#endif
            return;
/*
            while (true)
            {
                N++;

                if (isPrime(N))
                {
                    Y = N / Width;
                    X = N % Width;
                    return;
                }
            }
*/
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            //g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            while (isAlive)
            {
                Show();
                Move();

                if (++cnt == 10)
                {
                    form.Invalidate();
                    cnt = 0;
                }

                //System.Threading.Thread.Sleep(t);
            }

            br.Dispose();

            return;
        }

        // -------------------------------------------------------------------------
    };
};
