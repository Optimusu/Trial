using System.ComponentModel.DataAnnotations;
using Trial.Domain.Resources;

namespace Trial.DomainLogic.ResponsesSec;

public class EmailDTO
{
    [EmailAddress(ErrorMessageResourceName = nameof(Resource.Validation_InvalidEmail), ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
    public string Email { get; set; } = null!;
}