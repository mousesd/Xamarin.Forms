using System;
using System.Collections.Generic;
using System.Text;
using FormsApp15.Cells;
using FormsApp15.Models;
using Xamarin.Forms;

namespace FormsApp15.Selectors
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;

        public MessageDataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is Message message))
                return null;

            return message.IsIncoming ? incomingDataTemplate : outgoingDataTemplate;
        }
    }
}
