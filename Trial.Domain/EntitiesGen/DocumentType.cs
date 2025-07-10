using System.ComponentModel.DataAnnotations;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Entities;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesGen;

public class DocumentType
{
    [Key]
    public Guid DocumentTypeId { get; set; }

    [MaxLength(5, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Document), ResourceType = typeof(Resource))]
    public string DocumentName { get; set; } = null!;

    [MaxLength(200, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Description), ResourceType = typeof(Resource))]
    public string? Descripcion { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //Relaciones
    public int CorporationId { get; set; }

    public Corporation? Corporation { get; set; }

    public Usuario? Usuario { get; set; }

    public ICollection<Usuario>? Usuarios { get; set; }
}