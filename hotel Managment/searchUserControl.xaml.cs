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
    /// Interaction logic for searchUserControl.xaml
    /// </summary>
    public partial class searchUserControl : UserControl
    {
        public searchUserControl()
        {
            InitializeComponent();
        }
		SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");

		//search button click event
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			informationBox info = new informationBox();
			if (nameRadio.IsChecked == true)
			{
				if (searchBar.Text.Length < 1)
				{
					info.showMsg("The field is empty please enter something");
					info.ShowDialog();
				}
				else if (searchBar.Text.Any(char.IsDigit))
				{
					info.showMsg("The name contains a number, check again");
					info.ShowDialog();
				}
				else
				{
					string query = "SELECT [Identity Number], [First Name], [Family Name], [Age],[Gender], [CheckIn date],[CheckOut date],[Address],[Occupied Room] FROM archiveTable WHERE [First Name] = @name";
					databaseConnection.Open();
					SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
					cmd.Parameters.AddWithValue("@name",searchBar.Text);
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					searchDataGrid.ItemsSource = dt.DefaultView;
					databaseConnection.Close();
				}				
			}
			else if (lnameRadio.IsChecked == true)
			{
				if (searchBar.Text.Length < 1)
				{
					info.showMsg("The field is empty please enter something");
					info.ShowDialog();
				}
				else if (searchBar.Text.Any(char.IsDigit))
				{
					info.showMsg("The name contains a number, check again");
					info.ShowDialog();
				}
				else
				{
					string query = "SELECT [Identity Number],[First Name], [Family Name], [Age],[Gender], [CheckIn date],[CheckOut date],[Address],[Occupied Room] FROM archiveTable WHERE [Family Name] = @name";
					databaseConnection.Open();
					SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
					cmd.Parameters.AddWithValue("@name", searchBar.Text);
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					searchDataGrid.ItemsSource = dt.DefaultView;
					databaseConnection.Close();
				}
			}
			else if (phoneRadio.IsChecked == true)
			{
				if (searchBar.Text.Length < 5)
				{
					info.showMsg("Enter a proper phone number");
					info.ShowDialog();
				}
				else if (!int.TryParse(searchBar.Text, out int l))
				{
					info.showMsg("The number contains a character, check again");
					info.ShowDialog();
				}
				else
				{
					string query = "SELECT [Identity Number],[First Name], [Family Name], [Age],[Gender], [CheckIn date],[CheckOut date],[Address],[Occupied Room] FROM archiveTable WHERE [Phone Number] = @name";
					databaseConnection.Open();
					SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
					cmd.Parameters.AddWithValue("@name", searchBar.Text);
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					searchDataGrid.ItemsSource = dt.DefaultView;
					databaseConnection.Close();
				}
			}
			else if (idRadio.IsChecked == true)
			{
				if (searchBar.Text.Length < 4)
				{
					info.showMsg("Enter a proper ID");
					info.ShowDialog();
				}
				else if (!int.TryParse(searchBar.Text, out int n))
				{
					info.showMsg("The ID contains a character, check again");
					info.ShowDialog();
				}
				else
				{
					string query = "SELECT [Identity Number],[First Name], [Family Name], [Age],[Gender], [CheckIn date],[CheckOut date],[Address],[Occupied Room] FROM archiveTable WHERE [Identity Number] = @name";
					databaseConnection.Open();
					SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
					cmd.Parameters.AddWithValue("@name", searchBar.Text);
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					searchDataGrid.ItemsSource = dt.DefaultView;
					databaseConnection.Close();
				}
			}
			else
			{
				info.showMsg("Please pick a search type");
				info.ShowDialog();
			}			
		}
	}
}
