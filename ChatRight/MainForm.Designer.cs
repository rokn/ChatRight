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
            this.SignUpButton = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.mainChatBox = new System.Windows.Forms.TextBox();
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
            this.UnslideButton.TabIndex = 2;
            this.UnslideButton.UseVisualStyleBackColor = true;
            this.UnslideButton.Click += new System.EventHandler(this.UnslideButton_Click);
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(13, 247);
            this.passText.Multiline = true;
            this.passText.Name = "passText";
            this.passText.Size = new System.Drawing.Size(242, 20);
            this.passText.TabIndex = 3;
            // 
            // HideButton
            // 
            this.HideButton.Location = new System.Drawing.Point(171, 12);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(24, 23);
            this.HideButton.TabIndex = 4;
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(13, 221);
            this.emailText.Multiline = true;
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(241, 20);
            this.emailText.TabIndex = 5;
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(13, 195);
            this.userText.Multiline = true;
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(242, 20);
            this.userText.TabIndex = 6;
            // 
            // LoginButton
            // 
            this.LoginButton.AutoSize = true;
            this.LoginButton.Location = new System.Drawing.Point(141, 105);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 7;
            this.LoginButton.Text = "Log In";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // SignUpButton
            // 
            this.SignUpButton.AutoSize = true;
            this.SignUpButton.Location = new System.Drawing.Point(141, 135);
            this.SignUpButton.Name = "SignUpButton";
            this.SignUpButton.Size = new System.Drawing.Size(75, 23);
            this.SignUpButton.TabIndex = 8;
            this.SignUpButton.Text = "Sign Up";
            this.SignUpButton.UseVisualStyleBackColor = true;
            this.SignUpButton.Click += new System.EventHandler(this.SignUpButton_Click);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(13, 221);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(212, 54);
            this.chatBox.TabIndex = 9;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(231, 221);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(24, 54);
            this.SendMessageButton.TabIndex = 10;
            this.SendMessageButton.UseVisualStyleBackColor = true;
            // 
            // mainChatBox
            // 
            this.mainChatBox.Location = new System.Drawing.Point(13, -27);
            this.mainChatBox.Multiline = true;
            this.mainChatBox.Name = "mainChatBox";
            this.mainChatBox.Size = new System.Drawing.Size(212, 330);
            this.mainChatBox.TabIndex = 11;
            this.mainChatBox.Text = "asdfasdfasdfasdd";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 279);
            this.Controls.Add(this.mainChatBox);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.SignUpButton);
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
        private System.Windows.Forms.Button SignUpButton;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.TextBox mainChatBox;
    }
}

