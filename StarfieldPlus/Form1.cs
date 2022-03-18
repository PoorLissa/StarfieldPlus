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
        private bool        _isAlive = false;
        private Point       _oldMouseLocation;
        public my.myObject _obj = null;

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

        private void getScreenSaverObject()
        {
            // Starfield has a slight priority over the others
            int id = new Random().Next(19);

            id = 2;

            switch (id)
            {
                // Star Field
                case 0:
                    _obj = new my.myObj_000();
                    break;

                // Randomly Roaming Squares (Snow Like)
                case 1:
                    _obj = new my.myObj_001();
                    break;

                // Linearly Moving Circles (Soap Bubbles)
                case 2:
                    _obj = new my.myObj_002();
                    break;

                // Rain Drops
                case 3:
                    _obj = new my.myObj_003();
                    break;

                // Lines 1
                case 4:
                    _obj = new my.myObj_004();
                    break;

                // Lines 2
                case 5:
                    _obj = new my.myObj_004_a();
                    break;

                // Lines 3
                case 6:
                    _obj = new my.myObj_004_b();
                    break;

                // Lines 3
                case 7:
                    _obj = new my.myObj_004_c();
                    break;

                // Circle
                case 8:
                    _obj = new my.myObj_100();
                    break;

                // Desktop 1
                case 9:
                    _obj = new my.myObj_101();
                    break;

                // Desktop 2
                case 10:
                    _obj = new my.myObj_102();
                    break;

                // Desktop 3
                case 11:
                    _obj = new my.myObj_103();
                    break;

                // Desktop 4
                case 12:
                    _obj = new my.myObj_104();
                    break;

                // Desktop 5
                case 13:
                    _obj = new my.myObj_105();
                    break;

                // Desktop 5
                case 14:
                    _obj = new my.myObj_110();
                    break;

                // Moving Lines
                case 15:
                    _obj = new my.myObj_120();
                    break;

                // Growing shapes -- rain drops alike -- no refresh
                case 16:
                    _obj = new my.myObj_130();
                    break;

                // Growing shapes -- rain drops alike
                case 17:
                    _obj = new my.myObj_131();
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
            _isAlive = true;
            my.myObject.Count = 333;

            new System.Threading.Tasks.Task(() => {

                _obj.Process(this, ref _isAlive);

            }).Start();

            return;
        }

        // -------------------------------------------------------------------

        private void appExit()
        {
            this.MouseMove -= Form1_MouseMove;
            this.KeyDown -= Form1_KeyDown;

            _isAlive = false;

            while (_isAlive == false)
                ;

            Application.Exit();
        }

        // -------------------------------------------------------------------
    }
}
