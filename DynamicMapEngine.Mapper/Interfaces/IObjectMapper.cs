namespace DynamicMapEngine.Mapper.Interfaces
{
    public interface IObjectMapper<TSource, TTarget>
    {
        TTarget Map(TSource source);
    }
}
