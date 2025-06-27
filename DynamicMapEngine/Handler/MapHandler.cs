using DynamicMapEngine.Interfaces;
using Mapper;
using DynamicMapEngine.Mapper.Interfaces;
using System.Reflection;

namespace DynamicMapEngine.Handler
{
    public class MapHandler : IMapHandler
    {
        private readonly IModelTypeResolver _typeResolver;
        private readonly IMapperFactory _mapperFactory;
        public MapHandler(IModelTypeResolver typeResolver, IMapperFactory mapperFactory)
        {
            _typeResolver = typeResolver;
            _mapperFactory = mapperFactory;
        }

        public object Map(object data, string sourceName, string targetName)
        {
            Type _sourceType = _typeResolver.ResolveType(sourceName);
            var sourceObj = _typeResolver.GetSourceObject(data, _sourceType);
            var target = _typeResolver.CreateInstance(targetName);

            DoMapping(sourceObj, ref target);

            return target;
        }

        private void DoMapping(object source, ref object target)
        {
            var instance = _mapperFactory.GetInstance(source.GetType(), target.GetType());

            if (instance is not null)
            {
                var mapperType = instance.GetType();

                var mapMethod = mapperType.GetMethod("Map");

                if (mapMethod is not null)
                {
                    var parameters = new object[] { source };

                    try
                    {
                        target = mapMethod.Invoke(instance, parameters);
                    }
                    catch (TargetInvocationException ex)
                    {
                        throw ex.InnerException ?? ex; 
                    }

                }
            }
        }
    }
}
