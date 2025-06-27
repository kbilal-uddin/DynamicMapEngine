namespace DynamicMapEngine.Mapper.Interfaces
{
    public interface IMapperFactory
    {
        object? GetInstance(Type sourceType, Type targetType);
    }
}
