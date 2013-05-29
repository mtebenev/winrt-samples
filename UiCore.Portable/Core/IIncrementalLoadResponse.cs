using System.Collections.Generic;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Retrieved page (response)
	/// </summary>
	public interface IIncrementalLoadResponse<out TItem>
	{
		/// <summary>
		/// Retrieved items
		/// </summary>
		IEnumerable<TItem> Items { get; }

		/// <summary>
		/// Overall number of items
		/// </summary>
		int TotalItemCount { get; }
	}
}