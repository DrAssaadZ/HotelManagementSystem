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
	/// Interaction logic for archiveInfo.xaml
	/// </summary>
	public partial class archiveInfo : Window
	{
		public archiveInfo()
		{
			//fills the informations of the showinfo window in archives
			InitializeComponent();
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT * FROM archiveTable WHERE RguestID =@id ";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			cmd.Parameters.AddWithValue("@id", int.Parse(archiveUserControl.idArchiveSelected));
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
			checkInInfo.Text = dt.Rows[0]["CheckIn date"].ToString();
			checkOutInfo.Text = dt.Rows[0]["CheckOut date"].ToString();
		}

		private void btnDoneGuestInfo_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
