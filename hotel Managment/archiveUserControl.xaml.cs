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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlServerCe;
using System.Data;

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for archiveUserControl.xaml
	/// </summary>
	public partial class archiveUserControl : UserControl
	{
		
		static public string idArchiveSelected;

		public archiveUserControl()
		{
			InitializeComponent();
			btnShowAllInfoArchive.IsEnabled = false;
			//Filling the archive dataGrid
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT [RguestID], [First Name], [Family Name], [Age],[Gender], [CheckIn date],[CheckOut date] FROM archiveTable";
			databaseConnection.Open();
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			archiveDataGrid.ItemsSource = dt.DefaultView;
			databaseConnection.Close();
		}

		//show all info dialog on button click
		private void btnShowAllInfoArchive_Click(object sender, RoutedEventArgs e)
		{
			btnShowAllInfoArchive.IsEnabled = true;
			archiveInfo obj = new archiveInfo();
			obj.ShowDialog();
		}

		//the item selected in the archive data grid
		private void archiveDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (archiveDataGrid.SelectedItem != null)
			{
				btnShowAllInfoArchive.IsEnabled = true;
				object itemSelected = archiveDataGrid.SelectedItem;
				idArchiveSelected = ((archiveDataGrid.SelectedCells[0].Column.GetCellContent(itemSelected) as TextBlock).Text).ToString();				
			}
			else
			{
				btnShowAllInfoArchive.IsEnabled = false;
			}
		}
	}
}
