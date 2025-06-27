namespace Mapper.Interfaces
{
    public interface IModelTypeResolver
    {
        Type ResolveType(string name);
        object GetSourceObject(object data, Type type);
        object CreateInstance(string name, params object[] constructorArgs);
    }
}
