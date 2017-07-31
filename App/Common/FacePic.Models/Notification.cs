using Acquaint.ModelContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acquaint.Models
{
    public class Notification : ObservableEntityData, INotification
    {
        public string DataPartitionId { get; set; }
        public string FaceApiId { get; set; }

        [JsonIgnore]
        public bool IsRead { get; set; }
    }
}
