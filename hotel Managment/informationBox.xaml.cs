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

namespace hotel_Managment
{
	/// <summary>
	/// Interaction logic for informationBox.xaml
	/// </summary>
	public partial class informationBox : Window
	{
		public string textEntered;
		public informationBox()
		{
			InitializeComponent();
			
		}

		//get the text & set it to the text box 
		public void showMsg(string textEntered)
		{
			information.Text = textEntered;
			
		}

		//ok close button
		private void btnOKInformationBox_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
