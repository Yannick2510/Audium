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
    /// Permet d convertir le nom d'une image en chemin pour accéder à cette image
    /// </summary>
    class String2Image : IValueConverter
    {
        private static string imagesPath;

        static String2Image()
        {
            imagesPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\img\");
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = value as string;
            //if (string.IsNullOrWhiteSpace(imageName)) return null;
            string imagePath = Path.Combine(imagesPath, imageName);
            if (!File.Exists(imagePath))
            {
                //return new Uri(Path.Combine(imagesPath,@"icondefault\default.png"), UriKind.RelativeOrAbsolute);
                return new Uri("pack://application:,,,/img/default.png");
            }
            return new Uri(imagePath, UriKind.RelativeOrAbsolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
