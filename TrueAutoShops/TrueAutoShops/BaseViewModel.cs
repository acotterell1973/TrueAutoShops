using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TrueAutoShops
{
    public abstract class BaseViewModel<TM> : FreshMvvm.FreshBasePageModel
    {
        protected BaseViewModel(){}

        protected BaseViewModel(TM model)
        {
            Model = model;
        }

        public TM Model
        {
            get { return GetValue<TM>(); }
            set { SetValue(value); }
        }
        
        public bool IsBusy
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool IsError
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }


        #region property changed handler
        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            var shouldNotify = !_propertyValues.ContainsKey(propertyName) || !Equals(value, _propertyValues[propertyName]);

            _propertyValues[propertyName] = value;

            if (shouldNotify)
                // ReSharper disable once ExplicitCallerInfoArgument
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
        #endregion
    }
}
