using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Windows;

namespace hotel_Managment
{
	class Receptionist
	{
		string userNameRecep;
		string passwordRecep;
		string rePasswordRecep;
		string oldUserNameRecep;
		string oldPasswordRecep;

		static public bool logged = false;
		static public string loggedAs = "";
		
		//login constructor
		public Receptionist(string username ,string password )
		{
			this.userNameRecep = username;
			this.passwordRecep = password;
		}

		//create account constructor
		public Receptionist(string username, string password , string repassword)
		{
			this.userNameRecep = username;
			this.passwordRecep = password;
			this.rePasswordRecep = repassword;
		}

		//update account constructor
		public Receptionist(string oldusername,string oldpassword, string newusername, string newpassword, string newrepassword)
		{
			this.oldUserNameRecep = oldusername;
			this.oldPasswordRecep = oldpassword;
			this.userNameRecep = newusername;
			this.passwordRecep = newpassword;
			this.rePasswordRecep = newrepassword;
		}

		//log in function
		public void logIn()
		{
			informationBox info = new informationBox();

			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT UserName, Password FROM receptionistAccounts WHERE UserName = '" + this.userNameRecep +"' AND Password = '" + this.passwordRecep+"'";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();			
			SqlCeDataReader dr = cmd.ExecuteReader();		
			int count = 0;

			//tests if the information is correct 
			while (dr.Read())
			{
				count++;
			}
			if (count > 0)
			{
				//send the username to logged as var in the main window and shows the main window
				loggedAs = this.userNameRecep;
				MainWindow mn = new MainWindow();				
				mn.Show();				
				logged = true;								
			}
			else
			{
				info.showMsg("Incorrect Information!! Please Try again");
				info.ShowDialog();
			}
			databaseConnection.Close();
		}

		//function that creates an account
		public void createAccountFunc()
		{
			informationBox info = new informationBox();

			if (this.userNameRecep.Length > 3 && this.passwordRecep.Length > 7  )
			{
				SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
				string query = "SELECT UserName FROM receptionistAccounts WHERE UserName = '" + this.userNameRecep + "'";
				SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
				databaseConnection.Open();
				SqlCeDataReader dr = cmd.ExecuteReader();
				int count = 0;
				//tests if the information is correct 
				while (dr.Read())
				{
					count++;
				}
				databaseConnection.Close();
				if (count > 0)
				{
					info.showMsg("UserName already Exists!, Please pick another one");
					info.ShowDialog();
				}
				else
				{
					if (this.passwordRecep == this.rePasswordRecep)
					{
						string query2 = "INSERT INTO receptionistAccounts([UserName], [Password]) VALUES (@UserName, @Password)";
						SqlCeCommand com = new SqlCeCommand(query2, databaseConnection);
						com.Parameters.AddWithValue("@UserName", this.userNameRecep);
						com.Parameters.AddWithValue("@Password", this.passwordRecep);
						databaseConnection.Open();
						com.ExecuteNonQuery();
						databaseConnection.Close();

						info.showMsg("The account has been created");
						info.ShowDialog();
					}
					else
					{
						info.showMsg("Password doesn't match!, Please Verify");
						info.ShowDialog();
					}
				}
			}
			else
			{
				info.showMsg("UserName must be at least 4 caracteres \n and Password 8 caracteres");
				info.ShowDialog();
			}						
		}

		//function that deletes the current account
		public void deleteAccount()
		{
			informationBox info = new informationBox();
			if (this.countTableRows() > 1 )
			{
				SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
				string query = "SELECT UserName, Password FROM receptionistAccounts WHERE UserName = '" + this.userNameRecep + "' AND Password = '" + this.passwordRecep + "'";
				SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
				databaseConnection.Open();
				SqlCeDataReader dr = cmd.ExecuteReader();
				int count = 0;

				//tests if the information is correct 
				while (dr.Read())
				{
					count++;
				}
				databaseConnection.Close();
				if (count > 0 )
				{					
					string query2 = "DELETE FROM receptionistAccounts WHERE UserName = '" + this.userNameRecep + "' AND Password = '" + this.passwordRecep + "'";
					SqlCeCommand cmd2 = new SqlCeCommand(query2, databaseConnection);
					databaseConnection.Open();
					cmd2.ExecuteNonQuery();
					databaseConnection.Close();
					info.showMsg("The Account has been deleted!,\n Please Relog in with another account");
					info.ShowDialog();
					Application.Current.Shutdown();
				}
				else
				{
					info.showMsg("Incorrect password! please try again");
					info.ShowDialog();
				}
			}
			else
			{
				info.showMsg("There's only one account! you can't delete it");
				info.ShowDialog();
			}			
		}

		//function that update the account's info 
		public void updateAccount()
		{
			informationBox info = new informationBox();
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");

			if (userNameRecep.Length >3 && passwordRecep.Length > 7 )
			{
				if (this.passwordRecep == this.rePasswordRecep)
				{
					string query = "SELECT UserName FROM receptionistAccounts WHERE UserName = '" + this.userNameRecep + "'";
					SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
					databaseConnection.Open();
					SqlCeDataReader dr = cmd.ExecuteReader();
					int count = 0;
					//tests if the information is correct 
					while (dr.Read())
					{
						count++;
					}
					databaseConnection.Close();
					if (count > 0 && this.userNameRecep != this.oldUserNameRecep)
					{
						info.showMsg("This Username is already taken");
						info.ShowDialog();
					}
					else
					{
						string query2 = "UPDATE receptionistAccounts SET UserName ='" + this.userNameRecep + "', Password = '" + this.passwordRecep + "' WHERE UserName = '" + this.oldUserNameRecep + "'";
						SqlCeCommand cmd2 = new SqlCeCommand(query2, databaseConnection);
						databaseConnection.Open();
						try
						{
							cmd2.ExecuteNonQuery();
							databaseConnection.Close();
							info.showMsg("The account has been updated");
							info.ShowDialog();
						}
						catch (Exception)
						{
							databaseConnection.Close();
							info.showMsg("Account doesn't exist! ");
							info.ShowDialog();
						}
					}					
				}
				else
				{
					info.showMsg("Password doesn't match!, Please try again");
					info.ShowDialog();
				}				
			}
			else
			{
				info.showMsg("UserName must be at least 4 caracteres \n and Password 8 caracteres! please try again");
				info.ShowDialog();
			}			
		}

		//function that counts the number of the accounts
		public int countTableRows()
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT COUNT(*) FROM receptionistAccounts";
			int rowsCount;
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			databaseConnection.Open();
			rowsCount = (int)cmd.ExecuteScalar();
			databaseConnection.Close();
			return rowsCount;
		}
	}
}
