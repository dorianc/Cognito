namespace Acquaint.ModelContracts
{
    public interface INotification
    {
        string DataPartitionId { get; set; }
        string FaceApiId { get; set; }
    }
}