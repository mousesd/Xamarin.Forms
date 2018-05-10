using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using FormsApp30.Models;
using ReactiveUI;

namespace FormsApp30.ViewModels
{
    public class TodoViewModel : ReactiveObject
    {
        private ReactiveList<Todo> todos;
        public ReactiveList<Todo> Todos
        {
            get { return todos; }
            set { this.RaiseAndSetIfChanged(ref todos, value); }
        }

        private Todo selectedTodo;
        public Todo SelectedTodo
        {
            get { return selectedTodo; }
            set { this.RaiseAndSetIfChanged(ref selectedTodo, value); }
        }

        public ReactiveCommand DeleteTodoCommand { get; }

        public TodoViewModel()
        {
            this.Todos = new ReactiveList<Todo> { ChangeTrackingEnabled = true };
            this.Todos.Add(new Todo { IsDone = false, Title = "Go to Sleep" });
            this.Todos.Add(new Todo { IsDone = false, Title = "Go get some dinner" });
            this.Todos.Add(new Todo { IsDone = false, Title = "Watch GOT" });
            this.Todos.Add(new Todo { IsDone = true, Title = "Code code and code!!!!" });

            this.Todos.ItemChanged.Where(t => t.PropertyName == "IsDone" && t.Sender.IsDone)
                .Select(t => t.Sender)
                .Subscribe(t =>
                {
                    if (t.IsDone)
                    {
                        this.Todos.Remove(t);
                        this.Todos.Add(t);
                    }
                });

            this.DeleteTodoCommand = ReactiveCommand.Create<Todo>(t => this.Todos.Remove(t));
        }
    }
}
