using System.ComponentModel.DataAnnotations;

namespace Trial.Domain.Enum;

public enum TrialPhase
{
    [Display(Name = "Phase I")]
    PhaseI = 1,

    [Display(Name = "Phase II")]
    PhaseII = 2,

    [Display(Name = "Phase III")]
    PhaseIII = 3,

    [Display(Name = "Phase IV")]
    PhaseIV = 4
}