using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace TrueAutoShops
{
    [ImplementPropertyChanged]
    public class BaseNotificationHandler : INotifyPropertyChanged
    {
        public  event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            var shouldNotify = !_propertyValues.ContainsKey(propertyName) || !Equals(value, _propertyValues[propertyName]);

            _propertyValues[propertyName] = value;

            if (shouldNotify)
                RaisePropertyChanged(propertyName);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return GetValue(propertyName, default(T));
        }
        
        private T GetValue<T>(string propertyName, T defaultValue)
        {
            if (_propertyValues.ContainsKey(propertyName))
                return (T)_propertyValues[propertyName];

            return defaultValue;
        }

        public  virtual void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
