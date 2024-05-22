using SimpleWizard.Data.Entities.Enums;
using SimpleWizard.Data.Entities.Steps.Interfaces;
using System.Text;
using MySql.Data.MySqlClient;
using SimpleWizard.Data.Entities.Steps;

namespace SimpleWizard.Data.Entities;

public sealed class WizardService
{
    public event Action? OnStepUpdated;
    public string Name => "Testowy wizard";
    public IList<IStep> Steps { get => _steps; }
    public WizardStep CurrentStep { get; set; } = WizardStep.Invalid;

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

    private void ShowWizardStepsInConsole(List<IStep> sortedSteps)
    {
        StringBuilder stepsInfo = new StringBuilder();

        foreach (var step in sortedSteps)
            stepsInfo.AppendLine($"Step {step.Id}.:\n{(step as IStepLog)?.GetInfo()}\n");

        _logger.LogInformation($"Current steps information:\n{stepsInfo}");
    }

    public void InsertFormToDb()
    {
        var sortedSteps = _steps.OrderBy(x => x.Id)
                        .Distinct()
                        .ToList();

        ShowWizardStepsInConsole(sortedSteps);

        using (var conn = new MySqlConnection("Server=mysql60.mydevil.net;Port=3306;Database=m1158_wizard;User Id=m1158_wizard;Password=Kotarbinski24;SslMode=None"))
        {
            try
            {
                conn.Open();
                _logger.LogInformation("Successfully connected to database");

                Step1? step1 = sortedSteps[0] as Step1;
                Step2? step2 = sortedSteps[1] as Step2;
                Step3? step3 = sortedSteps[2] as Step3;

                _logger.LogInformation("Inserting data into db...");
                string sql = $"INSERT INTO `formularz` (`Nazwa`, `Marka`, `Model`, `Cena`, `Przebieg`, `Wyposażenie`, `Lokalizacja`, `Firma / Prywatnie`, `Czy uszkodzony`, `Kolor`) VALUES (" +
                    $"'{step1.NazwaOgłoszenia}', '{step1.Marka}', '{step1.Model}', '{step1.Cena}', '{step2.Przebieg}', '{step2.Wyposazenie}', '{step2.Lokalizacja}', '{step3.Seller}', '{step3.Damaged}', '{step3.Color}');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                _logger.LogInformation("Operation has been successfull. Closing connection...");
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Error while connecting to db");
            }
        }
    }
}
