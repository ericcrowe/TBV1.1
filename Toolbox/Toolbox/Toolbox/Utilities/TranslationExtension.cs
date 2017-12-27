using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Toolbox.Utilities
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "Toolbox.Resources.AppResources";

        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId
                                                                                                                  , typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            //load the language / culture from the properties dictionary. This is set initially in the App.xaml.cs file
            ci = (System.Globalization.CultureInfo)Application.Current.Properties["language"];
            //if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            //{
            //    ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            //    ci = new CultureInfo("FR-fr");
            //}
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
            #if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
            #else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
            #endif
            }
            return translation;
        }
    }
}
