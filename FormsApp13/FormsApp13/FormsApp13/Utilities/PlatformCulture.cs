using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp13.Utilities
{
    public class PlatformCulture
    {
        public string PlatformString { get; }
        public string LanguageCode { get; }
        public string LocalCode { get; }

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
                throw new ArgumentNullException(nameof(platformCultureString));

            this.PlatformString = platformCultureString.Replace('_', '-');
            var cultures = platformCultureString.Split('-');
            if (cultures.Length > 1)
            {
                this.LanguageCode = cultures[0];
                this.LocalCode = cultures[1];
            }
            else
            {
                this.LanguageCode = cultures[0];
                this.LocalCode = string.Empty;
            }
        }

        public override string ToString()
        {
            return this.PlatformString;
        }
    }
}
