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
            InternalReservation,

            [Description("internal.room")]
            InternalRoom,

            [Description("internal.guest")]
            InternalGuest,

            #endregion

            #region External

            #region Google

            [Description("google.reservation")]
            GoogleReservation,

            [Description("google.room")]
            GoogleRoom,

            [Description("google.guest")]
            GoogleGuest,

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
