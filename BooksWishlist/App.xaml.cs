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
        public static string DatabasePath { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        public App(string databasePath, IPlatformInitializer initializer = null) : base(initializer)
        {
            DatabasePath = databasePath;
            NavigationService.NavigateAsync("NavigationPage/BooksPage");
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
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
