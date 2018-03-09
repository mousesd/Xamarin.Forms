using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FormsApp13.DependencyServices
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
