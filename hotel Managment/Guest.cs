using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Data;

namespace hotel_Managment
{
	class Guest
	{
		public string firstNameGuest;
		public string lastNameGuest;
		public string genderGuest;
		public string addressGuest;
		public int ageGuest;
		public int identityGuest;
		public int phoneNumberGuest;
		public string occupiedRoomGuest;
		public DateTime checkInDate;
		public DateTime checkOutDate;

		//guest constructor
		public Guest(string fname, string lname, string gender, string address, int age, int identity, int phone, string occupied, DateTime checkoutdate)
		{
			this.firstNameGuest = fname;
			this.lastNameGuest = lname;
			this.genderGuest = gender;
			this.addressGuest = address;
			this.ageGuest = age;
			this.identityGuest = identity;
			this.phoneNumberGuest = phone;
			this.occupiedRoomGuest = occupied;
			this.checkOutDate = checkoutdate;
		}
		//empty guest constructor
		public Guest()
		{

		}

		//create a guest function
		public void createGuest()
		{			
				SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
				string query = "INSERT INTO guestTab([First Name], [Family Name], [Age],[Phone Number], [Address], [Gender], [Occupied Room], [Identity Number]) " +
								"VALUES ('" + this.firstNameGuest + "','" + this.lastNameGuest + "','" + this.ageGuest + "', '" + this.phoneNumberGuest + "', '" + this.addressGuest + "','" + this.genderGuest + "', '" + this.occupiedRoomGuest + "', '" + this.identityGuest + "')";
				SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
				databaseConnection.Open();
				cmd.ExecuteNonQuery();
				databaseConnection.Close();

				informationBox info = new informationBox();
				info.showMsg("Guest has been added");
				info.ShowDialog();

				query = "SELECT guestID FROM guestTab WHERE [Occupied Room]= '" + this.occupiedRoomGuest + "'";
				SqlCeCommand cmd2 = new SqlCeCommand(query, databaseConnection);
				databaseConnection.Open();
				SqlCeDataAdapter data = new SqlCeDataAdapter(cmd2);
				databaseConnection.Close();
				DataTable dt = new DataTable();
				data.Fill(dt);				
				int guestID = int.Parse(dt.Rows[0]["guestID"].ToString());
				Booking obj = new Booking(DateTime.Today,this.checkOutDate,this.occupiedRoomGuest, guestID);
				obj.insertBooking();
				Room.updateRoomStatus(this.occupiedRoomGuest);			
		}
	}	
}
