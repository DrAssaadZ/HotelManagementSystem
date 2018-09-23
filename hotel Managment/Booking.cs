using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hotel_Managment
{
	class Booking
	{
		DateTime checkInDate;
		DateTime checkOutDate;
		string roomName;
		int guestIdentity;
		
		//booking constructor
		public Booking(DateTime indate, DateTime outdate, string roomname, int identity)
		{
			this.checkInDate = indate;
			this.checkOutDate = outdate;
			this.roomName = roomname;
			this.guestIdentity = identity;
		}
		
		//function that inserts into the booking table
		public void insertBooking()
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");

			string query2 = "INSERT INTO booking([CheckIn date],[CheckOut date],[guestID],[Room Name]) VALUES (@checkindate,@checkoutdate,@guestid,@roomname)";
			SqlCeCommand cmd5 = new SqlCeCommand(query2, databaseConnection);
			cmd5.Parameters.AddWithValue("@guestid", this.guestIdentity);
			cmd5.Parameters.AddWithValue("@roomname", this.roomName);
			cmd5.Parameters.AddWithValue("@checkoutdate", this.checkOutDate);
			cmd5.Parameters.AddWithValue("@checkindate", DateTime.Today.ToString());
			databaseConnection.Open();
			cmd5.ExecuteNonQuery();
			databaseConnection.Close();			
		}
	}	
}
