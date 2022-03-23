using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;



namespace my
{
    public class myObject
    {
        public static int Width  { get; set; }
        public static int Height { get; set; }
        public static int Count  { get; set; }

        public int X        { get; set; }
        public int Y        { get; set; }
        public int Size     { get; set; }

        // -------------------------------------------------------------------------

        protected static myColorPicker  colorPicker = null;
        protected static Random         rand = new Random((int)DateTime.Now.Ticks);
        protected static Graphics       g = null;
        protected static Form           form = null;
        protected static Font           f = null;
        protected static bool           isAlive = true;

        // -------------------------------------------------------------------------

        public myObject()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void Move()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void Show()
        {
        }

        // -------------------------------------------------------------------------

        // Override it for every derived class to implement the logic
        protected virtual void Process()
        {
        }

        // -------------------------------------------------------------------------

        public void Process(System.Windows.Forms.Form mainForm)
        {
            form = mainForm;
            isAlive = true;

            // Using form's background image as our drawing surface
            try
            {
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

                Bitmap bmp = new Bitmap(Width, Height);             // set the size of the image
                form.BackgroundImage = bmp;                         // set the Form's image to be the buffer
                g = Graphics.FromImage(bmp);                        // set the graphics to draw on the image

                Process();

                g.Dispose();
                bmp.Dispose();

                g = null;
                bmp = null;
            }
            finally
            {
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            }

            isAlive = true;

            return;
        }

        // -------------------------------------------------------------------------

        public void Stop()
        {
            isAlive = false;

            while (isAlive == false);

            return;
        }

        // -------------------------------------------------------------------------

        protected void Log(string str)
        {
#if DEBUG
            using (System.IO.StreamWriter sw = System.IO.File.AppendText("zzz.log"))
            {
                sw.WriteLine(str);
            }
#endif
        }

        // -------------------------------------------------------------------------
    };
};
