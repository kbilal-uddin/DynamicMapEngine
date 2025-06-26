namespace DynamicMapEngine.Interfaces
{
    public interface IMapProcessor
    {
        object Map(object data, string sourceType, string targetType);
    }
}
