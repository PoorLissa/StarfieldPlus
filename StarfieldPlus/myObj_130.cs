using System;
using System.Drawing;

/*
    - ...
*/

namespace my
{
    public class myObj_130 : myObject
    {
        static Pen p = null;
        static SolidBrush br = null;
        static int shape = 0;
        static int moveMode = 0;
        static int A_Filling = 0;

        static int globalCounter = 0;
        static int moveSetUp = 0;
        static int moveParam1 = 0;
        static int moveParam2 = 0;
        static int moveParam3 = 0;
        static int moveParam4 = 0;
        static int moveParam5 = 0;

        int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, mult = 0, counter = 0;

        // -------------------------------------------------------------------------

        public myObj_130()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);

                moveMode = rand.Next(7);
                shape = rand.Next(6);
                A_Filling = rand.Next(11) + 1;
            }

#if false
            // #pmv override
            shape = 0;
            moveMode = 71;
            colorPicker.setMode(33);
#endif

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            Size = 1;
            maxSize = rand.Next(333) + 33;
            dSize = rand.Next(5) + 1;
            dA = rand.Next(5) + 1;
            mult = rand.Next(3) + 1;
            counter = 0;

            A = rand.Next(256);
            colorPicker.getColor(X, Y, ref R, ref G, ref B);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            switch (moveMode)
            {
                case 0: move0(); break;
                case 1: move1(); break;
                case 2: move2(); break;
                case 3: move3(); break;
                case 4: move4(); break;
                case 5: move5(); break;
                case 6: move6(); break;
                case 7: move7(); break;

                default: move_test(); break;
            }

            move_final();
        }

        // -------------------------------------------------------------------------

        private void move0()
        {
            ;
        }

        // -------------------------------------------------------------------------

        private void move1()
        {
            X += (rand.Next(3) - 1) * mult;
            Y += (rand.Next(3) - 1) * mult;
        }

        // -------------------------------------------------------------------------

        private void move2()
        {
            X += (rand.Next(3) - 1) * mult * 5;
            Y += (rand.Next(3) - 1) * mult * 5;
        }

        // -------------------------------------------------------------------------

        private void move3()
        {
            X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
            Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
            X += (int)(Math.Tan(X) * 10);
            Y += (int)(Math.Tan(Y) * 10);
        }

        // -------------------------------------------------------------------------

        private void move4()
        {
            if (moveSetUp == 0)
            {
                moveSetUp = 1;
                moveParam1 = rand.Next(9) + 2;
                moveParam2 = rand.Next(2) + 1;
                moveParam3 = rand.Next(6) + 1;
                moveParam4 = rand.Next(3);
            }

            int dx = 0, dy = 0;

            if (dSize > 2)
            {
                if (moveParam4 > 0)
                    dx = (rand.Next(moveParam1) + moveParam3) / moveParam2;

                if (moveParam4 > 1)
                    dx /= 2;

                dy = (rand.Next(moveParam1) + moveParam3) / moveParam2;
            }
            else
            {
                if (moveParam4 > 0)
                    dx = rand.Next(moveParam1) + moveParam3;

                if (moveParam4 > 1)
                    dx /= 2;

                dy = rand.Next(moveParam1) + moveParam3;
            }

            X += dx;
            Y += dy;
        }

        // -------------------------------------------------------------------------

        private void move5()
        {
            if (moveSetUp == 0)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        moveParam1 = rand.Next(50) + 1;
                        break;

                    case 1:
                        moveParam1 = rand.Next(100) + 1;
                        break;

                    case 2:
                        moveParam1 = rand.Next(300) + 1;
                        break;

                    case 3:
                        moveParam1 = rand.Next(600) + 1;
                        break;
                }

                moveParam2 = rand.Next(2);
                moveSetUp = 1;
            }

            if (moveParam2 == 0)
                dSize = 1;

            X += (int)(Math.Sin(Size) * moveParam1);
            Y += (int)(Math.Cos(Size) * moveParam1);
        }

        // -------------------------------------------------------------------------

        private void move6()
        {
            if (moveSetUp == 0)
            {
                moveParam1 = rand.Next(66) + 3;
                moveParam2 = rand.Next(600) + 66;
                moveSetUp = 1;
            }

            Size = moveParam2;

            X += (int)(Math.Sin(globalCounter) * moveParam1);
            Y += (int)(Math.Cos(globalCounter) * moveParam1);
        }

        // -------------------------------------------------------------------------

        private void move7()
        {
            if (moveSetUp == 0)
            {
                moveParam1 = rand.Next(66) + 3;
                moveParam2 = rand.Next(66) + 15;
                moveParam3 = rand.Next(11);
                moveParam4 = rand.Next(11);
                moveSetUp = 1;
            }

            Size = moveParam2;
            A += dA / 2;

            X += (int)(Math.Sin(globalCounter) * moveParam1);
            Y += (int)(Math.Cos(globalCounter) * moveParam1);

            X += (int)(Math.Sin(counter + moveParam3) * moveParam1);
            Y += (int)(Math.Cos(counter + moveParam4) * moveParam1);
        }

        // -------------------------------------------------------------------------

        private void move_final()
        {
            Size += dSize;

            // Increase disappearing speed when max size is reached
            if (Size > maxSize)
                dA++;

            // Decrease opacity until fully invisible
            A -= dA;

            counter++;

            if (A < 0)
            {
                generateNew();
            }
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
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 2:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillEllipse(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 3:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 4:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillEllipse(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawEllipse(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 5:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
            int t = 50;
            int num = 10;

            var list = new System.Collections.Generic.List<myObj_130>();

            list.Add(new myObj_130());

            if (rand.Next(2) == 0)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            }

            while (isAlive)
            {
                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if(list.Count < num)
                {
                    list.Add(new myObj_130());
                }

                globalCounter++;
            }

            return;
        }






        private void move_test()
        {
            int num = 1;

            if (moveSetUp == 0)
            {
                moveParam1 = rand.Next(33) + 1;
                moveParam2 = rand.Next(11) + 1;
                moveParam3 = rand.Next(3) + 1;
                moveParam4 = rand.Next(3) + 1;
                moveParam5 = (rand.Next(50) + 75);
                moveSetUp = 1;
            }

            switch (num)
            {
                case 0:
                    X += (int)(Math.Sin(Y) * 10);
                    Y += (int)(Math.Cos(X) * 10);
                    break;
/*
                case 1:
                    break;*/

                case 2:
                    X += (int)(Math.Sin(X) * Math.Sin(X) * 10);
                    Y += (int)(Math.Cos(Y) * Math.Cos(Y) * 10);
                    break;

                case 3:
                    X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
                    Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
                    break;

                case 4:
                    X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
                    Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
                    X += (int)(Math.Tan(X) * 10);
                    break;

                case 5:
                    X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
                    Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
                    X += (int)(Math.Tan(X) * 10);
                    Y += (int)(Math.Tan(Y) * 10);
                    break;

                // Ok
                case 6:
                    X += (int)(Math.Sin(X) * Math.Cos(X) * moveParam1);
                    Y += (int)(Math.Sin(Y) * Math.Cos(Y) * moveParam1);
                    break;

                // OK
                case 7:
                    dSize += (int)(Math.Sin(counter*dSize) * moveParam1);
                    break;

                // Ok
                case 8:
                    dSize += (int)(Math.Tan(counter * dSize) * moveParam1);
                    break;

                case 9:
                    dSize += (int)(Math.Sin(counter) * Math.Tan(dSize) * moveParam1);
                    break;

                // Ok
                case 10:
                    dSize += (int)(Math.Sin(counter) * Math.Sin(dSize) * moveParam1);
                    X += (int)(Math.Sin(Y) * moveParam2);
                    Y += (int)(Math.Cos(X) * moveParam2);
                    break;

                // Ok
                case 11:
                    X += moveParam3;
                    Y += moveParam4;
                    dSize += (int)(Math.Sin(counter) * Math.Sin(dSize) * moveParam1);
                    break;

                // Ok
                case 12:
                    X += moveParam3;
                    Y += moveParam4;
                    dSize += (int)(Math.Sin(counter) * Math.Cos(dSize) * moveParam1);
                    break;

                // Ok
                case 13:
                    dSize += (int)((Math.Tan(dSize) / (Math.Tan(counter) + 0.001)) / moveParam5);
                    break;

                case 14:
                    X += 10 * (int)(Math.Sin(globalCounter) * 5);
                    Y += 10 * (int)(Math.Cos(globalCounter) * 5);

                    dSize = 1;
                    Size = (int)(Math.Tan(counter));
                    break;

                case 15:
                    X += (int)(Math.Sin(maxSize) * 11);
                    Y += (int)(Math.Cos(maxSize) * 11);
                    break;

                case 1:
                    //X += moveParam3;
                    //Y += moveParam4;

                    double val1 = Math.Tan(dSize);
                    double val2 = Math.Sin(dSize);
                    double val3 = Math.Cos(dSize);

                    double val4 = Math.Tan(counter);
                    double val5 = Math.Sin(counter);
                    double val6 = Math.Cos(counter);

                    double val7 = Math.Tan(Size);
                    double val8 = Math.Sin(Size);
                    double val9 = Math.Cos(Size);

                    //dSize += (int)(Math.Sin(counter) * Math.Tan(dSize) * moveParam1);
                    //dSize += (int)(Math.Tan(counter * dSize) * moveParam1);

                    //dSize += (int)(Math.Sin(counter) * Math.Tan(dSize) * 5);

                    //dSize += (int)(Math.Tan(counter) * 5);

                    //dSize += (int)((val2 / (val4 + 0.001)) / moveParam5);

                    double vvv = Math.Sin(globalCounter);
                    double zzz = Math.Cos(globalCounter);

                    //X += 2 * (int)(vvv * 15);
                    //Y += 2 * (int)(zzz * 15);

                    Size = 2;

                    if (val5 < 0) val5 = -val5;
                    if (val6 < 0) val6 = -val6;

                    X += (int)(val5 * 10);
                    Y += (int)(val6 * 10);

                    A += dA/2;

                    //dSize = 1;
                    //Size = (int)((val4) * 1);

                    break;
            }
        }




    }
};


