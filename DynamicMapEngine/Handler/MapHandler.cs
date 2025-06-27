using DynamicMapEngine.Interfaces;
using Mapper;
using System.Reflection;

namespace DynamicMapEngine.Handler
{
    public class MapHandler : IMapHandler
    {
        public object Map(object data, string sourceName, string targetName)
        {
            Type _sourceType = ModelTypeResolver.ResolveType(sourceName);
            var sourceObj = ModelTypeResolver.GetSourceObject(data, _sourceType);
            var target = ModelTypeResolver.CreateInstance(targetName);

            DoMapping(sourceObj, ref target);

            return target;
        }

        private void DoMapping(object source, ref object target)
        {
            var instance = MapperFactory.GetInstance(source.GetType(), target.GetType());

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
