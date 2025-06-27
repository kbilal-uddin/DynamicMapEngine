namespace Mapper.Interfaces
{
    public interface IObjectMapper<TSource, TTarget>
    {
        TTarget Map(TSource source);
    }
}
