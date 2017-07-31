using Acquaint.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acquaint.Abstractions
{
	/// <summary>
	/// Defines a conract for data source that exposes CRUD operations for a specific type.
	/// </summary>
	public interface IDataSource
	{
		/// <summary>
		/// Gets all the items.
		/// </summary>
		/// <returns>All the items.</returns>
		Task<IEnumerable<Client>> GetItems();

        Task<IEnumerable<Notification>> GetNotificationItems();

        /// <summary>
        /// Gets an item.
        /// </summary>
        /// <returns>An item.</returns>
        /// <param name="id">Id of item to retrieve.</param>
        Task<Client> GetItem(string id);

		/// <summary>
		/// Adds an item.
		/// </summary>
		/// <returns>A bool representing whether or not the operation succeeded.</returns>
		/// <param name="item">An item.</param>
		Task<bool> AddItem(Client item);

		/// <summary>
		/// Updates an item.
		/// </summary>
		/// <returns>A bool representing whether or not the operation succeeded.</returns>
		/// <param name="item">An item.</param>
		Task<bool> UpdateItem(Client item);

		/// <summary>
		/// Removes an item.
		/// </summary>
		/// <returns>A bool representing whether or not the operation succeeded.</returns>
		/// <param name="item">An item.</param>
		Task<bool> RemoveItem(Client item);

		event DataSyncErrorEventHandler OnDataSyncError;

	}
}

