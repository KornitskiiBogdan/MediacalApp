using MedicalDatabase;
using ReactiveUI;

namespace MedicalApp.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    protected ViewModelBase()
    {
    }

    public abstract MedicalProject Project { get; }
}
