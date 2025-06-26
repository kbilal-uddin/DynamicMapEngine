using DynamicMapEngine.Handler;
using DynamicMapEngine.Interfaces;

namespace DynamicMapEngine.Processor
{
    public class MapProcessor : IMapProcessor
    {
        public object Map(object data, string sourceType, string targetType)
        {
            //Validate
            new ValidationHandler().Validate(data, sourceType, targetType);

            //Map
            return new MapHandler().Map(data, sourceType, targetType);
        }
    }
}
