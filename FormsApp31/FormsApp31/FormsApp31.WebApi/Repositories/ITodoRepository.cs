using System.Collections.Generic;
using FormsApp31.WebApi.Models;

namespace FormsApp31.WebApi.Repositories
{
    public interface ITodoRepository
    {
        bool DoesItemExist(string id);

        IEnumerable<TodoItem> All { get; }

        TodoItem Find(string id);

        void Insert(TodoItem item);

        void Update(TodoItem item);

        void Delete(string id);
    }
}
