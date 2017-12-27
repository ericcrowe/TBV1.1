using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using Xamarin.Forms;


[assembly: Dependency(typeof(Toolbox.UWP.Utilities.Localize))]
namespace Toolbox.UWP.Utilities
{
    public class Localize : ILocalize
    {
        public void SetLocale(CultureInfo ci) { }
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }

}