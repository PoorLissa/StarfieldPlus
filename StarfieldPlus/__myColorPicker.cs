using System;
using System.Drawing;



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
            _rand = new Random((int)DateTime.Now.Ticks);
            _mode = mode;

            if (_mode < 0)
            {
                _mode = _rand.Next(5);
            }

            switch (_mode)
            {
                // Use Desktop Snapshot
                case 0:
                    getSnapshot(Width, Height);
                    break;

                // Use Custom Picture
                case 1:
                    getCustomPicture(Width, Height);
                    break;

                // Use Custom Color
                default:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        public void setMode(int mode)
        {
            _mode = mode;
        }

        // -------------------------------------------------------------------------

        public int getMode()
        {
            return _mode;
        }

        // -------------------------------------------------------------------------

        public Bitmap getImg()
        {
            return _img;
        }

        // -------------------------------------------------------------------------

        public void setPixel(int x, int y)
        {
            if (x > -1 && y > -1 && x < _img.Width && y < _img.Height)
                _img.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
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

        // -------------------------------------------------------------------------

        private string getRandomFile(string dir)
        {
            string res = "";

            if (System.IO.Directory.Exists(dir))
            {
                try
                {
                    string[] files = System.IO.Directory.GetFiles(dir);

                    if (files != null)
                    {
                        string[] Extensions = { ".bmp", ".gif", ".png", ".jpg" };

                        var list = new System.Collections.Generic.List<string>();

                        for (int i = 0; i < files.Length; i++)
                        {
                            var file = files[i];

                            foreach (var ext in Extensions)
                            {
                                if (file.EndsWith(ext))
                                {
                                    list.Add(file);
                                    break;
                                }
                            }
                        }

                        if (list.Count > 0)
                        {
                            int rnd = new Random((int)DateTime.Now.Ticks).Next(list.Count);
                            res = list[rnd];
                        }
                    }
                }
                catch (Exception)
                {
                    res = "";
                }
            }

            return res;
        }

        // -------------------------------------------------------------------------

        private void getSnapshot(int Width, int Height)
        {
            try
            {
                if (_img == null)
                {
                    _img = new Bitmap(Width, Height);

                    using (Graphics g = Graphics.FromImage(_img))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Width, Height));
                    }
                }
            }
            catch (Exception)
            {
                ;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void getCustomPicture(int Width, int Height)
        {
            try
            {
                string currentWallpaper = getRandomFile(StarfieldPlus.Program._imgPath);

                if (currentWallpaper != null && currentWallpaper != string.Empty)
                {
                    _img = new Bitmap(currentWallpaper);

                    if (_img.Width < Width || _img.Height < Height)
                    {
                        // Stretch the image, if its size is less than the desktop size
                        _img = new Bitmap(_img, Width, Height);
                    }
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception)
            {
                _img = null;
                _mode = 0;
                getSnapshot(Width, Height);
            }

            return;
        }

        // -------------------------------------------------------------------------
    }
};
