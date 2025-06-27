using DynamicMapEngine.Handler;
using DynamicMapEngine.Interfaces;

namespace DynamicMapEngine.Processor
{
    public class MapProcessor : IMapProcessor
    {
        private readonly IMapHandler _mapHandler;

        public MapProcessor(IMapHandler mapHandler)
        {
            _mapHandler = mapHandler;
        }

        public object Map(object data, string sourceType, string targetType)
        {
            //Validate
            new ValidationHandler().Validate(data, sourceType, targetType);

            //Map
            return _mapHandler.Map(data, sourceType, targetType);
        }
    }
}
