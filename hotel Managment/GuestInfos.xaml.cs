using System;
using System.Collections.Generic;
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
using System.Data;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for GuestInfos.xaml
	/// </summary>
	public partial class GuestInfos : Window
	{
		public GuestInfos()
		{
			InitializeComponent();
			fillInfo();
		}

		//button done
		private void btnDoneGuestInfo_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		//fill info function
		public void fillInfo()
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT * FROM guestTab WHERE [Occupied Room]= '" + guestListUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			SqlCeDataAdapter data = new SqlCeDataAdapter(cmd);
			databaseConnection.Close();
			DataTable dt = new DataTable();
			data.Fill(dt);

			firstNameInfo.Text = dt.Rows[0]["First Name"].ToString();
			lastNameInfo.Text = dt.Rows[0]["Family Name"].ToString(); 
			ageInfo.Text = dt.Rows[0]["Age"].ToString();
			phoneInfo.Text = dt.Rows[0]["Phone Number"].ToString();
			idInfo.Text = dt.Rows[0]["Identity Number"].ToString();
			genderInfo.Text = dt.Rows[0]["Gender"].ToString();
			roomInfo.Text = dt.Rows[0]["Occupied Room"].ToString();
			addressInfo.Text = dt.Rows[0]["Address"].ToString();
			
			query = "SELECT * FROM booking WHERE [Room Name] = '" + guestListUserControl.roomNameDataGrid + "'";
			SqlCeCommand cmd2 = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			SqlCeDataAdapter data2 = new SqlCeDataAdapter(cmd2);
			databaseConnection.Close();
			DataTable dt2 = new DataTable();
			data2.Fill(dt2);
			checkInInfo.Text = dt2.Rows[0]["CheckIn date"].ToString();
			checkOutInfo.Text = dt2.Rows[0]["CheckOut date"].ToString();			
		}
	}
}
