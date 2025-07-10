﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial.Domain.Resources;

namespace Trial.Domain.Entities;

public class User : IdentityUser
{
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

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.JobPosition), ResourceType = typeof(Resource))]
    public string JobPosition { get; set; } = null!;

    //Identificacion de Origenes y Role del Usuario
    [Display(Name = nameof(Resource.Origin), ResourceType = typeof(Resource))]
    public string? UserFrom { get; set; }

    [Display(Name = nameof(Resource.Photo), ResourceType = typeof(Resource))]
    public string? PhotoUser { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    [NotMapped]
    public string? Pass { get; set; }

    public int? CorporationId { get; set; }

    //Relaciones
    public Corporation? Corporation { get; set; }

    public ICollection<UserRoleDetails>? UserRoleDetails { get; set; }
}