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
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlServerCe;


namespace hotel_Managment
{
    /// <summary>
    /// Interaction logic for LoggingWindow.xaml
    /// </summary>
    public partial class LoggingWindow : Window
    {						
		public LoggingWindow()
        {			
			InitializeComponent();			
		}

		//exit button in login window
		private void logExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();			
		}

		//information window in login window
		private void logInfo_Click(object sender, RoutedEventArgs e)
		{									
				InformationWindow inf = new InformationWindow();
				inf.ShowDialog();			
		}

		//login button event on login window
		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			Receptionist obj = new Receptionist(userNameTextBox.Text, passwordTextBox.Password);
			obj.logIn();
			if (Receptionist.logged)
			{
				this.Close();
			}
		}
	}
}
