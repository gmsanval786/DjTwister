using System;
using System.Net;

namespace Nop.Data.Extensions
{
    /// <summary>
    /// Common extensions
    /// </summary>
    public static class CommonExtensions
    {       
        public static string ToYesAndNo(this bool value)
        {
            return value ? "Yes" : "No";
        }   

        public static string ToYesAndNo(this bool? value)
        {
            return value.HasValue ? (value.Value ? "Yes" : "No") : "";
        }
    }
}