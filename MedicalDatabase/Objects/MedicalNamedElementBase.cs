﻿namespace MedicalDatabase.Objects;

public abstract class MedicalNamedElementBase : MedicalElementBase
{
    private string _name;

    protected MedicalNamedElementBase() : base()
    {
        _name = string.Empty;
    }

    protected MedicalNamedElementBase(Int64 id, string name) : base(id)
    {
        _name = name;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }
}