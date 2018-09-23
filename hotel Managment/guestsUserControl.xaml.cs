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
    /// Interaction logic for guestsUserControl.xaml
    /// </summary>
    public partial class guestsUserControl : UserControl
    {
        public guestsUserControl()
        {
            InitializeComponent();
        }

		//changing the guests' tabs on btn clicks
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int menuGuestSelectedIndex = int.Parse(((Button)e.Source).Uid);
			switch (menuGuestSelectedIndex)
			{
				case 0:
					GuestNameGrid.Children.Clear();
					GuestNameGrid.Children.Add(new guestListUserControl());
					break;
				case 1:
					GuestNameGrid.Children.Clear();
					GuestNameGrid.Children.Add(new addGuestUserControl());
					break;
				case 2:
					GuestNameGrid.Children.Clear();
					GuestNameGrid.Children.Add(new archiveUserControl());
					break;
			}
		}		
	}
}
