using System;
using System.Drawing;
using System.Windows.Forms;

// https://sites.harding.edu/fmccown/screensaver/screensaver.html

// What graphisc api to choose for C#
// https://www.reddit.com/r/csharp/comments/5uvyw9/what_can_i_use_for_basic_fast_2d_graphics/

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
            int id = new Random().Next(9);

            id = 4;

            switch (id)
            {
                // Stars
                case 0:
                    _obj = new my.myObj_000();
                    break;

                // Random roaming
                case 1:
                    _obj = new my.myObj_001();
                    break;

                // Circles
                case 2:
                    _obj = new my.myObj_002();
                    break;

                // Rain Drops
                case 3:
                    _obj = new my.myObj_003();
                    break;

                // Lines
                case 4:
                    _obj = new my.myObj_004();
                    break;

                // Lines
                case 5:
                    _obj = new my.myObj_004_a();
                    break;

                // Circle
                case 6:
                    _obj = new my.myObj_100();
                    break;

                // Desktop
                case 7:
                    _obj = new my.myObj_101();
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
