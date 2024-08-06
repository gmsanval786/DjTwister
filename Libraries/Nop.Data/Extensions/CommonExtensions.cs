using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public static string ReadableTimeFormat(this string timeNvarchar)
        {
            if (TimeSpan.TryParse(timeNvarchar, out TimeSpan timeSpan))
            {
                int hours = timeSpan.Hours;
                int minutes = timeSpan.Minutes;

                var formattedTime = "";

                if (hours > 0)
                {
                    formattedTime += $"{hours} hour{(hours > 1 ? "s" : "")}";
                }

                if (minutes > 0)
                {
                    if (formattedTime.Length > 0)
                    {
                        formattedTime += " and ";
                    }
                    formattedTime += $"{minutes} min{(minutes > 1 ? "s" : "")}";
                }

                return string.IsNullOrEmpty(formattedTime) ? "0 mins" : formattedTime;
            }

            return "Time not given";
        }
    }
}