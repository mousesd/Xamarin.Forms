using System.Windows.Input;
using MvvmHelpers;
using FormsApp16.Models;
using Xamarin.Forms;

namespace FormsApp16.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableRangeCollection<Monkey> monkeys;
        public ObservableRangeCollection<Monkey> Monkeys
        {
            get { return monkeys; }
            set { this.SetProperty(ref monkeys, value); }
        }

        public ICommand ChangeItemsCommand { get; set; }

        #region == Constructors ==
        public MainViewModel()
        {
            this.Monkeys = new ObservableRangeCollection<Monkey>
            {
                new Monkey
                {
                    Name = "Common Marmoset",
                    Image = "common_marmoset.jpg"
                },
                new Monkey
                {
                    Name = "Gibbon",
                    Image = "gibbon_information.jpg"
                },
                new Monkey
                {
                    Name = "Proboscis",
                    Image = "Proboscis_monkey.jpg"
                },
                new Monkey
                {
                    Name = "Pygmy",
                    Image = "pygmy.jpg"
                },
                new Monkey
                {
                    Name = "Spider",
                    Image = "spider_monkey.jpg"
                },
                new Monkey
                {
                    Name = "Squirrel Monkey",
                    Image = "squirrel_monkey_species.jpg"
                },
                new Monkey
                {
                    Name = "Tamarin",
                    Image = "Tamarin.jpg"
                },
                new Monkey
                {
                    Name = "Uknown",
                    Image = "types_of_monkeys.jpg"
                },
                new Monkey
                {
                    Name = "Vervet",
                    Image = "vervet_specie.jpg"
                }
            };
            this.ChangeItemsCommand = new Command(this.OnChangeItems);
        }
        #endregion

        private void OnChangeItems()
        {
            this.Monkeys = new ObservableRangeCollection<Monkey>
            {
                new Monkey
                {
                    Name = "Uknown",
                    Image = "types_of_monkeys.jpg"
                },
                new Monkey
                {
                    Name = "Vervet",
                    Image = "vervet_specie.jpg"
                }
            };
        }
    }
}
