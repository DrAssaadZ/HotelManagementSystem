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
    /// Interaction logic for createAccount.xaml
    /// </summary>
    public partial class createAccount : UserControl
    {
        public createAccount()
        {
            InitializeComponent();
        }

		//creating a new account on create account button click
		private void BtnCreateAcc_Click(object sender, RoutedEventArgs e)
		{
			Receptionist rec = new Receptionist(CreateAccUserName.Text, CreateAccPass.Password, CreateAccRePass.Password);
			rec.createAccountFunc();
			CreateAccUserName.Clear();
			CreateAccPass.Clear();
			CreateAccRePass.Clear();
		}
	}
}
