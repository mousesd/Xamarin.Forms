using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveUI;
using FormsApp18.Models;

namespace FormsApp18.ViewModels
{
    [SuppressMessage("ReSharper", "RedundantLogicalConditionalExpressionOperand")]
    public class ItemsViewModel : ViewModelBase
    {
        #region == Fields & Properties ==
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

        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        private ObservableAsPropertyHelper<bool> canAdd;
        public bool CanAdd
        {
            get { return canAdd?.Value ?? false; }
        }

        private string todoTitle;
        public string TodoTitle
        {
            get { return todoTitle; }
            set { this.RaiseAndSetIfChanged(ref todoTitle, value); }
        } 
        #endregion

        public ReactiveCommand AddCommand { get; }

        public ItemsViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            this.WhenAnyValue(o => o.TodoTitle, todoTitle => !string.IsNullOrEmpty(todoTitle))
                .ToProperty(this, o => o.CanAdd, out canAdd);

            this.AddCommand = ReactiveCommand.CreateFromTask(() =>
            {
                this.Todos.Add(new Todo { Title = this.TodoTitle });
                this.TodoTitle = string.Empty;
                return Task.CompletedTask;
            }
            , this.WhenAnyValue(o => o.CanAdd, canAdd => canAdd && true));

            this.Todos = new ReactiveList<Todo> { ChangeTrackingEnabled = true };
            this.Todos.Add(new Todo { IsDone = false, Title = "Go to sleep" });
            this.Todos.Add(new Todo { IsDone = false, Title = "Go get some dinner" });
            this.Todos.Add(new Todo { IsDone = false, Title = "Watch GOT" });
            this.Todos.Add(new Todo { IsDone = false, Title = "Code code and code!!!!" });

            this.Todos.ItemChanged.Where(o => o.PropertyName == "IsDone" && o.Sender.IsDone)
                .Select(o => o.Sender)
                .Subscribe(o =>
                {
                    if (o.IsDone)
                    {
                        this.Todos.Remove(o);
                        this.Todos.Add(o);
                    }
                });
        }
    }
}
