namespace UIAutomationTests.Models
{
    public static class Extension
    {
        public static T GetPropertyValue<T>(this Object obj, string property)
        {
            return (T)obj.GetType().GetProperty(property).GetValue(obj, null);
        }
    }
}
