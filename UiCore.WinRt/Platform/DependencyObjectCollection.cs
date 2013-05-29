using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;

namespace Mt.Common.WinRtUiCore.Platform
{
	/// <summary>
	/// c/p from nRoute
	/// </summary>
	public class DependencyObjectCollection<T>
			: DependencyObject, IList<T>, ICollection<T>, IEnumerable<T>, INotifyCollectionChanged
			where
					T : DependencyObject
	{
		private readonly ObservableCollection<T> _innerCollection;

		public DependencyObjectCollection()
		{
			_innerCollection = new ObservableCollection<T>();
			_innerCollection.CollectionChanged += InnerCollection_CollectionChanged;
		}


		private void InnerCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnCollectionChanged(e);
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if(this.CollectionChanged != null) this.CollectionChanged(this, e);
		}


		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public IEnumerator<T> GetEnumerator()
		{
			return _innerCollection.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _innerCollection.GetEnumerator();
		}

		public void Add(T item)
		{
			_innerCollection.Add(item);
		}

		public void Clear()
		{
			_innerCollection.Clear();
		}

		public bool Contains(T item)
		{
			return _innerCollection.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_innerCollection.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _innerCollection.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			return _innerCollection.Remove(item);
		}

		public int IndexOf(T item)
		{
			return _innerCollection.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			_innerCollection.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			_innerCollection.RemoveAt(index);
		}

		public T this[int index]
		{
			get
			{
				return _innerCollection[index];
			}
			set
			{
				_innerCollection[index] = value;
			}
		}

	}
}
