using System.ComponentModel.DataAnnotations;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesGen;
using Trial.Domain.Enum;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesStudy;

public class Study
{
    [Key]
    public Guid StudyId { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.SiteNumber), ResourceType = typeof(Resource))]
    public string? SiteNumber { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.StudyNumber), ResourceType = typeof(Resource))]
    public string? StudyNumber { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TherapeuticArea), ResourceType = typeof(Resource))]
    public int TherapeuticAreaId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.EnrollingStatus), ResourceType = typeof(Resource))]
    public int EnrollingId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Indication), ResourceType = typeof(Resource))]
    public int IndicationId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TrialPhase), ResourceType = typeof(Resource))]
    public TrialPhase TrialPhase { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Sponsor), ResourceType = typeof(Resource))]
    public int SponsorId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Protocol), ResourceType = typeof(Resource))]
    public string? Protocol { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.ProtocolComplete), ResourceType = typeof(Resource))]
    public string? CompleteProtocol { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.ClinicalDescription), ResourceType = typeof(Resource))]
    public string? ClinicalDescription { get; set; }

    [Required]
    [Display(Name = nameof(Resource.PrincipalInvestigator), ResourceType = typeof(Resource))]
    public int UsuarioId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.IRB), ResourceType = typeof(Resource))]
    public int IrbId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.CRO), ResourceType = typeof(Resource))]
    public int CroId { get; set; }

    [Display(Name = nameof(Resource.EnrollmentGoal), ResourceType = typeof(Resource))]
    public int EnrollmentGoal { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Relaciones
    public int CorporationId { get; set; }

    public Corporation? Corporation { get; set; }

    //BothDirecction

    public TherapeuticArea? TherapeuticArea { get; set; }
    public Enrolling? Enrolling { get; set; }
    public Indication? Indication { get; set; }
    public Sponsor? Sponsor { get; set; }
    public Usuario? Usuario { get; set; }
    public Irb? Irb { get; set; }
    public Cro? Cro { get; set; }
}