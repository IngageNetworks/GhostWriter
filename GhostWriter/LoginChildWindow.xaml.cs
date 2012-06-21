using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace GhostWriter
{
	public partial class LoginChildWindow : ChildWindow
	{
		public string Login { get; set; }

		public string Password { get; set; }

		public LoginChildWindow()
		{
			InitializeComponent();
		}

		protected override void OnOpened()
		{
			base.OnOpened();

			this.txtUserId.Text = Login;
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			// test values
			if (String.IsNullOrWhiteSpace(txtUserId.Text) || String.IsNullOrWhiteSpace(txtPassword.Password))
			{
				MessageBox.Show("WP7: Username & Password must be filled in before logging on.");
				//this.DialogResult = false;
			}
			else if (txtUserId.Text.Trim().Length < 2)
			{
				MessageBox.Show("WP7: Invalid username or password. Be sure to use the INgage website login.");
				//this.DialogResult = false;
			}
			else
			{
				// values are good so close this childwindow
				this.Login = this.txtUserId.Text.Trim();
				this.Password = this.txtPassword.Password.Trim();
				DialogResult = true;
			}
		}

		//private void btnCancel_Click(object sender, RoutedEventArgs e)
		//{
		//    this.DialogResult = false;
		//}
		private void txtUserId_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (txtPassword.Password.Length == 0)
				{
					txtPassword.Focus();
				}
				else
				{
					btnLogin_Click(sender, e);
				}
			}
		}

		private void txtPassword_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				btnLogin_Click(sender, e);
			}
		}

		private void txtPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			btnLogin_Click(sender, e);
		}
	}
}