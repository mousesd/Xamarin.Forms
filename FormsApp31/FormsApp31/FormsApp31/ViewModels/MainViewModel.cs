using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using MvvmHelpers;
using FormsApp31.Models;
using FormsApp31.Views;

namespace FormsApp31.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region == Fields & Properties ==
        private readonly INavigation navigation;

        private ObservableRangeCollection<TodoItem> todoItems = new ObservableRangeCollection<TodoItem>();
        public ObservableRangeCollection<TodoItem> TodoItems
        {
            get { return todoItems; }
            set { this.SetProperty(ref todoItems, value); }
        }
        #endregion

        #region == Commands ==
        public ICommand AddTodoItemCommand { get; }
        public ICommand TodoItemSelectedCommand { get; }
        #endregion

        #region == Constructors ==
        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            this.AddTodoItemCommand = new Command(this.AddTodoItem);
            this.TodoItemSelectedCommand = new Command(this.TodoItemSelected);
        }
        #endregion

        #region == Methods ==
        public async Task GetTodoItems()
        {
            this.TodoItems.Clear();

            var todoList = await App.TodoItemManager.RefreshTodoListAsync();
            if (todoList != null)
                this.TodoItems.AddRange(todoList);
        }

        private void AddTodoItem()
        {
            var todoItem = new TodoItem { Id = Guid.NewGuid().ToString() };
            var page = new TodoItemPage(todoItem, true);
            navigation.PushAsync(page);
        }

        private void TodoItemSelected(object selectedTodoItem)
        {
            if (!(selectedTodoItem is TodoItem todoItem))
                return;

            var page = new TodoItemPage(todoItem);
            navigation.PushAsync(page);
        } 
        #endregion
    }
}
