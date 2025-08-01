﻿using System.ComponentModel.DataAnnotations;
using Trial.Domain.Resources;

namespace Trial.Domain.EntitiesGen;

public class DocumentType
{
    [Key]
    public int DocumentTypeId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Document), ResourceType = typeof(Resource))]
    public string DocumentName { get; set; } = null!;

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }
}