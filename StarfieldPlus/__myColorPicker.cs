using System;
using System.Drawing;



namespace my
{
    public class myColorPicker
    {
        private int      _mode = -1;
        private Bitmap   _img = null;
        private Random   _rand = null;
        private Graphics _g = null;

        private static int gl_R = 0, gl_G = 0, gl_B = 0;

        private enum scaleParams { scaleToWidth, scaleToHeight };

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

        ~myColorPicker()
        {
            if (_g != null)
            {
                _g.Dispose();
                _g = null;
            }

            if (_img != null)
            {
                _img.Dispose();
                _img = null;
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

        public Graphics GetGraphics()
        {
            return _g;
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
                // Get Color from Snapshot
                case 0:
                // Get Color from custom image
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

        // Select a random image file from a certain directory
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
                        string[] Extensions = { ".bmp", ".gif", ".png", ".jpg", ".jpeg" };

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

        // Take a snapshot of a current desktop
        private void getSnapshot(int Width, int Height)
        {
            try
            {
                if (_img == null)
                {
                    _img = new Bitmap(Width, Height);

                    if (_g != null)
                    {
                        _g.Dispose();
                        _g = null;
                    }

                    _g = Graphics.FromImage(_img);
                    _g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Width, Height));
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

                    //if (_img.Width <= Width || _img.Height <= Height)
                    {
                        // Stretch the image, if its size is less than the desktop size

                        // todo: see why some of my 3840x1600 images are displayed incorrectly if not resized here

                        _img = resizeImage(_img, Width, Height, scaleParams.scaleToWidth);
                    }

                    if (_g != null)
                    {
                        _g.Dispose();
                        _g = null;
                    }

                    _g = Graphics.FromImage(_img);
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception)
            {
                _img = null;
                _g = null;
                _mode = 0;
                getSnapshot(Width, Height);
            }

            return;
        }

        // -------------------------------------------------------------------------

        // High quality resize: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        private Bitmap resizeImage(Image src, int width, int height, scaleParams scaleParam)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            //destImage.SetResolution(src.HorizontalResolution, src.VerticalResolution);

            float desktopRatio = (float)width / (float)height;
            float srcRatio = (float)src.Width / (float)src.Height;
            float ratioDiff = Math.Abs(desktopRatio - srcRatio);

            // todo: need to find out what max ratio value do we need here
            if (ratioDiff > 0.05f)
            {
                switch (scaleParam)
                {
                    case scaleParams.scaleToHeight:
                        destRect.Width = src.Width * height / src.Height;

                        if (destRect.Width < width)
                        {
                            destRect.X = (width - destRect.Width) / 2;
                        }
                        break;

                    case scaleParams.scaleToWidth:
                        destRect.Height = src.Height * width / src.Width;

                        if (destRect.Height > height)
                        {
                            int yOffset = destRect.Height - height;
                            yOffset = _rand.Next(yOffset);
                            destRect.Y = -yOffset;
                        }
                        break;
                }
            }

            using (var gr = Graphics.FromImage(destImage))
            {
                gr.CompositingMode    = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.InterpolationMode  = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode      = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.PixelOffsetMode    = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    gr.DrawImage(src, destRect, 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        // -------------------------------------------------------------------------

        // Get new random color and update the brush with this color instantly
        public void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                R = _rand.Next(max);
                G = _rand.Next(max);
                B = _rand.Next(max);
            }

            br.Color = Color.FromArgb(alpha, R, G, B);
        }

        // -------------------------------------------------------------------------

        // Get new random color and then gradually get closer to it with each iteration, until the color value is matched
        // Update the brush with the current color on each iteration
        public bool getNewBrush(SolidBrush br, bool doGenerate, int minVal = 100)
        {
            if (doGenerate)
            {
                gl_R = gl_G = gl_B = 0;

                while (gl_R + gl_G + gl_B < minVal)
                {
                    gl_R = _rand.Next(256);
                    gl_G = _rand.Next(256);
                    gl_B = _rand.Next(256);
                }
            }

            int r = br.Color.R;
            int g = br.Color.G;
            int b = br.Color.B;

            r += r == gl_R ? 0 : r > gl_R ? -1 : 1;
            g += g == gl_G ? 0 : g > gl_G ? -1 : 1;
            b += b == gl_B ? 0 : b > gl_B ? -1 : 1;

            br.Color = Color.FromArgb(255, r, g, b);

            return r == gl_R && g == gl_G && b == gl_B;
        }

        // -------------------------------------------------------------------------
    }
};
