using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mt.Common.UiCore.Core;
using Mt.Common.UiCore.MvvmCore;
using Mt.WinRtSamples.Views;

namespace Mt.WinRtSamples.ViewModels
{
	[Navigable(typeof(IncrementalSourceView))]
	public class IncrementalSourceViewModel : DemoViewModelBase
	{
		private readonly IncrementalLoadCollection<int> _incrementalLoadNumbers;

		public IncrementalSourceViewModel()
		{
			IIncrementalLoadSource<int> loadSource = new IncrementalLoadSourceNaturalNumbers();
			_incrementalLoadNumbers = new IncrementalLoadCollection<int>(loadSource);
		}

		public override string Title
		{
			get { return "ISupportIncrementalLoading support"; }
		}

		public override string Description
		{
			get { return "Base classes for implementing incremental data load."; }
		}

		public IncrementalLoadCollection<int> Numbers
		{
			get
			{
				return _incrementalLoadNumbers;
			}
		}
	}

	/// <summary>
	/// Async load of natural numbers
	/// </summary>
	public class IncrementalLoadSourceNaturalNumbers : IncrementalLoadSourcePagedBase<int>
	{
		private const int DefaultPageSize = 50;
		private const int TotalNumbers = 1000;

		public IncrementalLoadSourceNaturalNumbers()
			: base(DefaultPageSize)
		{
		}

		protected override async Task<IIncrementalLoadResponse<int>> DoDataFetch(int startPage, int pagesToFetch)
		{
			IEnumerable<Task<IList<int>>> pageRetrievalTasks = Enumerable
				.Range(startPage, pagesToFetch).Select(i => FetchPageAsync(i));

			IList<int>[] results = await Task.WhenAll(pageRetrievalTasks);

			// Merge numbers from retrieval tasks
			IEnumerable<int> searchResultItems = results
				.SelectMany(r => r, (r,  i) => i);

			// Create result item models
			IIncrementalLoadResponse<int> pagedResponse =
				new PagedResponse<int>(searchResultItems, TotalNumbers);

			return pagedResponse;

		}

		private async Task<IList<int>> FetchPageAsync(int pageNumber)
		{
			int startNumber = pageNumber * DefaultPageSize;

			IList<int> pageNumbers = Enumerable
				.Range(startNumber, DefaultPageSize)
				.ToList();

			return await Task.FromResult(pageNumbers);
		}
	}
}
