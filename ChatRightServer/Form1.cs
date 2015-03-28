using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRightServer
{
    public partial class Form1 : Form
    {
        private string conString;
        private DataRow dataRow;
        private DataSet dataSet;
        private Point defaultPosition;
        private int maxRows;
        private DatabaseConnection objConnect;
        private Timer networkUpdateTimer = new Timer();

        public Form1()
        {
            InitializeComponent();
            NetworkingServer.Initialize();

            networkUpdateTimer.Interval = 20;
            networkUpdateTimer.Start();
            networkUpdateTimer.Tick += networkUpdateTimer_Tick;
        }

        private void networkUpdateTimer_Tick(object sender, EventArgs e)
        {
            NetworkingServer.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void NavigateRecords()
        {
        }
    }
}