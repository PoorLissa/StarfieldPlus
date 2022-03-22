using System;
using System.Drawing;



namespace my
{
    public interface iMyObject
    {
        void Move();
    };


    public class myObject : iMyObject
    {
        public static int Width  { get; set; }
        public static int Height { get; set; }
        public static int Count  { get; set; }

        public int X        { get; set; }
        public int Y        { get; set; }
        public int Size     { get; set; }

        protected static myColorPicker _colorPicker = null;
        protected static Random         rand = new Random((int)DateTime.Now.Ticks);

        // -------------------------------------------------------------------------

        public myObject()
        {
        }

        // -------------------------------------------------------------------------

        public virtual void Move()
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
