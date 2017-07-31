using System;
namespace Acquaint.Abstractions
{
	/// <summary>
	/// A generically-typed delegate for handling data sync errors.
	/// </summary>
	public delegate void DataSyncErrorEventHandler(object sender, DataSyncErrorEventArgs e);
}

