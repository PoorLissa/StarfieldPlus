using System;
using System.Drawing;



namespace my
{
    public interface iMyObject
    {
        void Move();
        void getImage();
    };


    public class myObject : iMyObject
    {
        static public int Width  { get; set; }
        static public int Height { get; set; }
        static public int Count  { get; set; }

        public int X        { get; set; }
        public int Y        { get; set; }
        public int Size     { get; set; }

        protected Bitmap _originalScreen = null;

        protected static Random rand = new Random();

        // -------------------------------------------------------------------------

        public myObject()
        {
            getImage();
        }

        // -------------------------------------------------------------------------

        public virtual void Move()
        {
            ;
        }

        // -------------------------------------------------------------------------

        // Override this method for those child classes that actually need a snapshot or an image
        public virtual void getImage()
        {
            ;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public virtual void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            ;
        }

        // -------------------------------------------------------------------------
    };
};
