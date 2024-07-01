using Microsoft.Extensions.DependencyInjection;

namespace MedicalDatabase;

public record class OpenedApp(MedicalProject Project, IServiceCollection Services);