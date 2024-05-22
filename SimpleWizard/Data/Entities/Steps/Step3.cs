using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step3 : IStepBase
{
    public int Id => 3;
    public string FormTitle => "Informacje dodatkowe";

    [Required]
    public bool Seller { get; set; }

    [Required]
    public bool Damaged { get; set; }

    [Required]
    public string Color { get; set; }

    string IStepLog.GetInfo()
    {
        return $"Czy sprzedawany prywatnie ? : {Seller}\nUszkodzony ? : {Damaged}\nKolor : {Color}";
    }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.End;
}