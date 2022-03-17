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
        public static int Width  { get; set; }
        public static int Height { get; set; }
        public static int Count  { get; set; }

        public int X        { get; set; }
        public int Y        { get; set; }
        public int Size     { get; set; }

        protected static Bitmap _originalScreen = null;
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


namespace my
{
    public class myColorPicker
    {
        private int _mode = -1;
        private Bitmap _img = null;
        private Random _rand = null;

        private static int gl_R = 0, gl_G = 0, gl_B = 0;

        // -------------------------------------------------------------------------

        public myColorPicker(int Width, int Height, int mode = -1)
        {
            _rand = new Random();
            _mode = mode;

            if (_mode < 0)
            {
                _mode = _rand.Next(5);
            }

            switch (_mode)
            {
                // Use Desktop Snapshot
                case 0:
                    if (_img == null)
                    {
                        _img = new Bitmap(Width, Height);

                        using (Graphics g = Graphics.FromImage(_img))
                        {
                            g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Width, Height));
                        }
                    }
                    break;

                // Use Custom Picture
                case 1:

                    try
                    {
                        string currentWallpaper = "E:\\iNet\\pix\\wallpapers_3840x1600\\_dsc_00341_0_ultrawide.jpg";
                        currentWallpaper = "E:\\iNet\\pix\\wallpapers_3840x1600\\0jcj71fni5111.png";
                        currentWallpaper = "E:\\iNet\\pix\\05-01-11.gif";
                        currentWallpaper = "C:\\_maxx\\ukraine.png";

                        _img = new Bitmap(currentWallpaper);

                        if (_img.Width < Width || _img.Height < Height)
                        {
                            // Stretch it, if it is less than the desktop size
                            _img = new Bitmap(_img, Width, Height);
                        }
                    }
                    catch (Exception)
                    {
                        _img = null;
                        _mode++;
                    }
                    break;

                // Use Random Color
                default:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        public void getColor(int x, int y, ref int R, ref int G, ref int B)
        {
            switch (_mode)
            {
                // Get Color from Image
                case 0:
                case 1:

                    if (_img != null)
                    {
                        var pixel = _img.GetPixel(x, y);

                        R = pixel.R;
                        G = pixel.G;
                        B = pixel.B;
                    }
                    break;

                // Shades of a Single Random Color
                case 2:

                    while (gl_R + gl_G + gl_B < 500)
                    {
                        // Run once per session
                        gl_R = _rand.Next(256);
                        gl_G = _rand.Next(256);
                        gl_B = _rand.Next(256);
                    }

                    R = gl_R;
                    G = gl_G;
                    B = gl_B;
                    break;

                // Random Color
                case 3:
                    R = _rand.Next(256);
                    G = _rand.Next(256);
                    B = _rand.Next(256);
                    break;

                // Shades of Gray
                default:
                    R = _rand.Next(256);
                    G = R;
                    B = G;
                    break;
            }

            return;
        }

    }
};
