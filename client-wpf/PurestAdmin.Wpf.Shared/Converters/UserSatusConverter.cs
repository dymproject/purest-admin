// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PurestAdmin.Wpf.Shared.Converters
{
    public class UserSatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "全部";
            if (bool.TryParse(value.ToString(), out bool result))
            {
                return !result ? "正常" : "停用";
            }
            return "全部";
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
