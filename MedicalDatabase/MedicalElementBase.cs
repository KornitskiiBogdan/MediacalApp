namespace MedicalDatabase;

public abstract class MedicalElementBase
{
    private int _id;

    protected MedicalElementBase()
    {
    }

    protected MedicalElementBase(int id)
    {
        _id = id;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }
}