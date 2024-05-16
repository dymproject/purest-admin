// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Prism.Mvvm;

namespace PurestAdmin.Wpf.Shared.CommonViewModels
{
    public class PuaValidateModel : BindableBase, IDataErrorInfo
    {
        private bool _validationPerformed;
        private readonly Dictionary<string, string> _dataErrors = [];
        public bool Validate()
        {
            _dataErrors.Clear();
            _validationPerformed = true;
            Type type = GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var methodInfo = property.GetGetMethod();
                if (methodInfo != null && methodInfo.IsVirtual)
                {
                    RaisePropertyChanged(property.Name);
                }
            }
            return !_dataErrors.Any();
        }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                var vc = new ValidationContext(this, null, null)
                {
                    MemberName = columnName
                };
                var res = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if (res.Count > 0 && _validationPerformed)
                {
                    var errorInfo = string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                    if (!_dataErrors.ContainsKey(columnName))
                    {
                        _dataErrors.Add(columnName, errorInfo);
                    }
                    return errorInfo;
                }
                return string.Empty;
            }
        }
    }
}
