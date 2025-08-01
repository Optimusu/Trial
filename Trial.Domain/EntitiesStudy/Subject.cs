using System.ComponentModel.DataAnnotations;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesGen;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesStudy;

public class Subject
{
    [Key]
    public int SubjectId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
    public string FirstName { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
    public string LastName { get; set; } = null!;

    [MaxLength(101, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.FullName), ResourceType = typeof(Resource))]
    public string? FullName { get; set; }

    [Display(Name = nameof(Resource.DOB), ResourceType = typeof(Resource))]
    public DateTime? DOB { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TypeDocument), ResourceType = typeof(Resource))]
    public int DocumentTypeId { get; set; }

    [MaxLength(15, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Document), ResourceType = typeof(Resource))]
    public string Nro_Document { get; set; } = null!;

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Phone), ResourceType = typeof(Resource))]
    public string? PhoneNumber { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = nameof(Resource.Comments), ResourceType = typeof(Resource))]
    public string? Comments { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.EmailAddress)]
    [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
    public string UserName { get; set; } = null!;

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Relaciones

    public int CorporationId { get; set; }

    public Corporation? Corporation { get; set; }

    public ICollection<DocumentType>? DocumentTypes { get; set; }
}