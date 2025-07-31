using System.ComponentModel.DataAnnotations;
using Trial.Domain.EntitiesStudy;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesGen;

public class TherapeuticArea
{
    [Key]
    public int TherapeuticAreaId { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.TherapeuticArea), ResourceType = typeof(Resource))]
    public string Name { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Description), ResourceType = typeof(Resource))]
    public string? Description { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Relaciones

    public ICollection<Study>? Studies { get; set; }
}