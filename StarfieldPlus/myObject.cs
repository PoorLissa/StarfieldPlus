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
        static public int Width  { get; set; }
        static public int Height { get; set; }
        static public int Count  { get; set; }

        public int X        { get; set; }
        public int Y        { get; set; }
        public int Size     { get; set; }

        public static Random rand = new Random();

        // -------------------------------------------------------------------------

        public virtual void Move()
        {
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public static void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
        }
    };
};
