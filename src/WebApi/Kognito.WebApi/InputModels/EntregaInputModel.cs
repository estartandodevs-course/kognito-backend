using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.WebApi.InputModels
{
    public class DeliveryInputModel
    {
        public string Content { get; set; }
        public Guid StudentId { get; set; }

    }
}