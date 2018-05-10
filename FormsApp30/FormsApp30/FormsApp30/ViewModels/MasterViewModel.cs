using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using ReactiveUI;

using FormsApp30.Models;
using FormsApp30.Services;

namespace FormsApp30.ViewModels
{
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MasterViewModel : ReactiveObject
    {
        public ReactiveCommand NavigationItemSelectedCommand { get; }
        public ObservableCollection<MenuItem> MenuItems { get; }

        public MasterViewModel()
        {
            this.MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Id = 1, Title = "Contacts" },
                new MenuItem { Id = 2, Title = "Todos" }
            };
            this.NavigationItemSelectedCommand = ReactiveCommand.Create<MenuItem>(m => NavigationService.Navigate(m));
        }
    }
}
