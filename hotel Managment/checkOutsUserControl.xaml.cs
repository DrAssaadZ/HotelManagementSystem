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
    /// Interaction logic for checkOutsUserControl.xaml
    /// </summary>
    public partial class checkOutsUserControl : UserControl
    {
		SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
		static public string roomNameDataGrid;
		public checkOutsUserControl()
        {
            InitializeComponent();

			btnCheckOutNow.IsEnabled = false;			
			string query = "SELECT guestTab.[First Name],guestTab.[Family Name],guestTab.Gender,guestTab.[Occupied Room] FROM guestTab LEFT JOIN booking ON guestTab.guestID = booking.guestID WHERE booking.[CheckOut date] = @temp"; 
			databaseConnection.Open();
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			cmd.Parameters.AddWithValue("@temp",DateTime.Today);
			SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			CheckOutGrid.ItemsSource = dt.DefaultView;
			databaseConnection.Close();
		}

		//check out button 
		private void btnCheckOutNow_Click(object sender, RoutedEventArgs e)
		{
			CheckOutConfirmationBox2 obj = new CheckOutConfirmationBox2();
			obj.ShowDialog();
			
			//update the guest list grid
			string query = "SELECT guestTab.[First Name],guestTab.[Family Name],guestTab.Gender,guestTab.[Occupied Room] FROM guestTab LEFT JOIN booking ON guestTab.guestID = booking.guestID WHERE booking.[CheckOut date] = @temp";
			databaseConnection.Open();
			SqlCeCommand cmd7 = new SqlCeCommand(query, databaseConnection);
			cmd7.Parameters.AddWithValue("@temp",DateTime.Today);
			SqlCeDataAdapter da3 = new SqlCeDataAdapter(cmd7);
			DataTable dt4 = new DataTable();
			da3.Fill(dt4);
			CheckOutGrid.ItemsSource = dt4.DefaultView;
			databaseConnection.Close();
		}

		//check out selection changed 
		private void CheckOutGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CheckOutGrid.SelectedItem != null)
			{
				btnCheckOutNow.IsEnabled = true;
				object itemSelected = CheckOutGrid.SelectedItem;
				roomNameDataGrid = ((CheckOutGrid.SelectedCells[3].Column.GetCellContent(itemSelected) as TextBlock).Text).ToString();				
			}
			else
			{
				btnCheckOutNow.IsEnabled = false;
			}
		}
	}
}
