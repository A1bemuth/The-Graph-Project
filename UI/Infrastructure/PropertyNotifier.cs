using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UI.Annotations;

namespace UI.Infrastructure
{
    public abstract class PropertyNotifier : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly Dictionary<string, object> propertyValues;

        protected PropertyNotifier()
        {
            propertyValues = new Dictionary<string, object>();
        }

        private object this[string name]
        {
            get { return propertyValues.ContainsKey(name) ? propertyValues[name] : null; }
            set
            {
                RaisePropertyChanging(name);
                if (!propertyValues.ContainsKey(name))
                {
                    propertyValues.Add(name, value);

                }
                else
                {
                    propertyValues[name] = value;
                }
                RaisePropertyChanged(name);
            }
        }

        public TValue Get<TValue>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));
            var propertyValue = this[propertyName];
            return propertyValue != null ? (TValue) propertyValue : default(TValue);
        }

        public void Set<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            if(propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));
            this[propertyName] = value;
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanging(string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}