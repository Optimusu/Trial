using System.ComponentModel.DataAnnotations;
using Trial.Domain.Resources;

namespace Trial.Domain.Entities;

public class SoftPlan
{
    [Key]
    public int SoftPlanId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.SoftPlan), ResourceType = typeof(Resource))]
    public string? Name { get; set; }

    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Price), ResourceType = typeof(Resource))]
    public decimal Price { get; set; }

    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Months), ResourceType = typeof(Resource))]
    public int Meses { get; set; }

    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.TrialsCount), ResourceType = typeof(Resource))]
    public int TrialsCount { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Releaciones
    public ICollection<Corporation>? Corporations { get; set; }
}