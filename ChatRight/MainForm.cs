using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChatRight
{
    public enum RectangleCorners
    {
        None = 0, TopLeft = 1, TopRight = 2, BottomLeft = 4, BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public partial class MainForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private static Color MainBackgroundColor = Color.FromArgb(204, 39, 39);
        private const int roundnessRadius = 30;
        private const float borderSize = 6f;

        private DatabaseConnection objConnect;
        private string conString;
        private DataRow dataRow;
        private DataSet dataSet;
        private Point defaultPosition;
        private int maxRows;

        private Image logoImage;
        private NotifyIcon notifyIcon1;

        private GraphicsPath windowShape;
        private Pen borderPen;

        public MainForm()
        {
            InitializeComponent();
            InitializeLocation();
            InitializeControlButtons();
        }

        private void InitializeControlButtons()
        {
            Color ButtonBackColor = Color.FromArgb(100, 0, 0);
            Color ButtonBorderColor = Color.FromArgb(255, 0, 0, 0);

            ToolTip closeTooltip = new ToolTip();
            closeTooltip.SetToolTip(CloseButton, "Exit ChatRight");
            CloseButton.Image = Bitmap.FromFile(@"Resources/CloseButton.png");
            CloseButton.BackColor = ButtonBackColor;
            CloseButton.FlatAppearance.BorderColor = ButtonBorderColor;
            CloseButton.FlatStyle = FlatStyle.Flat;

            ToolTip slideTooltip = new ToolTip();
            slideTooltip.SetToolTip(SlideButton, "Hide ChatRight");
            SlideButton.Image = Bitmap.FromFile(@"Resources/Slide.png");
            SlideButton.BackColor = ButtonBackColor;
            SlideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            SlideButton.FlatStyle = FlatStyle.Flat;

            UnslideButton.Visible = false;
            UnslideButton.Height = Height - 2 * roundnessRadius;
            UnslideButton.Location = new Point(UnslideButton.Location.X, Height / 2 - UnslideButton.Height / 2);
            UnslideButton.Image = Bitmap.FromFile(@"Resources/Unslide.png");
            UnslideButton.BackColor = ButtonBackColor;
            UnslideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            UnslideButton.FlatStyle = FlatStyle.Flat;

            ToolTip hideTooltip = new ToolTip();
            hideTooltip.SetToolTip(HideButton, "Minimize to tray");
            HideButton.Image = Bitmap.FromFile(@"Resources/Hide.png");
            HideButton.BackColor = ButtonBackColor;
            HideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            HideButton.FlatStyle = FlatStyle.Flat;

            notifyIcon1 = new NotifyIcon();
            this.Icon = notifyIcon1.Icon;
            notifyIcon1.Text = "ChatRight";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            notifyIcon1.Icon = new Icon(@"Resources\Icon.ico");
            notifyIcon1.Visible = false;
        }

        private void InitializeLocation()
        {
            FormBorderStyle = FormBorderStyle.None;
            Rectangle workingRect = Screen.PrimaryScreen.WorkingArea;
            Height = (int)(workingRect.Height * 0.8f);
            defaultPosition = new Point(workingRect.Width - this.Width, workingRect.Height / 2 - this.Height / 2);
            MoveWindow(defaultPosition);

            BackColor = MainBackgroundColor;
            ShowInTaskbar = false;

            windowShape = CreateRoundRectangle(new Rectangle(0, 0, Width, Height), roundnessRadius, RectangleCorners.TopLeft | RectangleCorners.BottomLeft);
            this.Region = new Region(windowShape);
            borderPen = new Pen(Color.Black, borderSize);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseOpen();
            logoImage = Image.FromFile(@"Resources\ChatRightLogo.png", false);
        }

        public GraphicsPath CreateRoundRectangle(Rectangle r, int radius,
                                  RectangleCorners corners)
        {
            Rectangle tlc = new Rectangle(r.Left, r.Top, 2 * radius,
                                          2 * radius);
            Rectangle trc = tlc;
            trc.X = r.Right - 2 * radius;
            Rectangle blc = tlc;
            blc.Y = r.Bottom - 2 * radius;
            Rectangle brc = blc;
            brc.X = r.Right - 2 * radius;

            Point[] n = new Point[] {
            new Point(tlc.Left, tlc.Bottom), tlc.Location,
            new Point(tlc.Right, tlc.Top), trc.Location,
            new Point(trc.Right, trc.Top),
            new Point(trc.Right, trc.Bottom),
            new Point(brc.Right, brc.Top),
            new Point(brc.Right, brc.Bottom),
            new Point(brc.Left, brc.Bottom),
            new Point(blc.Right, blc.Bottom),
            new Point(blc.Left, blc.Bottom), blc.Location
            };

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            //Top Left Corner
            if ((RectangleCorners.TopLeft & corners)
                == RectangleCorners.TopLeft)
            {
                p.AddArc(tlc, 180, 90);
            }
            else
            {
                p.AddLines(new Point[] { n[0], n[1], n[2] });
            }

            //Top Edge
            p.AddLine(n[2], n[3]);

            //Top Right Corner
            if ((RectangleCorners.TopRight & corners)
                == RectangleCorners.TopRight)
            {
                p.AddArc(trc, 270, 90);
            }
            else
            {
                p.AddLines(new Point[] { n[3], n[4], n[5] });
            }

            //Right Edge
            p.AddLine(n[5], n[6]);

            //Bottom Right Corner
            if ((RectangleCorners.BottomRight & corners)
                == RectangleCorners.BottomRight)
            {
                p.AddArc(brc, 0, 90);
            }
            else
            {
                p.AddLines(new Point[] { n[6], n[7], n[8] });
            }

            //Bottom Edge
            p.AddLine(n[8], n[9]);

            //Bottom Left Corner
            if ((RectangleCorners.BottomLeft & corners)
                == RectangleCorners.BottomLeft)
            {
                p.AddArc(blc, 90, 90);
            }
            else
            {
                p.AddLines(new Point[] { n[9], n[10], n[11] });
            }

            //Left Edge
            p.AddLine(n[11], n[0]);

            p.CloseFigure();
            return p;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(logoImage, new Point(10, 10));
            e.Graphics.DrawPath(borderPen, windowShape);
            e.Graphics.DrawLine(borderPen, new Point(0, logoImage.Height + 10), new Point(Width, logoImage.Height + 10));

            this.TopMost = true; // HUE HUE HUE

            base.OnPaint(e);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabaseOpen()
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.UsersConnectionString;

                objConnect.connection_string = conString;
                objConnect.Sql = Properties.Settings.Default.SQL;

                dataSet = objConnect.GetConnection;

                maxRows = dataSet.Tables[0].Rows.Count;

                NavigateRecords();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            HideForm();
        }

        private void SlideButton_Click(object sender, EventArgs e)
        {
            UnslideButton.Visible = true;
            Point newPos = new Point(defaultPosition.X + Width - 27, defaultPosition.Y);
            MoveWindow(newPos);
        }

        private void UnslideButton_Click(object sender, EventArgs e)
        {
            UnslideButton.Visible = false;
            MoveWindow(defaultPosition);
        }

        private void HideForm()
        {
            notifyIcon1.BalloonTipTitle = "ChatRight";
            notifyIcon1.BalloonTipText = "The app is still running";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(200);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // |
        // |    MAGIC.DLL
        // |
        //\_/

        protected override CreateParams CreateParams
        {
            get
            {
                // Turn on WS_EX_TOOLWINDOW style bit
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void MoveWindow(Point location)
        {
            IntPtr id;
            id = this.Handle;
            MoveWindow(id, location.X, location.Y, Width, Height, true);
        }

        private void NavigateRecords()
        {
            dataRow = dataSet.Tables[0].Rows[0];
            userText.Text = dataRow.ItemArray.GetValue(1).ToString();
            emailText.Text = dataRow.ItemArray.GetValue(2).ToString();
            passText.Text = dataRow.ItemArray.GetValue(3).ToString(); ;
        }
    }
}