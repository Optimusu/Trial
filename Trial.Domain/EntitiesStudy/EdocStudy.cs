using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial.Domain.Entities;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesStudy;

public class EdocStudy
{
    [Key]
    public Guid EdocStudyId { get; set; }

    [Display(Name = nameof(Resource.EdocCategory), ResourceType = typeof(Resource))]
    public Guid EdocCategoryId { get; set; }

    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    public Guid StudyId { get; set; }

    //Fecha de Creado el Archivo
    [Display(Name = nameof(Resource.Date_Created), ResourceType = typeof(Resource))]
    public DateTime? DateCreated { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Title), ResourceType = typeof(Resource))]
    public string Title { get; set; } = null!;

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = nameof(Resource.Comments), ResourceType = typeof(Resource))]
    public string Comments { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Photo), ResourceType = typeof(Resource))]
    public string? FileNameOriginal { get; set; }

    [Display(Name = nameof(Resource.Photo), ResourceType = typeof(Resource))]
    public string? File { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Signing { get; set; }

    //TODO: Cambio de ruta para Imagenes
    [NotMapped]
    public string? FileFullPath { get; set; }

    [NotMapped]
    public string? ImgBase64 { get; set; }

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }

    public EdocCategory? EdocCategory { get; set; }

    public Study? Study { get; set; }
}