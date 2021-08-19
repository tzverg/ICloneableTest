public class CloneableValidationAttribute : System.Attribute
{
    public bool CloneableValidate<T>(T targetType)
    {
        return (targetType is System.ICloneable);
    }
}