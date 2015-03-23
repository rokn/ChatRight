﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private static readonly IntPtr HWND_TOP = new IntPtr(0);
        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        private Image logoImage;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseOpen();
            logoImage = Image.FromFile("ChatRightLogo.png", false);
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

        private Point defaultPosition;
        private const int roundnessRadius = 30;
        private static Color MainBackgroundColor = Color.FromArgb(204, 39, 39);

        public MainForm()
        {
            InitializeComponent();
            InitializeLocation();
            InitializeControlButtons();
            //userText.BackColor = MainBackgroundColor;
            
        }

        private void InitializeLocation()
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Rectangle workingRect = Screen.PrimaryScreen.WorkingArea;
            Height = (int)(workingRect.Height * 0.8f);
            defaultPosition = new Point(workingRect.Width - this.Width, workingRect.Height / 2 - this.Height / 2);
            MoveWindow(defaultPosition);
            this.TopMost = true;
            this.BackColor = MainBackgroundColor;
            this.notifyIcon1 = new NotifyIcon();
            this.notifyIcon1.Icon = new Icon("Icon.ico");
            this.Icon = notifyIcon1.Icon;
        }

        private void InitializeControlButtons()
        {
            ToolTip closeTooltip = new ToolTip();
            closeTooltip.SetToolTip(CloseButton, "Exit ChatRight");
            CloseButton.Image = Bitmap.FromFile("CloseButton.png");
            CloseButton.BackColor = Color.FromArgb(100, 0, 0);
            CloseButton.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0, 0);
            CloseButton.FlatStyle = FlatStyle.Flat;

            ToolTip slideTooltip = new ToolTip();
            slideTooltip.SetToolTip(SlideButton, "Hide ChatRight");
            SlideButton.Image = Bitmap.FromFile("Slide.png");
            SlideButton.BackColor = Color.FromArgb(100, 0, 0);
            SlideButton.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0, 0);
            SlideButton.FlatStyle = FlatStyle.Flat;

            UnslideButton.Visible = false;
            UnslideButton.Height = Height - 2 * roundnessRadius;
            UnslideButton.Location = new Point(UnslideButton.Location.X, Height / 2 - UnslideButton.Height / 2);
            UnslideButton.Image = Bitmap.FromFile("Unslide.png");
            UnslideButton.BackColor = Color.FromArgb(100, 0, 0);
            UnslideButton.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0, 0);
            UnslideButton.FlatStyle = FlatStyle.Flat;

            ToolTip hideTooltip = new ToolTip();
            hideTooltip.SetToolTip(HideButton, "Minimize to tray");
            HideButton.Image = Bitmap.FromFile("Hide.png");
            HideButton.BackColor = Color.FromArgb(100, 0, 0);
            HideButton.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0, 0);
            HideButton.FlatStyle = FlatStyle.Flat;


            this.notifyIcon1.Text = "ChatRight";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            notifyIcon1.Visible = false;

        }

        private void MoveWindow(Point location)
        {
            IntPtr id;
            id = this.Handle;
            MoveWindow(id, location.X, location.Y, Width, Height, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath shape = new GraphicsPath();
            shape = CreateRoundRectangle(new Rectangle(0, 0, Width, Height), roundnessRadius, RectangleCorners.TopLeft | RectangleCorners.BottomLeft);
            this.Region = new Region(shape);
            Pen borderPen = new Pen(Color.Black, 6);
            this.TopMost = true;
            e.Graphics.DrawImage(logoImage, new Point(10, 10));
            e.Graphics.DrawPath(borderPen, shape);
            e.Graphics.DrawLine(borderPen, new Point(0, logoImage.Height + 10), new Point(Width, logoImage.Height + 10));
            base.OnPaint(e);
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

            //Bottom Left Corne
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private System.Windows.Forms.NotifyIcon notifyIcon1;

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

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            HideForm();
        }

        DatabaseConnection objConnect;
        string conString;
        DataSet dataSet;
        DataRow dataRow;
        int maxRows;
        int inc = 0;        

        private void NavigateRecords()
        {
            dataRow = dataSet.Tables[0].Rows[0];
            userText.Text = dataRow.ItemArray.GetValue(1).ToString();
            emailText.Text = dataRow.ItemArray.GetValue(2).ToString();
            passText.Text = dataRow.ItemArray.GetValue(3).ToString(); ;
        }
    }
}