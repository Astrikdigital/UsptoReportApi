using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class EvaluateAssignment
    {
        public int? Id { get; set; }
        public bool? IsResubmission { get; set; }
        public double? ObtainedMark { get; set; }
        public string? Feedback { get; set; }
    }
}
