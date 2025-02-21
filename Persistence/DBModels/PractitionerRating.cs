using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class PractitionerRating : HealthTriageEntity
    {
        public PractitionerRating()
        {
            Status = Status.Active;
        }
        public string RatingId { get; set; }
        public string PractitionerId { get; set; }
        public string UserId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; } // Rating between 1 and 5
        // Navigation property
        public virtual Practitioner Practitioner { get; set; }
    }
}
