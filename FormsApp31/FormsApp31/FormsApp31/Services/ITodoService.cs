using System.Collections.Generic;
using System.Threading.Tasks;
using FormsApp31.Models;

namespace FormsApp31.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> RefreshTodoListAsync();

        Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

        Task DeleteTodoItemAsync(string id);
    }
}
