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

        // -------------------------------------------------------------------

        public Form1(Rectangle bounds)
        {
            InitializeComponent();

            Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Bounds = bounds;
            //this.TopMost = true;
            this.DoubleBuffered = true;

            my.myObject.Height = this.Height;
            my.myObject.Width = this.Width;

            this.MouseMove += Form1_MouseMove;
            this.KeyDown += Form1_KeyDown;

            getScreenSaverObject();
            RunScreensaver();
        }

        // -------------------------------------------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            appExit();
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
        // - divide all the screen as a grid. moving ball bounces from the walls and while under the ball, each cell is enlarged
        // - cover everything in spiralling traingles
        // - travelling sin and cosin functions - the same as moving lines

        private void getScreenSaverObject()
        {
            // Starfield has a slight priority over the others
            int id = new Random((int)DateTime.Now.Ticks).Next(18);

            id = 5;

            if (false)
            {
                _obj = new my.myObj_140();
                return;
            }

            if (false)
            {
                _obj = new my.myObj_060();
                return;
            }

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

                // Linearly Moving Circles (Soap Bubbles)
                case 2:
                    _obj = new my.myObj_020();
                    break;

                // Rain Drops
                case 3:
                    _obj = new my.myObj_030();
                    break;

                // Lines 1
                case 4:
                    _obj = new my.myObj_040();
                    break;

                // Lines 2
                case 5:
                    _obj = new my.myObj_004_b();
                    break;

                // Lines 3
                case 6:
                    _obj = new my.myObj_004_c();
                    break;

                // Desktop pieces swapping
                case 7:
                    _obj = new my.myObj_050();
                    break;

                // Desktop pieces falling off -- 1
                case 8:
                    _obj = new my.myObj_070();
                    break;

                // Desktop pieces falling off -- 2
                case 9:
                    _obj = new my.myObj_072();
                    break;

                // Big Bang
                case 10:
                    _obj = new my.myObj_100();
                    break;

                // Desktop -- Random pieces of the desktop are shown at their own slightly offset locations
                case 11:
                    _obj = new my.myObj_101();
                    break;

                // Desktop 2
                case 12:
                    _obj = new my.myObj_102();
                    break;

                // Desktop 3
                case 13:
                    _obj = new my.myObj_110();
                    break;

                // Moving Lines
                case 14:
                    _obj = new my.myObj_120();
                    break;

                // Growing shapes -- rain drops alike -- no refresh
                case 15:
                    _obj = new my.myObj_130();
                    break;

                // Growing shapes -- rain drops alike
                case 16:
                    _obj = new my.myObj_131();
                    break;

                // Splines
                case 17:
                    _obj = new my.myObj_132();
                    break;

                // Grid with moving rectangle lenses -- test, looks strange
                case 18:
                    _obj = new my.myObj_140();
                    break;

                default:
                    _obj = new my.myObj_000();
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------

        private void RunScreensaver()
        {
            my.myObject.Count = 333;

            new System.Threading.Tasks.Task(() => {

                _obj.Process(this);

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
