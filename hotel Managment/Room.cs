using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Windows;

namespace hotel_Managment
{
	class Room
	{
		//function that initialises the rooms database 
		static public void setRooms()
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string roomTypeVar = "A";
			int typeBindex = 1;
			int typeCindex = 1;
			
			for (int roomIndex = 1; roomIndex < 51; roomIndex++)
			{
				string roomNameVar = "";				
				if (roomIndex < 11)
				{
					roomTypeVar = "A";
					roomNameVar = roomTypeVar + roomIndex.ToString();
					
				}
				if (roomIndex < 31 && roomIndex>10)
				{
					roomTypeVar = "B";
					roomNameVar = roomTypeVar + typeBindex.ToString();
					typeBindex++;
				}
				else if (roomIndex > 30)
				{
					roomTypeVar = "C";
					roomNameVar = roomTypeVar + typeCindex.ToString();					
					typeCindex++; 
				}								
				string query = "INSERT roomsTab ([Room Name], [Room Type], [Room Status]) VALUES (@roomName, @roomType ,'Not occupied')";
				SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
				cmd.Parameters.AddWithValue("@roomName", roomNameVar);
				cmd.Parameters.AddWithValue("@roomType", roomTypeVar);
				databaseConnection.Open();
				cmd.ExecuteNonQuery();
				databaseConnection.Close();
			}
		}

		//function that updates the room's status 
		static public void updateRoomStatus(string roomname)
		{
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "UPDATE roomsTab SET [Room Status] = 'Occupied' WHERE [Room Name] =@roomname";
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			cmd.Parameters.AddWithValue("@roomname",roomname);
			databaseConnection.Open();
			cmd.ExecuteNonQuery();
			databaseConnection.Close();
		}
	}
}
