using Microsoft.Extensions.DependencyInjection;

namespace MediacalApp.Models;

public record class OpenedApp(MedicalProject Project, IServiceCollection Services);