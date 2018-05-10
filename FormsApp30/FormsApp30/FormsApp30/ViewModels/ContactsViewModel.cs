using System;
using System.Collections.Generic;
using System.Text;
using FormsApp30.Models;
using FormsApp30.Services;
using ReactiveUI;

namespace FormsApp30.ViewModels
{
    public class ContactsViewModel : ReactiveObject
    {
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set { this.RaiseAndSetIfChanged(ref selectedContact, value); }
        }

        public List<ListViewGrouping<Contact>> AllContacts { get; set; } = new List<ListViewGrouping<Contact>>();

        public ContactsViewModel()
        {
            this.AllContacts.AddRange(new[]
            {
                new ListViewGrouping<Contact>("Family", "Family")
                {
                    new Contact { IsFamily = true, Email = "mum@gmail.com", Name = "Mum" },
                    new Contact { IsFamily = true, Email = "dad@gmail.com", Name = "Dad" },
                    new Contact { IsFamily = true, Email = "bro@gmail.com", Name = "Bro" },
                    new Contact { IsFamily = true, Email = "sis@gmail.com", Name = "Sis" },
                },
                new ListViewGrouping<Contact>("Friends", "Friends")
                {
                    new Contact { IsFriend = true, Email = "ratchel@gmail.com", Name = "Ratchel" },
                    new Contact { IsFriend = true, Email = "Ed@gmail.com", Name = "Ed" },
                    new Contact { IsFriend = true, Email = "dina@gmail.com", Name = "Dina" },
                    new Contact { IsFriend = true, Email = "joe@gmail.com", Name = "Jow" },
                },
                new ListViewGrouping<Contact>("Work", "Work")
                {
                    new Contact { IsWork = true, Email = "peter@gmail.com", Name = "Mr Peter" },
                    new Contact { IsWork = true, Email = "john@gmail.com", Name = "Dr John" },
                    new Contact { IsWork = true, Email = "melissa@gmail.com", Name = "Mrs Melissa" },
                    new Contact { IsWork = true, Email = "reva@gmail.com", Name = "Mrs Reva" },
                }
            });
        }
    }
}
