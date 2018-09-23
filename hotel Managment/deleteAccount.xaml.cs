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

namespace hotel_Managment
{
    /// <summary>
    /// Interaction logic for deleteAccount.xaml
    /// </summary>
    public partial class deleteAccount : UserControl
    {
        public deleteAccount()
        {
            InitializeComponent();
        }

		//deleting the current account on button clicked
		private void BtnDeleteAccount_Click(object sender, RoutedEventArgs e)
		{
			Receptionist obj = new Receptionist(Receptionist.loggedAs , delAccountPasswordBox.Password);
			obj.deleteAccount();
		}
	}
}
