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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{		
		public MainWindow()
		{			
			InitializeComponent();
			loggedAs.Text = Receptionist.loggedAs;

			//number of occupied rooms
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
			cmd9.Parameters.AddWithValue("checkout",DateTime.Today);
			databaseConnection.Open();
			checkoutTodayNum.Text = cmd9.ExecuteScalar().ToString();
			databaseConnection.Close();
		}

		//drag and move the main window on mouse down on top of it 
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		//exit application on exit btn clicked
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		//change state of the window to minimized on btn minimize click
		private void BtnReduire_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		//interface function that shows which item in the menu is selected and changes the user controls for the main tab
		private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int listItemSelectedIndex = ListViewMenu.SelectedIndex;
			MoveCursorMenu(listItemSelectedIndex);
			switch (listItemSelectedIndex)
			{
				case 0:				
					gridPrincipal.Children.Clear();															
					gridPrincipal.Children.Add(new myUserControl());
					break;
				case 1:
					gridPrincipal.Children.Clear();
					gridPrincipal.Children.Add(new guestsUserControl());
					break;
				case 2:
					gridPrincipal.Children.Clear();
					gridPrincipal.Children.Add(new roomUserControl());
					break;
				case 3:
					gridPrincipal.Children.Clear();
					gridPrincipal.Children.Add(new searchUserControl());
					break;
				case 4:
					gridPrincipal.Children.Clear();
					gridPrincipal.Children.Add(new checkOutsUserControl());
					break;
				default:
					break;
			}
		}

		//interface function that change the cursor's position on selection
		private void MoveCursorMenu(int listItemSelectedIndex)
		{
			transitioningContentSlide.OnApplyTemplate();
			gridCursorMenu.Margin = new Thickness(0, (202 + (70 * listItemSelectedIndex)),0,0);
		}

		//showing settings window on setting button clicked
		private void BtnSettings_Click(object sender, RoutedEventArgs e)
		{			
				settingWindow settwindow = new settingWindow();
				settwindow.ShowDialog();			
		}

		//view all rooms on btn click in dashboard
		private void BtnViewAllRooms_Click(object sender, RoutedEventArgs e)
		{
			gridPrincipal.Children.Clear();
			gridPrincipal.Children.Add(new roomUserControl());
			transitioningContentSlide.OnApplyTemplate();
			gridCursorMenu.Margin = new Thickness(0, (202 + (70 * 2)), 0, 0);
		}

		//information window on btn click
		private void btnInfoMainwindow_Click(object sender, RoutedEventArgs e)
		{			
				InformationWindow inf = new InformationWindow();
				inf.ShowDialog();						
		}

		//view all guests on btn click in the dashboard
		private void BtnViewAllGuests_Click(object sender, RoutedEventArgs e)
		{
			gridPrincipal.Children.Clear();
			gridPrincipal.Children.Add(new guestsUserControl());
			transitioningContentSlide.OnApplyTemplate();
			gridCursorMenu.Margin = new Thickness(0, (202 + (70 * 1)), 0, 0);
		}

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
