﻿using System;
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
	/// Interaction logic for InformationWindow.xaml
	/// </summary>
	public partial class InformationWindow : Window
	{
		public InformationWindow()
		{
			InitializeComponent();
		}

		//Ok button 
		private void InfoOkBtn_Click(object sender, RoutedEventArgs e)
		{			
			this.Close();
		}
	}
}
