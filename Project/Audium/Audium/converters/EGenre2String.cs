using Donnees;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Audium.converters
{
    /// <summary>
    /// Permet de convertir un EGenre en String afin de pouvoir afficher le genre avec des espaces, des tirets, ...
    /// </summary>
    class EGenre2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EGenre)) return EGenre.AUCUN;
            EGenre genre = (EGenre)value;
            switch (genre)
            {
                case EGenre.JAZZ:
                    return "Jazz";
                case EGenre.ROCK:
                    return "Rock";
                case EGenre.CLASSIQUE:
                    return "Classique";
                case EGenre.HIPHOP:
                    return "Hip-Hop";
                case EGenre.BLUES:
                    return "Blues";
                case EGenre.BANDEORIGINALE:
                    return "Bande Originale";
                default:
                    return "Aucun";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
