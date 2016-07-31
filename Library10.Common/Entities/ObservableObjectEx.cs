using Library10.Common.Reflection;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library10.Common.Entities
{
    public abstract class ObservableObjectEx : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = Reflect.GetProperty(propertyExpression).Name;
            this.OnPropertyChanged(propertyName);
        }
    }
}