using BooksWishlist.ViewModels;
using BooksWishlist.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksWishlist
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/BooksPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<BookDetailsPage, BookDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<BooksPage, BooksPageViewModel>();
            containerRegistry.RegisterForNavigation<NewBookPage, NewBookPageViewModel>();
        }
    }
}
