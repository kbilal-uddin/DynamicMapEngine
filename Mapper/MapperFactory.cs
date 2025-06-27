using Common.Extensions;
using Common.Utils;
using DynamicMapEngine.Models.Internal;
using Mapper.Interfaces;
using System.Net;
using System.Reflection;

namespace Mapper
{
    public class MapperFactory : IMapperFactory
    {
        public object? GetInstance(Type sourceModelType, Type targetModelType)
        {
            var mapperType = GetMapper(sourceModelType, targetModelType);

            if (mapperType is null)
            {
                throw new StatusCodeException(HttpStatusCode.InternalServerError, 
                    new Error { Code = ErrorCache.NoMatchingMapper, UserMessage = ErrorCache.NoMatchingMapperMessage }, 
                    $"{sourceModelType.FullName}", $"{targetModelType.FullName}");
            }

            return Activator.CreateInstance(mapperType);
        }

        private static Type? GetMapper(Type sourceType, Type targetType)
        {

            var mapperType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    typeof(IObjectMapper<,>).MakeGenericType(sourceType, targetType).IsAssignableFrom(t));

            return mapperType;
        }
    }


}
