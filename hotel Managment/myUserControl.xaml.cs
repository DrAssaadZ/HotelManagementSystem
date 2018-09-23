using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hotel_Managment
{
    /// <summary>
    /// Interaction logic for myUserControl.xaml
    /// </summary>
    public partial class myUserControl : UserControl
    {
        public myUserControl()
        {
            InitializeComponent();

			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT COUNT(*) FROM roomsTab WHERE [Room Status] = 'Occupied'";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			occupiedRoomsNum.Text = cmd.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of availbe rooms
			query = "SELECT COUNT(*) FROM roomsTab WHERE [Room Status] = 'Not occupied'";
			SqlCeCommand cmd2 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			avaibleRoomsNum.Text = cmd2.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of avaible A rooms
			query = "SELECT COUNT(*) FROM roomsTab WHERE [Room Status] = 'Not occupied' AND [Room Type] = 'A'";
			SqlCeCommand cmd3 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			avaibleANum.Text = cmd3.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of avaible B rooms
			query = "SELECT COUNT(*) FROM roomsTab WHERE [Room Status] = 'Not occupied' AND [Room Type] = 'B'";
			SqlCeCommand cmd4 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			avaibleBNum.Text = cmd4.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of avaible C rooms
			query = "SELECT COUNT(*) FROM roomsTab WHERE [Room Status] = 'Not occupied' AND [Room Type] = 'C'";
			SqlCeCommand cmd5 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			avaibleCNum.Text = cmd5.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of guests
			query = "SELECT COUNT(*) FROM guestTab";
			SqlCeCommand cmd6 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			guestsNum.Text = cmd6.ExecuteScalar().ToString();
			databaseConnection.Close();


			//number of male guests
			query = "SELECT COUNT(*) FROM guestTab WHERE Gender = 'Male'";
			SqlCeCommand cmd7 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			guestsMaleNum.Text = cmd7.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of female guests
			query = "SELECT COUNT(*) FROM guestTab WHERE Gender = 'Female'";
			SqlCeCommand cmd8 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			guestsFemaleNum.Text = cmd8.ExecuteScalar().ToString();
			databaseConnection.Close();

			//number of checkouts today
			query = "SELECT COUNT(*) FROM booking WHERE [CheckOut date] =@checkout";
			SqlCeCommand cmd9 = new SqlCeCommand(query, databaseConnection);
			cmd9.Parameters.AddWithValue("checkout", DateTime.Today);
			databaseConnection.Open();
			checkoutTodayNum.Text = cmd9.ExecuteScalar().ToString();
			databaseConnection.Close();
		}		
	}
}
