using System.Collections.Generic;
using System.Diagnostics;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Paged response implementation
	/// </summary>
	[DebuggerDisplay("PageIndex = {PageIndex} - PageSize = {PageSize} - VirtualCount = {VirtualCount}")]
	public class PagedResponse<TItem> : IIncrementalLoadResponse<TItem>
	{
		public PagedResponse(IEnumerable<TItem> items, int totalItemCount)
		{
			this.Items = items;
			this.TotalItemCount = totalItemCount;
		}

		public int TotalItemCount { get; private set; }
		public IEnumerable<TItem> Items { get; private set; }
	}
}