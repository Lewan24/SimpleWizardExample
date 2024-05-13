using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step2 : IStepBase
{
    public int Id => 2;
    public string FormTitle => "Dane dodatkowe";

    [StringLength(9)]
    [Required]
    public string PhoneNumber { get; set; }

    string IStepLog.GetInfo()
    {
        return $"Numer telefonu: {PhoneNumber}";
    }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.End;
}
