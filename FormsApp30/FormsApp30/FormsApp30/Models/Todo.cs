using ReactiveUI;

namespace FormsApp30.Models
{
    public class Todo : ReactiveObject
    {
        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set { this.RaiseAndSetIfChanged(ref isDone, value); }
        }

        public string Title { get; set; }

        public bool IsEnabled
        {
            get { return !this.IsDone; }
        }
    }
}
