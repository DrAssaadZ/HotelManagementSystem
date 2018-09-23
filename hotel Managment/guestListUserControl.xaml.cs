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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for guestListUserControl.xaml
	/// </summary>
	public partial class guestListUserControl : UserControl
	{
		//variable that shows the room of the selected row in the datagrid
		static public string roomNameDataGrid;
		SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");

		public guestListUserControl()
		{
			InitializeComponent();
			btnGuestInfo.IsEnabled = false;
			btnDeleteGuest.IsEnabled = false;
			btnCheckOutGuest.IsEnabled = false;

			//showing the guests' list in the guest dataGrid
			string query = "SELECT guestTab.[First Name],guestTab.[Family Name],guestTab.Gender,guestTab.[Occupied Room],booking.[CheckOut date] FROM guestTab LEFT JOIN booking ON guestTab.guestID = booking.guestID;";
			databaseConnection.Open();
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			guestListDataGrid.ItemsSource = dt.DefaultView;
			databaseConnection.Close();
		}

		//Guest list data grid selection changed
		private void guestListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (guestListDataGrid.SelectedItem != null)
			{
				btnGuestInfo.IsEnabled = true;
				btnDeleteGuest.IsEnabled = true;
				btnCheckOutGuest.IsEnabled = true;
				object itemSelected = guestListDataGrid.SelectedItem;
				roomNameDataGrid = ((guestListDataGrid.SelectedCells[3].Column.GetCellContent(itemSelected) as TextBlock).Text).ToString();				
			}
			else
			{
				btnGuestInfo.IsEnabled = false;
				btnDeleteGuest.IsEnabled = false;
				btnCheckOutGuest.IsEnabled = false;
			}								
		}

		//delete guest on delete guest button click
		private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
		{
			DeleteConfirmationBox obj = new DeleteConfirmationBox();
			obj.ShowDialog();
			
			//update the guest list grid
			string query = "SELECT guestTab.[First Name],guestTab.[Family Name],guestTab.Gender,guestTab.[Occupied Room],booking.[CheckOut date] FROM guestTab LEFT JOIN booking ON guestTab.guestID = booking.guestID;";
			databaseConnection.Open();
			SqlCeCommand cmd2 = new SqlCeCommand(query, databaseConnection);
			SqlCeDataAdapter da = new SqlCeDataAdapter(cmd2);
			DataTable dt = new DataTable();
			da.Fill(dt);
			guestListDataGrid.ItemsSource = dt.DefaultView;
			databaseConnection.Close();			
		}

		//guest info button on the guests' tab
		private void btnGuestInfo_Click(object sender, RoutedEventArgs e)
		{			
				btnGuestInfo.IsEnabled = true;
				GuestInfos obj = new GuestInfos();
				obj.ShowDialog();
		}

		//check out button on guest list tab
		private void btnCheckOutGuest_Click(object sender, RoutedEventArgs e)
		{
			CheckOutConfirmationBox obj = new CheckOutConfirmationBox();
			obj.ShowDialog();
			
			//update the guest list grid
			string query = "SELECT guestTab.[First Name],guestTab.[Family Name],guestTab.Gender,guestTab.[Occupied Room],booking.[CheckOut date] FROM guestTab LEFT JOIN booking ON guestTab.guestID = booking.guestID;";
			databaseConnection.Open();
			SqlCeCommand cmd7 = new SqlCeCommand(query, databaseConnection);
			SqlCeDataAdapter da3 = new SqlCeDataAdapter(cmd7);
			DataTable dt4 = new DataTable();
			da3.Fill(dt4);
			guestListDataGrid.ItemsSource = dt4.DefaultView;
			databaseConnection.Close();
		}
	}
}
