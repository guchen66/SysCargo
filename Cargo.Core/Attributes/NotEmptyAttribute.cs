using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Attributes
{
    /// <summary>
    /// 自己造轮子，写一个属性不能为空的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmptyAttribute:Attribute
    {
        private string _message;

        public NotEmptyAttribute(string message)
        {
            _message = message;
        }

        public bool ValidData(object obj)
        {
            return !string.IsNullOrEmpty(obj?.ToString());
        }
    }
}
