using System;
using System.Windows;
using System.Windows.Controls;
using INgageNetworks;

using Microsoft.Phone.Controls;

namespace GhostWriter
{
	public partial class MainPage : PhoneApplicationPage
	{
		private const string ApiKey = "74a51b4bd780eea3d52e9eccacbe94da";

		private string Username { get; set; }

		private Api _ingageApi;

		// Constructor
		public MainPage()
		{
			InitializeComponent();
			Loaded += MainPage_Loaded;
		}

		// Handle selection changed on ListBox
		private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If selected index is -1 (no selection) do nothing
			if (MainListBox.SelectedIndex == -1) return;
			NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

			// Reset selected index to -1 (no selection)
			MainListBox.SelectedIndex = -1;

			// Navigate to the new page
		}

		// Load data for the ViewModel Items
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			if (App.ApiInstance != null && App.ViewModel.IsDataLoaded) return;
			Username = "admin";
			var loginWindow = new LoginChildWindow { Login = Username };
			loginWindow.Closed += OnLoginChildWindowShow;

			loginWindow.Show();
		}

		private void OnLoginChildWindowShow(object sender, EventArgs e)
		{
			if (sender == null) return;
			var loginChildWindow = sender as LoginChildWindow;

			if (loginChildWindow == null || loginChildWindow.DialogResult != true) return;
			_ingageApi = new Api(loginChildWindow.Login, loginChildWindow.Password, ApiKey);
			if (_ingageApi == null) return;
			App.ApiInstance = _ingageApi;
			// Set the data context of the listbox control to the sample data
			DataContext = App.ViewModel;
			App.ViewModel.LoadData();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if (_ingageApi != null) return;
			Username = "admin";
			var loginWindow = new LoginChildWindow { Login = Username };
			loginWindow.Closed += OnLoginChildWindowShow;

			loginWindow.Show();
		}
	}
}