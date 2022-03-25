using System;
using System.Drawing;
using System.Drawing.Imaging;

/*
    - Random pieces of the desktop are shown at their own slightly offset locations
*/

namespace my
{
    public class myObj_101 : myObject
    {
        int offset = 0, maxX = 0, maxY = 0, maxZ = 0;
        int showMode = 2;
        int randomSize = 2;
        int A = 55;
        int x, y, w, h;

        Rectangle srcRect  = new Rectangle(1, 1, 1, 1);
        Rectangle destRect = new Rectangle(1, 1, 1, 1);

        public myObj_101()
        {
            if (colorPicker == null)
            {
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));

                showMode = rand.Next(showMode);
                randomSize = rand.Next(randomSize);
                A = 255 - rand.Next(A);

                Log($"myObj_101: colorPicker({colorPicker.getMode()}), showMode({showMode})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            maxX = rand.Next(100) + 1;
            maxY = rand.Next(100) + 1;
            maxZ = rand.Next(100) + 0;

            offset = rand.Next(25) + 5;

            if (offset % 2 == 0)
                offset++;

            w = rand.Next(maxX) + maxZ;
            h = rand.Next(maxY) + maxZ;

            srcRect.Width = w;
            srcRect.Height = h;

            destRect.Width = w;
            destRect.Height = h;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            x = rand.Next(Width);
            y = rand.Next(Height);

            switch (randomSize)
            {
                case 0:
                    w = rand.Next(maxX) + maxZ;
                    h = rand.Next(maxY) + maxZ;

                    srcRect.X = x;
                    srcRect.Y = y;
                    srcRect.Width = w;
                    srcRect.Height = h;

                    destRect.X = x + rand.Next(offset) - offset / 2;
                    destRect.Y = y + rand.Next(offset) - offset / 2;
                    destRect.Width = w;
                    destRect.Height = h;
                    break;

                case 1:
                    srcRect.X = x;
                    srcRect.Y = y;

                    destRect.X = x + rand.Next(offset) - offset / 2;
                    destRect.Y = y + rand.Next(offset) - offset / 2;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            switch (showMode)
            {
                case 0:
                    g.DrawImage(colorPicker.getImg(), destRect, srcRect, GraphicsUnit.Pixel);
                    break;

                case 1:
                    drawTransparentImg();
                    break;
            }
        }

        // -------------------------------------------------------------------------

        private void drawTransparentImg()
        {
            const int bytesPerPixel = 4;

            Bitmap bmp = new Bitmap(srcRect.Width, srcRect.Height);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.DrawImage(colorPicker.getImg(), new Rectangle(0, 0, bmp.Width, bmp.Height), srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel);
            }

            // Specify a pixel format
            PixelFormat pxf = PixelFormat.Format32bppArgb;

            // Lock the bitmap's bits
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Get the address of the first line
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha)
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            // Copy the ARGB values into the array
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

            // Manipulate the bitmap, such as changing the RGB values for all pixels in the the bitmap
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)

                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;

                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value

                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * A);
            }

            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

            // Unlock the bits
            bmp.UnlockBits(bmpData);

            g.DrawImage(bmp, destRect.X, destRect.Y);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            if (rand.Next(2) == 0)
            {
                g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            }
            else
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            }

            int cnt = 10000, t = 0;

            while (isAlive)
            {
                Move();
                Show();

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (--cnt == 0)
                {
                    cnt = 10000;
                    t += 1;
                }
            }

            return;
        }
    }
};
