using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace приложение.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page3 : ContentPage
	{
		public Page3 ()
		{
			InitializeComponent ();
		}
        public static string Name;
    

     

        private void Panel1Tapped(object sender, EventArgs e)
        {
            Name = "volvo";
            Navigation.PushModalAsync(new auto());
        }

        

        private void Panel2Tapped(object sender, EventArgs e)
        {
            Name = "chary";
            Navigation.PushModalAsync(new auto());
        }

        private void Panel3Tapped(object sender, EventArgs e)
        {
            Name = "Lada";
            Navigation.PushModalAsync(new auto());
        }
        private void Panel4Tapped(object sender, EventArgs e)
        {
            Name = "nissan";
            Navigation.PushModalAsync(new auto());

        }
        private void Panel5Tapped(object sender, EventArgs e)
        {
            Name = "mercedes";
            Navigation.PushModalAsync(new auto());
        }
        private void Panel6Tapped(object sender, EventArgs e)
        {
            Name = "BMW";
            Navigation.PushModalAsync(new auto());
        }

        private void GoToThisPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page3());
        }

        private void GoToCart_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new korzina());
        }

        private void Panel7Tapped(object sender, EventArgs e)
        {

            Name = "Mitsubishi";
            Navigation.PushModalAsync(new auto());
        }

        private void Panel8Tapped(object sender, EventArgs e)
        {
            Name = "Subaru";
            Navigation.PushModalAsync(new auto());
        }
    }
}