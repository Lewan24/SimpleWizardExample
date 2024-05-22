using DataAnnotationsExtensions;
using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SimpleWizard.Data.Entities.Steps;

public sealed class Step1 : IStepBase
{
    public int Id => 1;
    public string FormTitle => "Ogólne";

    [MinLength(4)]
    [MaxLength(50)]
    [Required]
    public string NazwaOgłoszenia { get; set; }

    [MaxLength(15)]
    [Required]
    public string Marka { get; set; }

    [MaxLength(15)]
    [Required]
    public string Model { get; set; }

    [Min(1)]
    [Max(1_000_000)]
    [Required]
    public int Cena { get; set; }

    string IStepLog.GetInfo()
    {
        return $"Nazwa Ogłoszenia: {NazwaOgłoszenia}\nMarka i model: {Marka}, {Model}\nCena wywoławcza: {Cena} zł";
    }

    WizardStep INextStep.GetNextStep()
        => Enums.WizardStep.Step2;
}
