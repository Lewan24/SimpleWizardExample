using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.Text;

namespace SimpleWizard.Data.Entities;

public sealed class WizardService
{
    public event Action? OnStepUpdated;
    public string Name => "Testowy wizard";
    public IList<IStep> Steps { get => _steps; }
    public WizardStep CurrentStep { get; set; } = WizardStep.Step1;

    private IList<IStep> _steps = new List<IStep>();
    private readonly ILogger<WizardService> _logger;

    public WizardService(ILogger<WizardService> logger)
    {
        _logger = logger;
    }

    public void ResetToDefault()
    {
        _logger.LogInformation("Resetting wizard to default values...");

        _steps = new List<IStep>();
        CurrentStep = WizardStep.Step1;
    }

    public void AddStep(IStep step)
    {
        _steps.Add(step);
        _logger.LogInformation($"Added step no: {step.Id}");
    }

    public void ChangeCurrentState(INextStep step)
    {
        CurrentStep = step.GetNextStep();
        OnStepUpdated?.Invoke();
    }

    public void EndWizard()
    {
        CurrentStep = WizardStep.Invalid;
    }

    public void ShowWizardStepsInConsole()
    {
        var sortedSteps = _steps.OrderBy(x => x.Id)
                            .Distinct()
                            .ToList();

        StringBuilder stepsInfo = new StringBuilder();

        foreach (var step in sortedSteps)
            stepsInfo.AppendLine($"Step {step.Id}.:\n{(step as IStepLog)?.GetInfo()}\n");

        _logger.LogInformation($"Current steps information:\n{stepsInfo}");
    }
}
