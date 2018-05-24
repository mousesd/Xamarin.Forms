using System.Collections.Generic;
using System.Linq;
using FormsApp31.WebApi.Models;

namespace FormsApp31.WebApi.Repositories
{
    public class ToDoRepository : ITodoRepository
    {
        #region == Fields & Constructors ==
        private List<TodoItem> toDoList;

        public ToDoRepository()
        {
            toDoList = new List<TodoItem>
            {
                new TodoItem
                {
                    Id = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                    Name = "Learn app development",
                    Notes = "Attend Xamarin University",
                    Done = true
                },
                new TodoItem
                {
                    Id = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                    Name = "Develop apps",
                    Notes = "Use Xamarin Studio/Visual Studio",
                    Done = false
                },
                new TodoItem
                {
                    Id = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                    Name = "Publish apps",
                    Notes = "All app stores",
                    Done = false,
                }
            };
        }
        #endregion

        #region == Implement members of the IToDoRepository interface ==
        public IEnumerable<TodoItem> All
        {
            get { return toDoList; }
        }

        public bool DoesItemExist(string id)
        {
            return toDoList.Any(item => item.Id == id);
        }

        public TodoItem Find(string id)
        {
            return toDoList.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(TodoItem item)
        {
            toDoList.Add(item);
        }

        public void Update(TodoItem item)
        {
            var todo = this.Find(item.Id);
            int index = toDoList.IndexOf(todo);
            toDoList.RemoveAt(index);
            toDoList.Insert(index, item);
        }

        public void Delete(string id)
        {
            toDoList.Remove(this.Find(id));
        }
        #endregion
    }
}
