using SimpleWizard.Data.Entities.Enums;

namespace SimpleWizard.Data.Entities.Steps.Interfaces;

public interface INextStep
{
    WizardStep GetNextStep();
}
