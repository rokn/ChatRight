﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChatRight
{
    public enum RectangleCorners
    {
        None = 0, TopLeft = 1, TopRight = 2, BottomLeft = 4, BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public enum MenuType
    {
        StartUp, Register, SignIn, Activation, MainScreen, Contacts, Profile, None
    }

    public partial class MainForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private static MainTextBox mainChatBox;
        public static string UserName;
        public static Font MainFont;
        public static Font messageFont;
        private static Color MainBackgroundColor = Color.FromArgb(204, 39, 39);
        private const int roundnessRadius = 30;
        private const float borderSize = 6f;
        private static int logoHeight;
        private static int workingAreaHeight;
        private static MenuType currentMenu;
        private static Dictionary<MenuType, List<Control>> Menus;
        private Point defaultPosition;
        private bool contactsShown;

        private Image logoImage;
        private NotifyIcon notifyIcon1;

        private GraphicsPath windowShape;
        private Pen borderPen;
        private List<Control> staticControls;

        private PrivateFontCollection fontCollection;
        private Timer networkUpdateTimer = new Timer();

        private MainTextBox chatBox;

        public MainForm()
        {
            InitializeComponent();
            InitializeMenus();
            InitializeLocation();
            InitializeControls();
            InitializeNetworking();
        }

        private void InitializeNetworking()
        {
            networkUpdateTimer.Interval = 20;
            networkUpdateTimer.Start();
            networkUpdateTimer.Tick += networkUpdateTimer_Tick;
            NetworkingClient.InitializeClient("37.157.138.76");
        }

        private void networkUpdateTimer_Tick(object sender, EventArgs e)
        {
            NetworkingClient.Update();
        }

        private void InitializeMenus()
        {
            currentMenu = MenuType.None;
            Menus = new Dictionary<MenuType, List<Control>>();
            for (int i = 0; i < Enum.GetNames(typeof(MenuType)).Length; i++)
            {
                Menus.Add((MenuType)i, new List<Control>());
            }

            staticControls = new List<Control>();
        }

        private void InitializeControls()
        {
            contactsShown = false;

            mainChatBox = new MainTextBox();
            this.Controls.Add(mainChatBox);

            chatBox = new MainTextBox(new Size(212, 54));
            this.Controls.Add(chatBox);

            foreach (Control control in Controls)
            {
                control.Visible = false;
            }

            logoImage = Image.FromFile(@"Resources\ChatRightLogo.png", false);
            logoHeight = logoImage.Height + 10;
            workingAreaHeight = Height - logoHeight;

            fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(@"Resources/Aaargh.ttf");
            MainFont = new Font(fontCollection.Families[0], 15);
            messageFont = new Font(fontCollection.Families[0], 12);

            Color ButtonBackColor = Color.FromArgb(100, 0, 0);
            Color ButtonBorderColor = Color.FromArgb(255, 0, 0, 0);

            ToolTip closeTooltip = new ToolTip();
            closeTooltip.SetToolTip(CloseButton, "Exit ChatRight");
            CloseButton.Image = Bitmap.FromFile(@"Resources/CloseButton.png");
            CloseButton.BackColor = ButtonBackColor;
            CloseButton.FlatAppearance.BorderColor = ButtonBorderColor;
            CloseButton.FlatStyle = FlatStyle.Flat;
            staticControls.Add(CloseButton);

            ToolTip slideTooltip = new ToolTip();
            slideTooltip.SetToolTip(SlideButton, "Hide ChatRight");
            SlideButton.Image = Bitmap.FromFile(@"Resources/Slide.png");
            SlideButton.BackColor = ButtonBackColor;
            SlideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            SlideButton.FlatStyle = FlatStyle.Flat;
            staticControls.Add(SlideButton);

            UnslideButton.Visible = false;
            UnslideButton.Height = Height - 2 * roundnessRadius;
            UnslideButton.Location = new Point(UnslideButton.Location.X, Height / 2 - UnslideButton.Height / 2);
            UnslideButton.Image = Bitmap.FromFile(@"Resources/Unslide.png");
            UnslideButton.BackColor = ButtonBackColor;
            UnslideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            UnslideButton.FlatStyle = FlatStyle.Flat;
            staticControls.Add(UnslideButton);

            ToolTip hideTooltip = new ToolTip();
            hideTooltip.SetToolTip(HideButton, "Minimize to tray");
            HideButton.Image = Bitmap.FromFile(@"Resources/Hide.png");
            HideButton.BackColor = ButtonBackColor;
            HideButton.FlatAppearance.BorderColor = ButtonBorderColor;
            HideButton.FlatStyle = FlatStyle.Flat;
            staticControls.Add(HideButton);

            SendMessageButton.Image = Bitmap.FromFile(@"Resources/Slide.png");
            SendMessageButton.BackColor = ButtonBackColor;
            SendMessageButton.FlatAppearance.BorderColor = ButtonBorderColor;
            SendMessageButton.FlatStyle = FlatStyle.Flat;
            SendMessageButton.Location = new Point(SendMessageButton.Location.X, (Height - roundnessRadius) - SendMessageButton.Height);
            Menus[MenuType.MainScreen].Add(SendMessageButton);

            notifyIcon1 = new NotifyIcon();
            this.Icon = notifyIcon1.Icon;
            notifyIcon1.Text = "ChatRight";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            notifyIcon1.Icon = new Icon(@"Resources\Icon.ico");
            notifyIcon1.Visible = false;

            LoginButton.Font = MainFont;
            LoginButton.BackColor = ButtonBackColor;
            LoginButton.FlatAppearance.BorderColor = ButtonBorderColor;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Location = new Point((this.Width - LoginButton.Width) / 2, logoHeight + workingAreaHeight / 2 - LoginButton.Height - 5);
            Menus[MenuType.StartUp].Add(LoginButton);

            RegisterButton.Font = MainFont;
            RegisterButton.BackColor = ButtonBackColor;
            RegisterButton.FlatAppearance.BorderColor = ButtonBorderColor;
            RegisterButton.FlatStyle = FlatStyle.Flat;
            RegisterButton.Location = new Point((this.Width - RegisterButton.Width) / 2, logoHeight + workingAreaHeight / 2 + 5);
            Menus[MenuType.StartUp].Add(RegisterButton);

            userText.Font = messageFont;
            emailText.Font = messageFont;
            passText.Font = messageFont;
            passConfirmText.Font = messageFont;
            usernameLabel.Font = messageFont;
            emailLabel.Font = messageFont;
            passwordLabel.Font = messageFont;
            passConfirmLabel.Font = messageFont;

            userText.KeyPress += inputTextKeyPress;
            emailText.KeyPress += inputTextKeyPress;
            passText.KeyPress += inputTextKeyPress;
            passConfirmText.KeyPress += inputTextKeyPress;

            userText.Location = new Point(emailLabel.Location.X, usernameLabel.Location.Y + usernameLabel.Height + 2);

            emailLabel.Location = new Point(emailLabel.Location.X, userText.Location.Y + userText.Height + 10);
            emailText.Location = new Point(emailLabel.Location.X, emailLabel.Location.Y + emailLabel.Height + 2);

            passwordLabel.Location = new Point(emailLabel.Location.X, emailText.Location.Y + emailText.Height + 10);
            passText.Location = new Point(emailLabel.Location.X, passwordLabel.Location.Y + passwordLabel.Height + 2);
            passText.PasswordChar = '*';

            passConfirmLabel.Location = new Point(emailLabel.Location.X, passText.Location.Y + passText.Height + 10);
            passConfirmText.Location = new Point(emailLabel.Location.X, passConfirmLabel.Location.Y + passConfirmLabel.Height + 2);
            passConfirmText.PasswordChar = '*';

            sendRegisterButton.Font = MainFont;
            sendRegisterButton.BackColor = ButtonBackColor;
            sendRegisterButton.FlatAppearance.BorderColor = ButtonBorderColor;
            sendRegisterButton.FlatStyle = FlatStyle.Flat;
            sendRegisterButton.Location = new Point((this.Width - RegisterButton.Width) / 2, passConfirmText.Location.Y + passConfirmText.Height + 20);
            Menus[MenuType.Register].Add(sendRegisterButton);

            Menus[MenuType.Register].Add(userText);
            Menus[MenuType.Register].Add(usernameLabel);
            Menus[MenuType.Register].Add(emailText);
            Menus[MenuType.Register].Add(emailLabel);
            Menus[MenuType.Register].Add(passText);
            Menus[MenuType.Register].Add(passwordLabel);
            Menus[MenuType.Register].Add(passConfirmText);
            Menus[MenuType.Register].Add(passConfirmLabel);

            activationCodeText.Location = new Point((Width - activationCodeText.Width) / 2, logoHeight + 30);
            activationCodeText.Font = messageFont;
            activationCodeText.MaxLength = 4;
            Menus[MenuType.Activation].Add(activationCodeText);

            activateButton.Font = MainFont;
            activateButton.BackColor = ButtonBackColor;
            activateButton.FlatAppearance.BorderColor = ButtonBorderColor;
            activateButton.FlatStyle = FlatStyle.Flat;
            activateButton.Location = new Point((this.Width - activateButton.Width) / 2, activationCodeText.Location.Y + activationCodeText.Height + 20);
            Menus[MenuType.Activation].Add(activateButton);

            logInSendButton.Font = MainFont;
            logInSendButton.BackColor = ButtonBackColor;
            logInSendButton.FlatAppearance.BorderColor = ButtonBorderColor;
            logInSendButton.FlatStyle = FlatStyle.Flat;
            logInSendButton.Location = new Point((this.Width - logInSendButton.Width) / 2, passText.Location.Y + passText.Height + 20);
            Menus[MenuType.SignIn].Add(logInSendButton);

            Menus[MenuType.SignIn].Add(userText);
            Menus[MenuType.SignIn].Add(usernameLabel);
            Menus[MenuType.SignIn].Add(passText);
            Menus[MenuType.SignIn].Add(passwordLabel);

            chatBox.Font = messageFont;
            chatBox.Size = new System.Drawing.Size(230, 54);
            chatBox.Location = new Point(10, (Height - roundnessRadius) - chatBox.Height);
            chatBox.Multiline = true;
            chatBox.KeyPress += chatBox_KeyPress;
            chatBox.BackColor = Color.FromArgb(255, 211, 211, 211);
            chatBox.DetectUrls = true;
            chatBox.LinkClicked += mainChatBox_LinkClicked;
            chatBox.UpdateShape();

            mainChatBox.Font = messageFont;
            mainChatBox.Width = Width - 22;
            mainChatBox.Height = workingAreaHeight - (this.Height - chatBox.Location.Y) - 20;
            mainChatBox.Location = new Point((Width - mainChatBox.Width) / 2, logoHeight + 8);
            mainChatBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            mainChatBox.ReadOnly = true;
            mainChatBox.Multiline = true;
            mainChatBox.BackColor = Color.FromArgb(255, 211, 211, 211);
            mainChatBox.DetectUrls = true;
            mainChatBox.LinkClicked += mainChatBox_LinkClicked;
            mainChatBox.UpdateShape();

            ToolTip contactsTooltip = new ToolTip();
            contactsTooltip.SetToolTip(SlideButton, "Show Contacts");
            contactsButton.Image = Bitmap.FromFile(@"Resources/Contacts.png");
            contactsButton.BackColor = ButtonBackColor;
            contactsButton.FlatAppearance.BorderColor = ButtonBorderColor;
            contactsButton.FlatStyle = FlatStyle.Flat;
            Menus[MenuType.MainScreen].Add(contactsButton);

            Menus[MenuType.MainScreen].Add(chatBox);
            Menus[MenuType.MainScreen].Add(mainChatBox);

            for (int i = 0; i < 100; i++)
            {
                mainChatBox.AppendText("\n");
            }

            ChangeMenu(MenuType.StartUp);

            foreach (Control control in staticControls)
            {
                if (control.Name != UnslideButton.Name)
                {
                    control.Visible = true;
                }
            }
        }

        private void inputTextKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (currentMenu == MenuType.SignIn)
                {
                    SignIn();
                }
                else if (currentMenu == MenuType.Register)
                {
                    Register();
                }
            }
        }

        private void mainChatBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public static void ChangeMenu(MenuType menuType)
        {
            if (currentMenu == menuType)
            {
                return;
            }

            foreach (Control control in Menus[currentMenu])
            {
                control.Visible = false;
            }

            currentMenu = menuType;

            foreach (Control control in Menus[currentMenu])
            {
                control.Visible = true;
            }
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
        }

        public static GraphicsPath CreateRoundRectangle(Rectangle r, int radius,
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
            base.OnPaint(e);

            e.Graphics.DrawImage(logoImage, new Point(10, 10));
            e.Graphics.DrawPath(borderPen, windowShape);
            e.Graphics.DrawLine(borderPen, new Point(0, logoHeight), new Point(Width, logoHeight));

            this.TopMost = true; // HUE HUE HUE
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            HideForm();
        }

        private void SlideButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.Visible = false;
            }
            UnslideButton.Visible = true;
            Point newPos = new Point(((defaultPosition.X + Width) - UnslideButton.Width) - UnslideButton.Location.X, defaultPosition.Y);
            MoveWindow(newPos);
        }

        private void UnslideButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in Menus[currentMenu])
            {
                control.Visible = true;
            }
            foreach (Control control in staticControls)
            {
                control.Visible = true;
            }
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
        private static bool ValidatePassword(string password)
        {
            return Regex.IsMatch(password, @"^[a-zA-Z0-9_]+$");
        }

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

        private void Register_Button_Click(object sender, EventArgs e)
        {
            ChangeMenu(MenuType.Register);
        }

        private bool Register()
        {
            if (passText.Text != passConfirmText.Text)
            {
                return false;
            }

            if (ValidatePassword(passText.Text))
            {
                NetworkingClient.SendRegistrationData(userText.Text, emailText.Text, passText.Text);
                UserName = userText.Text;
                userText.Clear();
                passText.Clear();
                emailText.Clear();
                passConfirmText.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SignIn()
        {
            if (ValidatePassword(passText.Text))
            {
                NetworkingClient.SendLoginData(userText.Text, passText.Text);
                UserName = userText.Text;
                userText.Clear();
                passText.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            ChangeMenu(MenuType.SignIn);
        }

        private void sendRegisterButton_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            NetworkingClient.SendActivationCode(activationCodeText.Text);
        }

        private void logInSendButton_Click(object sender, EventArgs e)
        {
            SignIn();
        }

        private void chatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Control.ModifierKeys != Keys.Shift)
                {
                    chatBox.Text = chatBox.Text.Remove(chatBox.Text.Length - 1);
                    SendChatMessage();
                }
            }
        }

        private bool SendChatMessage()
        {
            if (chatBox.Text.Length > 0)
            {
                NetworkingClient.SendChatMessage(UserName, chatBox.Text);
                chatBox.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            SendChatMessage();
        }

        public static void RecieveMessage(string username, string message)
        {
            mainChatBox.AppendText(username + ":", Color.Blue);
            mainChatBox.AppendText("\n");
            mainChatBox.AppendText(message);
            mainChatBox.AppendText("\n\n");
            mainChatBox.ScrollToCaret();
        }

        private void contactsButton_Click(object sender, EventArgs e)
        {
            if (!contactsShown)
            {
                this.Width *= 2;
                this.Location = new Point(Screen.GetWorkingArea(this).Width - this.Width, this.Location.Y);
                windowShape = CreateRoundRectangle(new Rectangle(0, 0, Width, Height), roundnessRadius, RectangleCorners.TopLeft | RectangleCorners.BottomLeft);
                this.Region = new Region(windowShape);
                contactsShown = true;
                Invalidate();
            }
            else
            {
                this.Width /= 2;
                this.Location = defaultPosition;
                windowShape = CreateRoundRectangle(new Rectangle(0, 0, Width, Height), roundnessRadius, RectangleCorners.TopLeft | RectangleCorners.BottomLeft);
                this.Region = new Region(windowShape);
                contactsShown = false;
                Invalidate();
            }
        }
    }
}