using System.ComponentModel.DataAnnotations;
using Trial.Domain.Resources;

namespace Trial.DomainLogic.ResponsesSec;

public class ChangePasswordDTO
{
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Resource.Validation_BetweenLength), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Key_CurrentPassword), ResourceType = typeof(Resource))]
    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Resource.Validation_BetweenLength), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Key_NewPassword), ResourceType = typeof(Resource))]
    public string NewPassword { get; set; } = null!;

    [Compare("NewPassword", ErrorMessageResourceName = nameof(Resource.Validation_PasswordMismatch), ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Resource.Validation_BetweenLength), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Key_ConfirmPassword), ResourceType = typeof(Resource))]
    public string Confirm { get; set; } = null!;
}