using Acquaint.ModelContracts;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acquaint.Service.DataObjects
{
    public class Notification : EntityData, INotification
    {
        public string FaceApiId { get; set; }
        public string DataPartitionId { get; set; }
    }
}