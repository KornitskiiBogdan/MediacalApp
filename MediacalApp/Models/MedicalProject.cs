using System;
using System.Threading.Tasks;
using MediacalApp.Messages;
using MediacalApp.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace MediacalApp.Models;

public class MedicalProject
{
    public IMessageBus MessageBus { get; }

    public IServiceProvider Services { get; private set; } = null!;

    private MedicalProject()
    {
        MessageBus = StrongReferenceMessageBus.Instance;
    }

    public static async Task<MedicalProject> Create()
    {
        var medicalProject = new MedicalProject();
        var serviceCollection = new ServiceCollection();
            
        await medicalProject.MessageBus.SendAsync<OpenedApp>(new OpenedApp(medicalProject, serviceCollection));

        medicalProject.Services = serviceCollection.BuildServiceProvider();
        return medicalProject;
    }
}
