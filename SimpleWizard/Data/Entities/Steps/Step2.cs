using DataAnnotationsExtensions;
using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step2 : IStepBase
{
    public int Id => 2;
    public string FormTitle => "Formularz sprzedaży samochodu";

    [Min(0)]
    [Required]
    public int Przebieg { get; set; }

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string Wyposazenie { get; set; }

    [MinLength(4)]
    [MaxLength(40)]
    [Required]
    public string Lokalizacja { get; set; }

    string IStepLog.GetInfo()
    {
        return $"Przebieg: {Przebieg}\nWyposażenie: {Wyposazenie}\nLokalizacja: {Lokalizacja}";
    }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.Step3;
}
