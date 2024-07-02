using System;

namespace MedicalApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class MedicalExtensionAttribute : Attribute
    {
    }
}
