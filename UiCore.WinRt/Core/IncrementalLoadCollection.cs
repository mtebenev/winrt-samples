using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mt.Common.AppCore.Utils;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Observable collection with load on demand capabilities.
	/// Design note: the collection runs items retrieval on UI thread.
	/// </summary>
	public class IncrementalLoadCollection<TItem> : ObservableCollection<TItem>, ISupportIncrementalLoading
	{
		private int _totalItemCount; // Total number of items in data source
		private bool _isInitialLoadDone;	// The source need to perform call at least once to dermine how many items there.

		private readonly IIncrementalLoadSource<TItem> _incrementalLoadSource;

		public IncrementalLoadCollection(IIncrementalLoadSource<TItem> incrementalLoadSource)
		{
			_incrementalLoadSource = incrementalLoadSource;
		}

		public bool HasMoreItems
		{
			get
			{
				bool result = true;

				if(_isInitialLoadDone)
					result = this.Count < _totalItemCount;

				return result;
			}
		}

		public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
		{
			IAsyncOperation<LoadMoreItemsResult> result =
				TaskUtils.RunOnCurrentContext(() => PerformPageLoading((int)count))
				.AsAsyncOperation();

			return result;
		}

		/// <summary>
		/// Effectively this implementation ignores count argument. 
		/// It just loads another page of data if needed
		/// </summary>
		private async Task<LoadMoreItemsResult> PerformPageLoading(int count)
		{
			IIncrementalLoadResponse<TItem> response = await _incrementalLoadSource.Fetch(count);

			if(!_isInitialLoadDone)
			{
				_totalItemCount = response.TotalItemCount;
				_isInitialLoadDone = true;
			}

			foreach(TItem item in response.Items)
				this.Add(item);

			LoadMoreItemsResult result = 
				new LoadMoreItemsResult
					{
						Count = (uint) response.Items.Count()
					};

			return result;
		}
	}
}
