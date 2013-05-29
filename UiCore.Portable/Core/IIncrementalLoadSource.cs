using System.Threading.Tasks;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Interface for paged data retrieval
	/// </summary>
	public interface IIncrementalLoadSource<TItem>
	{
		/// <summary>
		/// Inherited class must perform data loading
		/// </summary>
		Task<IIncrementalLoadResponse<TItem>> Fetch(int itemsToFetch);
	}
}