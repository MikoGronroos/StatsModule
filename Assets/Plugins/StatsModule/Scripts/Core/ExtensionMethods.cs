using System.Linq;

public static class ExtensionMethods
{
    public static T GetCoreValue<T>(this ICoreValue[] coreValues, string id)
    {
        return (T)(object)coreValues.Where(t => t.Id.Equals(id)).FirstOrDefault();
    }

}