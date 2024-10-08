﻿using Microsoft.Extensions.DependencyInjection;
using Tools.Messaging;

namespace MedicalDatabase;

public class MedicalProject
{
    public IMessageBus MessageBus { get; }

    public IServiceProvider Services { get; private set; } = null!;

    private MedicalProject()
    {
        MessageBus = StrongReferenceMessageBus.Instance;
    }

    public static async Task<MedicalProject> CreateAsync()
    {
        var medicalProject = new MedicalProject();
        var serviceCollection = new ServiceCollection();
            
        await medicalProject.MessageBus.SendAsync<OpenedApp>(new OpenedApp(medicalProject, serviceCollection));

        medicalProject.Services = serviceCollection.BuildServiceProvider();

        await medicalProject.MessageBus.SendAsync<ServiceCreationCompleted>();
        return medicalProject;
    }
}