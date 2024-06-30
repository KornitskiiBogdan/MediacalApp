namespace MedicalDatabase.Objects;

public abstract class MedicalElementBase
{
    private Int64 _id;

    protected MedicalElementBase()
    {
    }

    protected MedicalElementBase(Int64 id)
    {
        _id = id;
    }

    public Int64 Id
    {
        get => _id;
        set => _id = value;
    }
}