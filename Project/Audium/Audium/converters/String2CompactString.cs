using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Audium.converters
{
    /// <summary>
    /// Permet de compacter un String s'il est trop long en mettant des points de suspension
    /// </summary>
    class String2CompactString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string titre = value as string;
            if (titre.Length > 25)
            {

                titre = String.Concat(titre.Substring(0,22),"...");
            }
            return titre;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
