﻿using NiceUI.BL.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NiceUI.UI.Pages
{
    public class BasePage : ContentPage, IDisposable
    {
        protected BaseViewModel BaseViewModel => BindingContext as BaseViewModel;

        public BasePage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BackgroundColor = Color.White;
        }

        public void Dispose()
        {
            BaseViewModel?.Dispose();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent == null)
                Dispose();
            else
                BaseViewModel.StartLoadData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await Task.Delay(50); // Allow UI to handle events loop
                if (BaseViewModel != null)
                {
                    await BaseViewModel.OnPageAppearing();
                    BaseViewModel.StartLoadData();
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Task.Run(async () =>
            {
                await Task.Delay(50); // Allow UI to handle events loop
                if (BaseViewModel != null)
                    await BaseViewModel.OnPageDisappearing();
            });
        }
    }

    public class BasePage<T> : BasePage where T : BaseViewModel
    {
        public T ViewModel => BaseViewModel as T;
    }
}
