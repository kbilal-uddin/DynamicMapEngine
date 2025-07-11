﻿using DynamicMapEngine.Common.Extensions;
using DynamicMapEngine.Common.Utils;
using DynamicMapEngine.Models.Internal;
using DynamicMapEngine.Models.Attributes;
using System.Net;

namespace DynamicMapEngine.Mapper.Helper
{
    public class ValidationHelper
    {
        public static void ValidateRequiredProperties(object obj)
        {
            if (obj is null)
                throw new StatusCodeException(HttpStatusCode.InternalServerError, new Error { Code = "", UserMessage = "Unknow error" });

            var type = obj.GetType();

            foreach (var prop in type.GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(RequiredFieldAttribute)))
                {
                    var value = prop.GetValue(obj);
                    if (value == null || value.Equals(GetDefault(prop.PropertyType)) || (value is string str && string.IsNullOrWhiteSpace(str)))
                        throw new StatusCodeException(HttpStatusCode.BadRequest,
                            new Error { Code = ErrorCache.RequiredField, UserMessage = ErrorCache.RequiredFieldMessage }, $"{prop.Name}" );
                }
            }
        }

        private static object? GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
