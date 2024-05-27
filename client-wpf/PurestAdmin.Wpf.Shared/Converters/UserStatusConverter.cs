// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PurestAdmin.Wpf.Shared.Converters
{
    public class UserStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            result = value switch
            {
                0 => "正常",
                1 => "停用",
                _ => "全部",
            };
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ComboBoxItem;
            int? result = item.Content.ToString() switch
            {
                "停用" => 1,
                "正常" => 0,
                _ => null,
            };
            return result;
        }
    }
}
