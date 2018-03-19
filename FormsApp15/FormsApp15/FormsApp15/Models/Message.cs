using System;
using System.Collections.Generic;
using System.Text;
using Plugin.DeviceInfo;

namespace FormsApp15.Models
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public bool IsIncoming
        {
            get { return this.UserId != CrossDeviceInfo.Current.Id; }
        }
    }
}
