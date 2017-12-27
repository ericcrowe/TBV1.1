using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Toolbox.Views;
using Toolbox.Utilities;
using Toolbox.Resources;

namespace Toolbox
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();

            setLanguage();

            MainPage = new LoginPage();
        }

        

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private async void setLanguage()
        {
            var language = new System.Globalization.CultureInfo("en-US");

            //Get the language choice from settings if possible otherwise look at the device
            if (Application.Current.Properties.ContainsKey("language"))
            {
                //Get the language (culture) settings from the properties dictionary
                language = (System.Globalization.CultureInfo)Application.Current.Properties["language"];
            }
            else
            {
                // This lookup NOT required for Windows platforms - the Culture will be automatically set
                if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                {
                    // determine the correct, supported .NET culture
                    language = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                }
                else
                {
                    //get the windows current culture
                    language = System.Globalization.CultureInfo.CurrentCulture;
                }

                //save the setting to the properties dictionary
                Application.Current.Properties["language"] = language;

                //ensure that the proprties are saved to the device in case of an unexpected crash
                await SavePropertiesAsync();
            }

            //Force Culture to French for testing if needed: See (https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx) for additional options
            language = new System.Globalization.CultureInfo("fr-FR");
            Application.Current.Properties["language"] = language;

            AppResources.Culture = language; // set the RESX for resource localization
            DependencyService.Get<ILocalize>().SetLocale(language); // set the Thread for locale-aware methods
        }
	}
}
