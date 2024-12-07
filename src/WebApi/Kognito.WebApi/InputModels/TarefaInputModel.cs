using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.WebApi.InputModels
{
    public class TaskInputModel
    {
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime FinalDeliveryDate { get; set; }
        public Guid ClassId { get; set; }
    }
}