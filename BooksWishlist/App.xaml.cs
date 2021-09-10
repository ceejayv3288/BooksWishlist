using BooksWishlist.Services;
using BooksWishlist.Services.Managers;
using BooksWishlist.ViewModels;
using BooksWishlist.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksWishlist
{
    public partial class App : PrismApplication
    {
        public static SQLiteConnection Connection { get; private set; }
        public static GoogleBooksAPIManager GoogleBooksAPIManager { get; private set; }
        public static string DatabasePath { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        public App(string databasePath, IPlatformInitializer initializer = null) : base(initializer)
        {
            DatabasePath = databasePath;
            Connection = new SQLiteConnection(DatabasePath);
            GoogleBooksAPIManager = new GoogleBooksAPIManager(new GoogleBooksAPIServices());

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
