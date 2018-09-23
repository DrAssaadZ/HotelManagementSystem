using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlServerCe;
using System.Threading;
using System.Diagnostics;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : Window
	{
		//paths
		static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		static string appDirectoryPath = appDataPath + @"\TheLotusTempleManager";
		static string dataBasePath = appDirectoryPath + @"\TheLotusTempleDB.sdf";
		bool pathExists = Directory.Exists(appDirectoryPath);

		public SplashScreen()
		{
			if (Process.GetProcessesByName("hotel Managment").Length > 1)
			{
				//Application is already running								
				MessageBox.Show("The application is already running");
				Application.Current.Shutdown();								
			}
			else
			{
				//Only one instance of the application is running 
				InitializeComponent();

				//creating the background worker which creates the database along with the tables and controls the progressbar
				BackgroundWorker worker = new BackgroundWorker();
				worker.RunWorkerCompleted += worker_RunWorkerCompleted;
				worker.WorkerReportsProgress = true;
				worker.DoWork += worker_DoWork;
				worker.ProgressChanged += worker_ProgressChanged;
				worker.RunWorkerAsync();				
			}								
		}

		//predefined function which is monituring the progress by the background worker 
		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			threadProgressBar.Value = e.ProgressPercentage;
			progress.Text = (string)e.UserState;
		}

		//predefined function that does the work of the thread 
		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			//creating a background worker & sending its info while it progresses
			var worker = sender as BackgroundWorker;
			worker.ReportProgress(0, string.Format("Initialisation"));
			Thread.Sleep(500);
			if (!pathExists)
			{
				Directory.CreateDirectory(appDirectoryPath);
				worker.ReportProgress(2, string.Format("Creating paths"));
				Thread.Sleep(200);
				File.Create(dataBasePath).Close();
				worker.ReportProgress(4, string.Format("Creating Database"));
				Thread.Sleep(200);
				try
				{
					//creating the database along with the first table 
					SqlCeConnection dataBaseConnection = new SqlCeConnection(@"Data Source=" + dataBasePath + ";Max Database Size = 4091;");
					string query = " CREATE TABLE receptionistAccounts(ID int PRIMARY KEY IDENTITY(1,1) , UserName nvarchar(50), Password nvarchar(50))";
					SqlCeCommand cmd = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(6, "Updating Database 1");
					Thread.Sleep(200);

					//inserting the default username and password
					query = "INSERT INTO receptionistAccounts([UserName], [Password]) VALUES (@UserName, @Password)";
					SqlCeCommand cmd2 = new SqlCeCommand(query, dataBaseConnection);
					cmd2.Parameters.AddWithValue("@UserName", "TheLotusTemple");
					cmd2.Parameters.AddWithValue("@Password", "Temple2018");
					dataBaseConnection.Open();
					cmd2.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(8, "Updating Database 2");
					Thread.Sleep(200);

					//creating the room table 
					query = "CREATE TABLE roomsTab(roomID int IDENTITY(1,1), [Room Name] nvarchar(50) PRIMARY KEY , [Room Type] nvarchar(50), [Room Status] nvarchar(50))";
					SqlCeCommand cmd3 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd3.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(10, "Updating Datebase 3 ");
					Thread.Sleep(200);

					// inserting elements in rooms' table					
					Room.setRooms();
					worker.ReportProgress(90, "Inserting informations");
					Thread.Sleep(1000);

					//creating the Guests' Table
					query = "CREATE TABLE guestTab(guestID int IDENTITY(1,1) PRIMARY KEY , [First Name] nvarchar(50) , [Family Name] nvarchar(50), [Age] int ,[Phone Number] numeric , [Address] nvarchar(200), [Gender] nvarchar(20), [Occupied Room] nvarchar(10), [Identity Number] numeric)";
					SqlCeCommand cmd4 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd4.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(92, "Inserting More Informations");
					Thread.Sleep(200);

					//creating the booking Table
					query = "CREATE TABLE booking(bookingID int IDENTITY(1,1) PRIMARY KEY, [CheckIn date] datetime, [CheckOut date] datetime, [guestID] int , [Room Name] nvarchar(50) )";
					SqlCeCommand cmd5 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd5.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(94, "Updating Database 4");
					Thread.Sleep(200);

					//adding the guest foreign key to the booking table
					query = "ALTER TABLE booking ADD CONSTRAINT [guestID] FOREIGN KEY ([guestID]) REFERENCES guestTab([guestID])";
					SqlCeCommand cmd6 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd6.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(96, "Adding constraints");
					Thread.Sleep(200);

					//adding the room foreig key to the booking table
					query = "ALTER TABLE booking ADD CONSTRAINT [Room Name] FOREIGN KEY ([Room Name]) REFERENCES roomsTab([Room Name])";
					SqlCeCommand cmd7 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd7.ExecuteNonQuery();
					dataBaseConnection.Close();
					worker.ReportProgress(98, "Adding constraints");
					Thread.Sleep(200);

					//creating the checkout table
					query = "CREATE TABLE archiveTable(RguestID int IDENTITY(1,1) PRIMARY KEY , [First Name] nvarchar(50) , [Family Name] nvarchar(50), [Age] int ,[Phone Number] numeric , [Address] nvarchar(200), [Gender] nvarchar(20), [Occupied Room] nvarchar(10), [Identity Number] numeric,[CheckIn date] datetime, [CheckOut date] datetime)";
					SqlCeCommand cmd8 = new SqlCeCommand(query, dataBaseConnection);
					dataBaseConnection.Open();
					cmd8.ExecuteNonQuery();
					dataBaseConnection.Close();
				}
				catch (Exception)
				{
					informationBox info = new informationBox();
					info.showMsg("An erreur has been accured,\n please restart the application");
					info.ShowDialog();
				}
			}
			else
			{
				worker.ReportProgress(50, "Initialisation");
				Thread.Sleep(1000);
			}
			worker.ReportProgress(100, "Done processing");
			Thread.Sleep(200);
		}

		//predefined function that shows the logging window when the backgroundworker is done 
		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Thread.Sleep(500);
			LoggingWindow obj = new LoggingWindow();
			obj.Show();
			this.Close();
		}
	}
}
