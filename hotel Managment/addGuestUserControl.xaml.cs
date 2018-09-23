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
	/// Interaction logic for addGuestUserControl.xaml
	/// </summary>
	public partial class addGuestUserControl : UserControl
	{
		public addGuestUserControl()
		{
			InitializeComponent();
			fillComboBox();
		}

		//function that fills a combo box from the rooms database
		public void fillComboBox()
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT [Room Name] FROM roomsTab WHERE [Room Status] = 'Not occupied'";			
			SqlCeDataAdapter cmd = new SqlCeDataAdapter(query, databaseConnection);
			DataTable roomsData = new DataTable();
			databaseConnection.Open();
			cmd.Fill(roomsData);
			for (int i = 0; i < roomsData.Rows.Count; i++)
			{
				roomsComboBox.Items.Add(roomsData.Rows[i]["Room Name"]);
			}
		}

		//function on btn click that adds a guest 
		private void btnAddGuest_Click(object sender, RoutedEventArgs e)
		{
			informationBox obj = new informationBox();
			
			if (firstNameTextBox.Text.Length > 1 && lastNameTextBox.Text.Length > 2 && ageTextBox.Text.Length > 0 &&  identityTextBox.Text.Length > 0 && phoneTextBox.Text.Length > 0 && addressTextBox.Text.Length > 0 )
			{
				if (!int.TryParse(ageTextBox.Text, out int n))
				{
					obj.showMsg("age must be a number");
					obj.ShowDialog();
				}
				else if (!(int.Parse(ageTextBox.Text) > 17))
				{
					obj.showMsg("Age must be 18 or above");
					obj.ShowDialog();
				}
				else if ((int.Parse(ageTextBox.Text) > 100))
				{					
					obj.showMsg("Age must be under 100!");
					obj.ShowDialog();
				}
				else if (!int.TryParse(identityTextBox.Text, out int m) )
				{					
					obj.showMsg("The ID must be a number");
					obj.ShowDialog();
				}
				else if (identityTextBox.Text.Length < 4)
				{					
					obj.showMsg("The ID number is too small , check again");
					obj.ShowDialog();
				}
				else if (!int.TryParse(phoneTextBox.Text, out int l))
				{					
					obj.showMsg("Check again the phone number");
					obj.ShowDialog();
				}
				else if (phoneTextBox.Text.Length < 5)
				{					
					obj.showMsg("The Phone number number is too small , check again");
					obj.ShowDialog();
				}
				else if (firstNameTextBox.Text.Any(char.IsDigit) )
				{					
					obj.showMsg("The first name has a number in it , please check again");
					obj.ShowDialog();
				}
				else if (lastNameTextBox.Text.Any(char.IsDigit))
				{					
					obj.showMsg("The last name has a number in it , please check again");
					obj.ShowDialog();
				}
				else if (checkOutDatePicker.Text.Length == 0)
				{					
					obj.showMsg("Please Enter a Check out date");
					obj.ShowDialog();
				}
				else if (roomsComboBox.Text.Length == 0)
				{					
					obj.showMsg("Please pick the room");
					obj.ShowDialog();
				}
				else if (DateTime.Compare(DateTime.Parse(checkOutDatePicker.Text), DateTime.Today) < 0)
				{					
					obj.showMsg("the date you picked is incorrect");
					obj.ShowDialog();
				}
				else
				{
					Guest obj2 = new Guest(firstNameTextBox.Text, lastNameTextBox.Text, genderComboBox.Text, addressTextBox.Text, int.Parse(ageTextBox.Text), int.Parse(identityTextBox.Text), int.Parse(phoneTextBox.Text), roomsComboBox.Text, DateTime.Parse(checkOutDatePicker.Text));
					obj2.createGuest();
					roomsComboBox.Items.Clear();
					fillComboBox();
				}						
			}
			else
			{				
				obj.showMsg("Please Fill all the fields properly!!");
				obj.ShowDialog();				
			}			
		}
	}
}
