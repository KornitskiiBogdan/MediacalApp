using MediacalApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MediacalApp.Messages;

public record class OpenedApp(MedicalProject Project, IServiceCollection Services);