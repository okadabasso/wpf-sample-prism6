using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Infrastructure.DataTypes
{
    public static class EnumExtension
    {
        public static string DisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var memberInfo = enumType.GetMember(value.ToString()).First();
            if (memberInfo == null || !memberInfo.CustomAttributes.Any())
            {
                return value.ToString();
            }

            var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute == null)
            {
                return value.ToString();
            }
            if (displayAttribute.ResourceType != null && displayAttribute.Name != null)
            {
                var manager = new ResourceManager(displayAttribute.ResourceType);
                return manager.GetString(displayAttribute.Name) ?? displayAttribute.Name;
            }

            return displayAttribute.Name ?? value.ToString();
        }
    }
}
