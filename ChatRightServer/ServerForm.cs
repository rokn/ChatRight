using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRightServer
{
    public partial class ServerForm : Form
    {
        private static DataSet dataSet;
        private static int maxRows;
        private static DatabaseConnection objConnect;
        private static Random rand;

        private string conString;
        private DataRow dataRow;
        private Timer networkUpdateTimer = new Timer();

        public ServerForm()
        {
            InitializeComponent();
            NetworkingServer.Initialize();

            networkUpdateTimer.Interval = 20;
            networkUpdateTimer.Start();
            networkUpdateTimer.Tick += networkUpdateTimer_Tick;
            DatabaseOpen();

            rand = new Random();
        }

        private void networkUpdateTimer_Tick(object sender, EventArgs e)
        {
            NetworkingServer.Update();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
        }

        public static bool UserRegister(string userName, string email, string password)
        {
            if (ValidateUserName(userName) && ValidateEmail(email))
            {
                int code = rand.Next(1010, 9999);
                DataRow newRow = dataSet.Tables[0].NewRow();
                newRow[1] = userName;
                newRow[2] = password;
                newRow[3] = email;
                newRow[4] = false;
                newRow[5] = code.ToString();
                dataSet.Tables[0].Rows.Add(newRow);

                SendActicationMail(email, code);

                maxRows++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckActivationCode(string username, string code)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (username == row[1].ToString())
                {
                    if (code == row[5].ToString())
                    {
                        row[4] = true;
                        row[5] = DBNull.Value;
                        return true;
                    }
                }
            }

            return false;
        }

        public static void UpdateDatabase()
        {
            try
            {
                objConnect.UpdateDatabase(dataSet);
            }
            catch
            {
                MessageBox.Show("Problem with database update");
            }
        }

        private static void SendActicationMail(string email, int activationCode)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("amindov98@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Chat Right Registration";
                mail.Body = "This email have been registered for the application ChatRight(C).\n\n Activation code : " + activationCode.ToString() + "\n If you haven't regstered for ChatRight(C) just ignore this e-mail.";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("amindov98@gmail.com", "7410852963");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static bool ValidateUserName(string userName)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row[1].ToString() == userName)
                {
                    return false;
                }
            }
            return Regex.IsMatch(userName, @"^[a-zA-Z0-9_]+$");
        }

        private static bool ValidateEmail(string email)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row[3].ToString() == email)
                {
                    return false;
                }
            }
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
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
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public static int CheckLoginStatus(string username, string password)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (username == row[1].ToString())
                {
                    if (password == row[2].ToString())
                    {
                        if ((bool)row[4])
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
            return -1;
        }
    }
}