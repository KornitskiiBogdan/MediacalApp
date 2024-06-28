using MediacalApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MediacalApp.Messaging.Messages;

public record class OpenedApp(MedicalProject Project, IServiceCollection Services);