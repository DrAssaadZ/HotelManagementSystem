using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for CheckOutConfirmationBox2.xaml
	/// </summary>
	public partial class CheckOutConfirmationBox2 : Window
	{
		public CheckOutConfirmationBox2()
		{
			InitializeComponent();
		}

		//yes button 
		private void btnYesGuestCheckOut2_Click(object sender, RoutedEventArgs e)
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT * FROM guestTab WHERE [Occupied Room]= '" + checkOutsUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			SqlCeDataAdapter data = new SqlCeDataAdapter(cmd);
			databaseConnection.Close();
			DataTable dt = new DataTable();
			data.Fill(dt);
			Guest obj = new Guest();

			obj.firstNameGuest = dt.Rows[0]["First Name"].ToString();
			obj.lastNameGuest = dt.Rows[0]["Family Name"].ToString();
			obj.ageGuest = int.Parse(dt.Rows[0]["Age"].ToString());
			obj.phoneNumberGuest = int.Parse(dt.Rows[0]["Phone Number"].ToString());
			obj.identityGuest = int.Parse(dt.Rows[0]["Identity Number"].ToString());
			obj.genderGuest = dt.Rows[0]["Gender"].ToString();
			obj.occupiedRoomGuest = dt.Rows[0]["Occupied Room"].ToString();
			obj.addressGuest = dt.Rows[0]["Address"].ToString();

			query = "SELECT * FROM booking WHERE [Room Name] = '" + checkOutsUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd2 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			SqlCeDataAdapter data2 = new SqlCeDataAdapter(cmd2);
			databaseConnection.Close();
			DataTable dt2 = new DataTable();
			data2.Fill(dt2);

			obj.checkInDate = DateTime.Parse(dt2.Rows[0]["CheckIn date"].ToString());
			obj.checkOutDate = DateTime.Parse(dt2.Rows[0]["CheckOut date"].ToString());

			query = "INSERT INTO archiveTable([First Name], [Family Name],[Age],[Phone Number],[Address], [Gender],[Occupied Room],[Identity Number],[CheckIn date],[CheckOut date])" +
				" VALUES(@fname,@lname,@age,@phone,@address,@gender,@room,@id,@in,@out)";
			SqlCeCommand cmd3 = new SqlCeCommand(query, databaseConnection);
			cmd3.Parameters.AddWithValue("@fname", obj.firstNameGuest);
			cmd3.Parameters.AddWithValue("@lname", obj.lastNameGuest);
			cmd3.Parameters.AddWithValue("@age", obj.ageGuest);
			cmd3.Parameters.AddWithValue("@phone", obj.phoneNumberGuest);
			cmd3.Parameters.AddWithValue("@address", obj.addressGuest);
			cmd3.Parameters.AddWithValue("@gender", obj.genderGuest);
			cmd3.Parameters.AddWithValue("@room", obj.occupiedRoomGuest);
			cmd3.Parameters.AddWithValue("@id", obj.identityGuest);
			cmd3.Parameters.AddWithValue("@in", obj.checkInDate);
			cmd3.Parameters.AddWithValue("@out", obj.checkOutDate);
			databaseConnection.Open();
			cmd3.ExecuteNonQuery();
			databaseConnection.Close();

			//Delete from the booking table
			query = "DELETE FROM booking WHERE [Room Name] = '" + checkOutsUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd4 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd4.ExecuteNonQuery();
			databaseConnection.Close();

			//Delete from the guest table
			query = "DELETE FROM guestTab WHERE [Occupied Room] = '" + checkOutsUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd5 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd5.ExecuteNonQuery();
			databaseConnection.Close();			

			//updating the room status when deleting
			query = "UPDATE roomsTab SET [Room Status] = 'Not occupied' WHERE [Room Name] ='" + checkOutsUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd6 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd6.ExecuteNonQuery();
			databaseConnection.Close();

			this.Close();
		}

		private void btnNoGuestCheckOut2_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
