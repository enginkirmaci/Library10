using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Library10.Common.Entities
{
    public class ObservableCollectionEx<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ObservableCollectionEx()
           : base()
        {
        }

        public ObservableCollectionEx(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Items.Add(enumerator.Current);
                }
            }

            Subscribe(Items);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Unsubscribe(e.OldItems);
            Subscribe(e.NewItems);
            base.OnCollectionChanged(e);
        }

        protected override void ClearItems()
        {
            foreach (T element in this)
                element.PropertyChanged -= ContainedElementChanged;

            base.ClearItems();
        }

        private void Subscribe(IList<T> iList)
        {
            if (iList != null)
            {
                foreach (T element in iList)
                    element.PropertyChanged += ContainedElementChanged;
            }
        }

        private void Subscribe(IList iList)
        {
            if (iList != null)
            {
                foreach (T element in iList)
                    element.PropertyChanged += ContainedElementChanged;
            }
        }

        private void Unsubscribe(IList iList)
        {
            if (iList != null)
            {
                foreach (T element in iList)
                    element.PropertyChanged -= ContainedElementChanged;
            }
        }

        private void ContainedElementChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(args);
        }
    }
}