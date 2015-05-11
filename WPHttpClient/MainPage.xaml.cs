using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPHttpClient.Resources;
using Newtonsoft.Json;


namespace WPHttpClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //we will make a post request where we send the username and the password to a web api, which accepts json and then sends back a json response.
            if (user_name.Text != "" && password.Password != "")
            {
                ApiCall request = new ApiCall();
                request.requestData = "";
                request.requestData = "{\"sessions\": {\"links\":{\"user\":{\"email\":\"" + user_name.Text + "\",\"password\":\"" + password.Password + "\" }}}}";
                request.resource_url = "api/sessions";
                //Make api call and wait for it to complete and send back the response string!
                var response = await request.postRequest();

                if (!string.IsNullOrEmpty(response) && !string.IsNullOrWhiteSpace(response))
                {
                    try
                    {
                        //deserialize the string into a json object
                        var webresponse = JsonConvert.DeserializeObject<Login.RootObject>(response);
                        //using local storage, set  the user id and the user_token
                        Globals<string> user_id = new Globals<string>("user_id", "");
                        user_id.Value = webresponse.sessions.links.user.id.ToString();
                        Globals<string> user_token = new Globals<string>("user_token", "");
                        user_token.Value = webresponse.sessions.access_token.ToString();
                        error.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green); 
                        error.Text = "Finished communicating with the api and processing the string ";
                    }
                    catch (Exception)
                    {
                       
                    }
                }
                else
                {
                  //tell the user you couldn't get the string!
                }
            }
            else
            {
                error.Text = "Username or Password cannot be blank";
            }
        }

        private void clear_errors(object sender, RoutedEventArgs e)
        {
            error.Text = "";
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}