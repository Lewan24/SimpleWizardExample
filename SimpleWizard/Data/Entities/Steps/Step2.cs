using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step2 : IStep, INextStep
{
    public int Id => 2;
    public string FormTitle => "Dane podstawowe";

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string LastName { get; set; }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.End;
}
