using Persistence.Concrete;
using System.ComponentModel.DataAnnotations;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class PractitionerRating : HealthTriageEntity
    {
        public PractitionerRating()
        {
            Status = Status.Active;
            RatingId = string.Empty;
            PractitionerId = string.Empty;
            UserId = string.Empty;
            Rating = 1; // Default rating set to the lowest value
        }

        public string RatingId { get; set; }
        public string PractitionerId { get; set; }
        public string UserId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } // Rating between 1 and 5

        // Navigation property
        public virtual Practitioner Practitioner { get; set; }
    }
}
