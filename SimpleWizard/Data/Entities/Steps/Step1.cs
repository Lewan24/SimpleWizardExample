using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step1 : IStep, INextStep
{
    public int Id => 1;
    public string FormTitle => "Dane Podstawowe";

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string FirstName { get; set; }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.Step2;
}
