using Cargo.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cargo.Core.Extensions
{
    public static class ValidateExtension
    {
        /// <summary>
        /// 效验属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool ValidDataExtend<T>(this T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(NotEmptyAttribute), true))
                {
                    object oValue = prop.GetValue(t);//获取要校验类型的值
                    object[] oAttributeArr = prop.GetCustomAttributes(typeof(NotEmptyAttribute), true);//获取所有自定义的特性
                    foreach (NotEmptyAttribute attr in oAttributeArr)
                    {
                        if (!attr.ValidData(oValue))
                        {
                             return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
