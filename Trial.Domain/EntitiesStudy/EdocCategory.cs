using System.ComponentModel.DataAnnotations;
using Trial.Domain.Entities;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesStudy;

public class EdocCategory
{
    [Key]
    public Guid EdocCategoryId { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.IRB), ResourceType = typeof(Resource))]
    public string Name { get; set; } = null!;

    [MaxLength(63, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Container), ResourceType = typeof(Resource))]
    public string NameContainer { get; set; } = null!;

    public Guid StudyId { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Relaciones
    public int CorporationId { get; set; }

    public Corporation? Corporation { get; set; }

    public Study? Study { get; set; }
}