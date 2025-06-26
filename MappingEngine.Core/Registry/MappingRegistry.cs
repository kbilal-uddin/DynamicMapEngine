using System.ComponentModel;
using System.Reflection;

namespace Mapper.Registry
{
    public static class MappingRegistry
    {
        public enum SupportedMappings
        {
            #region INTERNAL

            [Description("internal.reservation")]
            INTERNAL_RESERVATION,

            [Description("internal.room")]
            INTERNAL_ROOM,

            [Description("internal.guest")]
            INTERNAL_GUEST,

            #endregion

            #region External

            #region Google

            [Description("google.reservation")]
            GOOGLE_RESERVATION,

            [Description("google.room")]
            GOOGLE_ROOM,

            [Description("google.guest")]
            GOOGLE_GUEST,

            #endregion

            #endregion
        }

        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field is not null)
            {
                var attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr is not null)
                    return attr.Description;
            }
            return value.ToString();
        }
    }
}
