using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class PractitionerRatingRequest
    {
        public string PractitionerId { get; set; }
        public int Rating { get; set; }
    }
}
