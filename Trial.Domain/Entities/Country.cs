using System.ComponentModel.DataAnnotations;
using Trial.Domain.Resources;

namespace Trial.Domain.Entities;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Country), ResourceType = typeof(Resource))]
    public string Name { get; set; } = null!;

    [MaxLength(10, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.CodPhone), ResourceType = typeof(Resource))]
    public string? CodPhone { get; set; }

    [Display(Name = nameof(Resource.States), ResourceType = typeof(Resource))]
    public int StatesNumber => States == null ? 0 : States.Count;

    //relaciones
    public ICollection<State>? States { get; set; }

    public ICollection<Corporation>? Corporations { get; set; }
}