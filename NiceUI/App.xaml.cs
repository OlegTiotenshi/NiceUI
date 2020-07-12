using NiceUI.Helpers;
using NiceUI.UI;
using Xamarin.Forms;

namespace NiceUI
{
    public partial class App : Application
    {
        /// <summary>
		/// DO NOT TOUCH APP()
		/// </summary>
        public App()
        {
            //Fix ios crash
            if(Device.RuntimePlatform == Device.iOS)
                Current.MainPage = new ContentPage();
        }

        protected override async void OnStart()
        {
            InitializeComponent();
            SettingService.Init(this);
            DialogService.Init(this);
            await NavigationService.Init(Pages.Main);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
