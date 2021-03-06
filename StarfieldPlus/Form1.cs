using System;
using System.Drawing;
using System.Windows.Forms;


// ================================================================================================
// Creating a Screen Saver with C#
// https://sites.harding.edu/fmccown/screensaver/screensaver.html

// What graphisc api to choose for C#
// https://www.reddit.com/r/csharp/comments/5uvyw9/what_can_i_use_for_basic_fast_2d_graphics/
// ================================================================================================



namespace StarfieldPlus
{
    public partial class Form1 : Form
    {
        private Point       _oldMouseLocation;
        private my.myObject _obj = null;

        private bool isRelease =
#if RELEASE
            true;
#else
            false;
#endif

        // -------------------------------------------------------------------

        public Form1(Rectangle bounds)
        {
            InitializeComponent();

            Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Bounds = bounds;
            this.TopMost = isRelease;
            this.DoubleBuffered = true;

            my.myObject.Height = this.Height;
            my.myObject.Width = this.Width;

            this.MouseMove += Form1_MouseMove;
            this.KeyDown += Form1_KeyDown;

            this.KeyPreview = true;

            getScreenSaverObject();
            RunScreensaver();
        }

        // -------------------------------------------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    my.myObject.ShowInfo = !my.myObject.ShowInfo;
                    break;

                case Keys.Space:
                    my.myObject.GetNext = !my.myObject.GetNext;
                    break;

                default:
                    appExit();
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int dist = 50;

            if (!_oldMouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(_oldMouseLocation.X - e.X) > dist || Math.Abs(_oldMouseLocation.Y - e.Y) > dist)
                {
                    appExit();
                }
            }

            // Update mouse location
            _oldMouseLocation = e.Location;
        }

        // -------------------------------------------------------------------

        // todo:
        // - divide the screen in squares and swap them randomly
        // - gravity
        // - sort all the screen pixels
        // - moving stripes (from top to bottom, for example)
        // - gravity, where the color of a pixel is its mass
        // - posterization (color % int)
        // - divide in squares and each square gets its own blur factor
        // - sperm floating towards the center
        // - cover everything in spiralling traingles
        // - try bezier curves: https://en.wikipedia.org/wiki/B%C3%A9zier_curve
        // - try rotating rectangles: https://stackoverflow.com/questions/10210134/using-a-matrix-to-rotate-rectangles-individually
        // - something like myObj_101, but the pieces are moved via sine/cosine function (up-down or elliptically)
        // - randomly generate points. Every point grows its own square (with increasing or decreasing opacity). Grown squares stay a while then fade away. Example: myobj040 + moveType = 1 + shape = 0 + Show == g.FillRectangle(br, X, Y, Size, Size);
        // - bouncing ball, but its trajctory is not straight line, but curved like in obj_040
        // - moving ponts generator, where the moment of generation depends on sin(time)
        // - battle ships
        // - grid over an image. grid pulses, increasing and decreasing its cells size. each cell is displaying average img color
        // - bouncing ball and lots of triangles rotating to point to it
        // - mandlebrot

        private void getScreenSaverObject()
        {
            int id = -1;

#if DEBUG
            id = 23;
            id = 19;
            id = 18;

#if false
            _obj = new my.distributionTester();
            return;
#endif

#if false
            _obj = new my.myObj_170();
            return;
#endif
#endif
start:
            this.Text = id.ToString();

            switch (id)
            {
                // Star Field
                case 0:
                    _obj = new my.myObj_000();
                    break;

                // Randomly Roaming Squares (Snow Like)
                case 1:
                    _obj = new my.myObj_010();
                    break;

                // Randomly Roaming Lines (based on Randomly Roaming Squares)
                case 2:
                    _obj = new my.myObj_011();
                    break;

                // Linearly Moving Circles (Soap Bubbles)
                case 3:
                    _obj = new my.myObj_020();
                    break;

                // Rain Drops
                case 4:
                    _obj = new my.myObj_030();
                    break;

                // Lines 1
                case 5:
                    _obj = new my.myObj_040();
                    break;

                // Lines 2
                case 6:
                    _obj = new my.myObj_004_b();
                    break;

                // Lines 3 -- Patchwork / Micro Schematics
                case 7:
                    _obj = new my.myObj_004_c();
                    break;

                // Various shapes growing out from a single starting point
                case 8:
                    _obj = new my.myObj_004_d();
                    break;

                // Desktop pieces swapping
                case 9:
                    _obj = new my.myObj_050();
                    break;

                // Desktop pieces falling off -- 1
                case 10:
                    _obj = new my.myObj_070();
                    break;

                // Desktop pieces falling off -- 2
                case 11:
                    _obj = new my.myObj_072();
                    break;

                // Big Bang
                case 12:
                    _obj = new my.myObj_100();
                    break;

                // Desktop -- Random pieces of the desktop are shown at their own slightly offset locations
                case 13:
                    _obj = new my.myObj_101();
                    break;

                // Desktop 2
                case 14:
                    _obj = new my.myObj_102();
                    break;

                // Desktop 3
                case 15:
                    _obj = new my.myObj_110();
                    break;

                // Moving Lines
                case 16:
                    _obj = new my.myObj_120();
                    break;

                // Growing shapes -- rain drops alike -- no refresh
                case 17:
                    _obj = new my.myObj_130();
                    break;

                // Growing shapes -- rain drops alike
                case 18:
                    _obj = new my.myObj_131();
                    break;

                // Splines
                case 19:
                    _obj = new my.myObj_132();
                    break;

                // Grid with moving rectangle lenses -- test, looks strange
                case 20:
                    _obj = new my.myObj_140();
                    break;

                // Cellular Automaton: Conway's Life
                case 21:
                    _obj = new my.myObj_150();
                    break;

                // Desktop: Ever fading away pieces
                case 22:
                    _obj = new my.myObj_160();
                    break;

                // Desktop: Diminishing pieces
                case 23:
                    _obj = new my.myObj_170();
                    break;

                // Points generator
                case 24:
                    _obj = new my.myObj_180();
                    break;

                default:
                    id = new Random((int)DateTime.Now.Ticks).Next(1 + 24);
                    goto start;
            }

            return;
        }

        // -------------------------------------------------------------------

        private void RunScreensaver()
        {
            my.myObject.Count = 333;

            new System.Threading.Tasks.Task(() => {

                try
                {

                    _obj.Process(this);

                }
                catch (Exception ex)
                {

                    this.Bounds = new Rectangle(100, 100, 666, 333);
                    MessageBox.Show($"{_obj.ToString()} says:\n{ex.Message}", "Process Exception}", MessageBoxButtons.OK);

                }

            }).Start();

            return;
        }

        // -------------------------------------------------------------------

        private void appExit()
        {
            _obj.Stop();

            this.MouseMove -= Form1_MouseMove;
            this.KeyDown -= Form1_KeyDown;

            Application.Exit();
        }

        // -------------------------------------------------------------------
    }
}
