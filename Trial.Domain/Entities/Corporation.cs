using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Resources;

namespace Trial.Domain.Entities;

public class Corporation
{
    [Key]
    public int CorporationId { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Corporation), ResourceType = typeof(Resource))]
    public string? Name { get; set; }

    [MaxLength(5, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.TypeDocument), ResourceType = typeof(Resource))]
    public string? TypeDocument { get; set; }

    [MaxLength(15, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Document), ResourceType = typeof(Resource))]
    public string? NroDocument { get; set; }

    [MaxLength(12, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = nameof(Resource.Phone), ResourceType = typeof(Resource))]
    public string? Phone { get; set; }

    [MaxLength(200, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Address), ResourceType = typeof(Resource))]
    public string? Address { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Country), ResourceType = typeof(Resource))]
    public int CountryId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.SoftPlan), ResourceType = typeof(Resource))]
    public int SoftPlanId { get; set; }

    //Tiempo Activo de la cuenta
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.DateStart), ResourceType = typeof(Resource))]
    public DateTime DateStart { get; set; }

    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.DateEnd), ResourceType = typeof(Resource))]
    public DateTime DateEnd { get; set; }

    [Display(Name = nameof(Resource.Logo), ResourceType = typeof(Resource))]
    public string? Imagen { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    //TODO: Cambio de ruta para Imagenes
    public string ImageFullPath => Imagen == string.Empty || Imagen == null
        ? $"https://localhost:7229/Images/NoImage.png"
        : $"https://localhost:7229/Images/ImgCorporation/{Imagen}";

    [NotMapped]
    public string? ImgBase64 { get; set; }

    //Relaciones
    public SoftPlan? SoftPlan { get; set; }

    public Country? Country { get; set; }

    public ICollection<Manager>? Managers { get; set; }

    public ICollection<Usuario>? Usuarios { get; set; }

    public ICollection<UsuarioRole>? UsuarioRoles { get; set; }
}