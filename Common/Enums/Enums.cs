using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    internal class Enums
    {
    }

    public enum ArticleStatus
    {
        Published = 0,
        Draft = 1,
        PendingReview = 2
    }

    public enum AuthorType
    {
        User = 0,
        Practitioner = 1,
    }

    public enum ArticleCategory
    {
        ConditionsAndDiseases = 1,
        ChronicDiseases,
        InfectiousDiseases,
        MentalHealth,
        Cancer,
        RareDiseases,
        Pharmaceuticals,
        SurgicalTreatments,
        AlternativeMedicine,
        PhysicalTherapy,
        VaccinesAndImmunizations,
        NutritionAndDiet,
        ExerciseAndFitness,
        MentalWellness,
        PreventiveCare,
        PublicHealth,
        HealthInsurance,
        Telemedicine,
        MedicalTechnology,
        HealthLiteracy,
        ParentingAndChildHealth,
        AgingAndGeriatrics,
        PregnancyAndReproductiveHealth,
        ClinicalTrials,
        MedicalInnovations,
        GenomicsAndBiotechnology,
        PublicationsAndReviews,
        PatientRights,
        Bioethics,
        BreakingNews,
        HealthPolicyChanges,
        WeightManagement,
        DiabetesManagement,
        HeartHealth,
        PatientStoriesAndTestimonials
    }

    public enum TreatmentType
    {
        ModernMedcine =1, // For English/Western/Pharmaceutical Medicine
        TraditionalMedicine,// For Native/Herbal/Indigineous Remedies
        HomeRemedies // For Home-based/Natural Remedies
    }

    public enum SeverityLevel
    {
        Low =1,
        Medium,
        Critical
    }

    public enum ActivityType
    {
        SymptomSearch = 1,
        PractitionerConsultation,
        Article
    }

}
