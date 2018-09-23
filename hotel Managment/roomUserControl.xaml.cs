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
    /// Interaction logic for roomUserControl.xaml
    /// </summary>
    public partial class roomUserControl : UserControl
    {
        public roomUserControl()
        {
			InitializeComponent();

			//showing the rooms in the room data grid
			SqlCeConnection databaseConnection = new SqlCeConnection(@"Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TheLotusTempleManager\TheLotusTempleDB.sdf");
			string query = "SELECT [Room Name], [Room Type], [Room Status] FROM roomsTab";
			databaseConnection.Open();
			SqlCeCommand cmd = new SqlCeCommand(query, databaseConnection);
			SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			roomsDataGrid.ItemsSource = dt.DefaultView;
			databaseConnection.Close();			
        }
    }
}
