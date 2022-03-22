using System;
using System.Drawing;



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
        protected virtual void Process(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
        }

        // -------------------------------------------------------------------------

        public void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            // Using form's background image as our drawing surface

            Bitmap bmp = new Bitmap(Width, Height);             // set the size of the image
            form.BackgroundImage = bmp;                         // set the Form's image to be the buffer
            g = Graphics.FromImage(bmp);                        // set the graphics to draw on the image


            Process(form, g, ref isAlive);


            bmp.Dispose();
            g.Dispose();

            isAlive = true;

            return;
        }

        // -------------------------------------------------------------------------
    };
};
