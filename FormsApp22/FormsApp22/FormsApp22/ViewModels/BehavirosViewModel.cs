using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using FormsApp22.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace FormsApp22.ViewModels
{
    public class BehavirosViewModel : BaseViewModel
    {
        public static string[] ValidRatings = { "Good", "Magnificent" };

        public readonly ObservableCollection<Ratings> beardRatings = new ObservableCollection<Ratings>
        {
            new Ratings{ Description="Fair", StarRating = 0 },
            new Ratings{ Description="Good", StarRating = 1 },
            new Ratings{ Description = "Cool", StarRating = 2 },
            new Ratings{ Description = "Great", StarRating = 3 },
            new Ratings{ Description = "Magnificent", StarRating = 4 }
        };

        public ObservableCollection<Ratings> BeardRatings
        {
            get { return beardRatings; }
        }

        private Ratings selectedBeardRatings;
        public Ratings SelectedBeardRating
        {
            get { return selectedBeardRatings; }
            set { this.SetProperty(ref selectedBeardRatings, value); }
        }

        public ICommand EntryPressCommand { get; }

        public BehavirosViewModel()
        {
            this.SelectedBeardRating = this.BeardRatings[0];
            this.EntryPressCommand = new Command<string>(this.ParseBeardText);
        }

        private void ParseBeardText(string input)
        {
            if (input.ToLower().Contains("fair"))
                this.SelectedBeardRating = this.BeardRatings[0];
            else if (input.ToLower().Contains("good"))
                this.SelectedBeardRating = this.BeardRatings[1];
            else if (input.ToLower().Contains("cool"))
                this.SelectedBeardRating = this.BeardRatings[2];
            else if (input.ToLower().Contains("great"))
                this.SelectedBeardRating = this.BeardRatings[3];
            else if (input.ToLower().Contains("magnificent"))
                this.SelectedBeardRating = this.BeardRatings[4];
        }
    }
}
