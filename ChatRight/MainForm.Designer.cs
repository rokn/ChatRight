namespace ChatRight
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseButton = new System.Windows.Forms.Button();
            this.SlideButton = new System.Windows.Forms.Button();
            this.UnslideButton = new System.Windows.Forms.Button();
            this.passText = new System.Windows.Forms.TextBox();
            this.HideButton = new System.Windows.Forms.Button();
            this.emailText = new System.Windows.Forms.TextBox();
            this.userText = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            mainChatBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passConfirmText = new System.Windows.Forms.TextBox();
            this.passConfirmLabel = new System.Windows.Forms.Label();
            this.sendRegisterButton = new System.Windows.Forms.Button();
            this.activationCodeText = new System.Windows.Forms.TextBox();
            this.activateButton = new System.Windows.Forms.Button();
            this.logInSendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(231, 12);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(24, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SlideButton
            // 
            this.SlideButton.Location = new System.Drawing.Point(201, 12);
            this.SlideButton.Name = "SlideButton";
            this.SlideButton.Size = new System.Drawing.Size(24, 23);
            this.SlideButton.TabIndex = 1;
            this.SlideButton.UseVisualStyleBackColor = true;
            this.SlideButton.Click += new System.EventHandler(this.SlideButton_Click);
            // 
            // UnslideButton
            // 
            this.UnslideButton.Location = new System.Drawing.Point(3, 117);
            this.UnslideButton.Name = "UnslideButton";
            this.UnslideButton.Size = new System.Drawing.Size(24, 23);
            this.UnslideButton.TabIndex = 13;
            this.UnslideButton.UseVisualStyleBackColor = true;
            this.UnslideButton.Click += new System.EventHandler(this.UnslideButton_Click);
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(11, 247);
            this.passText.Name = "passText";
            this.passText.Size = new System.Drawing.Size(242, 20);
            this.passText.TabIndex = 5;
            // 
            // HideButton
            // 
            this.HideButton.Location = new System.Drawing.Point(171, 12);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(24, 23);
            this.HideButton.TabIndex = 2;
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(11, 221);
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(242, 20);
            this.emailText.TabIndex = 4;
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(11, 195);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(242, 20);
            this.userText.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.AutoSize = true;
            this.LoginButton.Location = new System.Drawing.Point(141, 105);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 11;
            this.LoginButton.Text = "Log In";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.AutoSize = true;
            this.RegisterButton.Location = new System.Drawing.Point(141, 135);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 23);
            this.RegisterButton.TabIndex = 12;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.Register_Button_Click);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(11, 45);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(212, 54);
            this.chatBox.TabIndex = 9;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(231, 45);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(24, 54);
            this.SendMessageButton.TabIndex = 10;
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // mainChatBox
            // 
            mainChatBox.Location = new System.Drawing.Point(11, 6);
            mainChatBox.Multiline = true;
            mainChatBox.Name = "mainChatBox";
            mainChatBox.Size = new System.Drawing.Size(72, 37);
            mainChatBox.TabIndex = 8;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(8, 179);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 16;
            this.usernameLabel.Text = "User name:";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(8, 145);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(38, 13);
            this.emailLabel.TabIndex = 14;
            this.emailLabel.Text = "E-mail:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 158);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 14;
            this.passwordLabel.Text = "Password:";
            // 
            // passConfirmText
            // 
            this.passConfirmText.Location = new System.Drawing.Point(11, 263);
            this.passConfirmText.Name = "passConfirmText";
            this.passConfirmText.Size = new System.Drawing.Size(242, 20);
            this.passConfirmText.TabIndex = 6;
            // 
            // passConfirmLabel
            // 
            this.passConfirmLabel.AutoSize = true;
            this.passConfirmLabel.Location = new System.Drawing.Point(8, 158);
            this.passConfirmLabel.Name = "passConfirmLabel";
            this.passConfirmLabel.Size = new System.Drawing.Size(93, 13);
            this.passConfirmLabel.TabIndex = 15;
            this.passConfirmLabel.Text = "Confirm password:";
            // 
            // sendRegisterButton
            // 
            this.sendRegisterButton.AutoSize = true;
            this.sendRegisterButton.Location = new System.Drawing.Point(141, 164);
            this.sendRegisterButton.Name = "sendRegisterButton";
            this.sendRegisterButton.Size = new System.Drawing.Size(75, 23);
            this.sendRegisterButton.TabIndex = 7;
            this.sendRegisterButton.Text = "Register";
            this.sendRegisterButton.UseVisualStyleBackColor = true;
            this.sendRegisterButton.Click += new System.EventHandler(this.sendRegisterButton_Click);
            // 
            // activationCodeText
            // 
            this.activationCodeText.Location = new System.Drawing.Point(94, 289);
            this.activationCodeText.Name = "activationCodeText";
            this.activationCodeText.Size = new System.Drawing.Size(67, 20);
            this.activationCodeText.TabIndex = 17;
            // 
            // activateButton
            // 
            this.activateButton.AutoSize = true;
            this.activateButton.Location = new System.Drawing.Point(94, 315);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(75, 23);
            this.activateButton.TabIndex = 18;
            this.activateButton.Text = "Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // logInSendButton
            // 
            this.logInSendButton.AutoSize = true;
            this.logInSendButton.Location = new System.Drawing.Point(60, 106);
            this.logInSendButton.Name = "logInSendButton";
            this.logInSendButton.Size = new System.Drawing.Size(75, 23);
            this.logInSendButton.TabIndex = 19;
            this.logInSendButton.Text = "Log In";
            this.logInSendButton.UseVisualStyleBackColor = true;
            this.logInSendButton.Click += new System.EventHandler(this.logInSendButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 607);
            this.Controls.Add(this.logInSendButton);
            this.Controls.Add(this.activateButton);
            this.Controls.Add(this.activationCodeText);
            this.Controls.Add(this.sendRegisterButton);
            this.Controls.Add(this.passConfirmLabel);
            this.Controls.Add(this.passConfirmText);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(mainChatBox);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.emailText);
            this.Controls.Add(this.HideButton);
            this.Controls.Add(this.passText);
            this.Controls.Add(this.UnslideButton);
            this.Controls.Add(this.SlideButton);
            this.Controls.Add(this.CloseButton);
            this.Name = "MainForm";
            this.Text = "ChatRight";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SlideButton;
        private System.Windows.Forms.Button UnslideButton;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.Button HideButton;
        private System.Windows.Forms.TextBox emailText;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passConfirmText;
        private System.Windows.Forms.Label passConfirmLabel;
        private System.Windows.Forms.Button sendRegisterButton;
        private System.Windows.Forms.TextBox activationCodeText;
        private System.Windows.Forms.Button activateButton;
        private System.Windows.Forms.Button logInSendButton;
        private static System.Windows.Forms.TextBox mainChatBox;
    }
}

