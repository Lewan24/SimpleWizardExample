using DataAnnotationsExtensions;
using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step1 : IStepBase
{
    public int Id => 1;
    public string FormTitle => "Dane Podstawowe";

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string FirstName { get; set; }

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string LastName { get; set; }

    [Min(18)]
    [Max(100)]
    [Required]
    public int Age { get; set; }

    string IStepLog.GetInfo()
    {
        return $"Imie: {FirstName}\nNazwisko: {LastName}\nWiek: {Age}";
    }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.Step2;
}
