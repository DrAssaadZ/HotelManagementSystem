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
    /// Interaction logic for settingWindow.xaml
    /// </summary>
    public partial class settingWindow : Window
    {
        public settingWindow()
        {
            InitializeComponent();
        }

		//drag & move the setting window on mouse down on top of it
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		//changing the setting window tabs on buttons clicked
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int settingSelectionIndex = int.Parse(((Button)e.Source).Uid);
			switch (settingSelectionIndex)
			{
				case 5:
					settingMainGrid.Children.Clear();
					settingMainGrid.Children.Add(new createAccount());
					break;
				case 6:
					settingMainGrid.Children.Clear();
					settingMainGrid.Children.Add(new updataAccount());
					break;
				case 7:
					settingMainGrid.Children.Clear();
					settingMainGrid.Children.Add(new deleteAccount());
					break;
			}
		}		

		//setting window close event on button click
		private void settingCloseBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
