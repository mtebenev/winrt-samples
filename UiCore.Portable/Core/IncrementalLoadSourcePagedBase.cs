using System;
using System.Threading.Tasks;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Use as base class for incremental load sources able to retrieve data in fashioned manner
	/// </summary>
	public abstract class IncrementalLoadSourcePagedBase<TItem> : IIncrementalLoadSource<TItem>
	{
		private readonly int _pageSize;
		private int _currentPage;

		protected IncrementalLoadSourcePagedBase(int pageSize)
		{
			_pageSize = pageSize;
			_currentPage = 0;
		}

		public async Task<IIncrementalLoadResponse<TItem>> Fetch(int itemsToFetch)
		{
			// Calculate number of pages to retrieve
			int pagesToFetch = (int) Math.Ceiling((decimal)itemsToFetch / _pageSize);

			// Perform fetch
			IIncrementalLoadResponse<TItem> response = await DoDataFetch(_currentPage, pagesToFetch);

			// Update state
			_currentPage++;

			return response;
		}

		/// <summary>
		/// Must be implemented in derived classes. Performs actual data fetching.
		/// </summary>
		protected abstract Task<IIncrementalLoadResponse<TItem>> DoDataFetch(int startPage, int pagesToFetch);
	}
}