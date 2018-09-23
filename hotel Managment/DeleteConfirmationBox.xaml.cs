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
using System.Windows.Shapes;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for DeleteConfirmationBox.xaml
	/// </summary>
	public partial class DeleteConfirmationBox : Window
	{
		public DeleteConfirmationBox()
		{
			InitializeComponent();
		}

		//yes button
		private void btnYesGuestDelete_Click(object sender, RoutedEventArgs e)
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");

			//Delete from the booking table
			string query = "DELETE FROM booking WHERE [Room Name] = '" + guestListUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd3 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd3.ExecuteNonQuery();
			databaseConnection.Close();

			//Delete from the guest table
			query = "DELETE FROM guestTab WHERE [Occupied Room] = '" + guestListUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd.ExecuteNonQuery();
			databaseConnection.Close();			

			//updating the room status when deleting
			query = "UPDATE roomsTab SET [Room Status] = 'Not occupied' WHERE [Room Name] ='" + guestListUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd4 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			cmd4.ExecuteNonQuery();
			databaseConnection.Close();

			this.Close();
		}

		//no button
		private void btnNoGuestDelete_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
