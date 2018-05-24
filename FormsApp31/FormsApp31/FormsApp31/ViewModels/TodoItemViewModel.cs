using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using MvvmHelpers;
using FormsApp31.Models;

namespace FormsApp31.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        #region == Fields & Properties ==
        private readonly INavigation navigation;
        private readonly bool isNew;

        private TodoItem todoItem;
        public TodoItem TodoItem
        {
            get { return todoItem; }
            set { this.SetProperty(ref todoItem, value); }
        }
        #endregion

        #region == Commands ==
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region == Constructors ==
        public TodoItemViewModel(INavigation navigation, TodoItem todoItem, bool isNew)
        {
            this.navigation = navigation;
            this.isNew = isNew;
            this.TodoItem = todoItem;

            this.SaveCommand = new Command(async () => await this.SaveAsync());
            this.DeleteCommand = new Command(async () => await this.DeleteAsync());
            this.CancelCommand = new Command(async () => await this.CancelAsync());
        }
        #endregion

        #region == Methods ==
        private async Task SaveAsync()
        {
            if (this.TodoItem == null)
                return;

            await App.TodoItemManager.SaveTodoItemAsync(this.TodoItem, this.isNew);
            await navigation.PopAsync();
        }

        private async Task DeleteAsync()
        {
            if (this.TodoItem == null)
                return;

            await App.TodoItemManager.DeleteTodoItemAsync(this.TodoItem.Id);
            await navigation.PopAsync();
        }

        private async Task CancelAsync()
        {
            await navigation.PopAsync();
        } 
        #endregion
    }
}
