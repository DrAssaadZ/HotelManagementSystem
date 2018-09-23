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
    /// Interaction logic for updataAccount.xaml
    /// </summary>
    public partial class updataAccount : UserControl
    {
        public updataAccount()
        {
            InitializeComponent();
        }

		//update an existing account on click
		private void btnUpdateAccount_Click(object sender, RoutedEventArgs e)
		{
			Receptionist obj = new Receptionist(currentUserName.Text, currentPassword.Password, newUserName.Text, newPassword.Password, newRepassword.Password);
			obj.updateAccount();
			currentUserName.Clear();
			currentPassword.Clear();
			newUserName.Clear();
			newPassword.Clear();
			newRepassword.Clear();
		}
	}
}
