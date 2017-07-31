using System;
using Acquaint.ModelContracts;

namespace Acquaint.Abstractions
{
	/// <summary>
	/// A generically-typed EventArgs class. 
	/// Contains the local item queued to be pushed, and the backend service item that the local item is in conflict with.
	/// </summary>
	public class DataSyncErrorEventArgs : EventArgs
	{
		public DataSyncErrorEventArgs(object localQueuedItem, object conflictedServiceItem)
		{
			_LocalQueuedItem = localQueuedItem;
			_ConflictedServiceItem = conflictedServiceItem;
		}

		private object _LocalQueuedItem;
		public object LocalQueuedItem
		{
			get { return _LocalQueuedItem; }
		}

		private object _ConflictedServiceItem;
		public object ConflictedServiceItem
		{
			get { return _ConflictedServiceItem; }
		}
	}
}

