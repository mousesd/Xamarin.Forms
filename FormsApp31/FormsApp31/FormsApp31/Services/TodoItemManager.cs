using System.Collections.Generic;
using System.Threading.Tasks;
using FormsApp31.Models;

namespace FormsApp31.Services
{
    public class TodoItemManager
    {
        #region == Fields & Constructors ==
        private readonly ITodoService todoService;

        public TodoItemManager(ITodoService todoService)
        {
            this.todoService = todoService;
        }
        #endregion

        #region == Methods ==
        public Task<List<TodoItem>> RefreshTodoListAsync()
        {
            return todoService.RefreshTodoListAsync();
        }

        public Task SaveTodoItemAsync(TodoItem item, bool isNewItem)
        {
            return todoService.SaveTodoItemAsync(item, isNewItem);
        }

        public Task DeleteTodoItemAsync(string id)
        {
            return todoService.DeleteTodoItemAsync(id);
        } 
        #endregion
    }
}
