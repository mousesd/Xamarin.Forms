using ReactiveUI;

namespace FormsApp18.Models
{
    public class Todo : ReactiveObject
    {
        public string Title { get; set; }

        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set { this.RaiseAndSetIfChanged(ref isDone, value); }
        }

        public bool IsEnabled
        {
            get { return !isDone; }
        }
    }
}
