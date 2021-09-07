using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BooksWishlist.ViewModels
{
    public class BooksPageViewModel
    {
        INavigationService _navigationService;
        public ICommand NewBookCommand { get; set; }
        public BooksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NewBookCommand = new DelegateCommand(NewBookAction);
        }

        private async void NewBookAction()
        {
            await _navigationService.NavigateAsync("NewBookPage", null, false, true);
        }
    }
}
