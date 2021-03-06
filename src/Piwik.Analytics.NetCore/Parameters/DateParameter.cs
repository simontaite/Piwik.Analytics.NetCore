﻿#region license

// http://www.gnu.org/licenses/gpl-3.0.html GPL v3 or later

#endregion

using System;

namespace Piwik.Analytics.NetCore.Parameters
{
    internal class DateParameter : Parameter
    {
        private DateTimeOffset _date;

        public DateParameter(string name, DateTimeOffset date) : base(name)
        {
            _date = date;
        }

        public override string Serialize()
        {
            var parameter = string.Empty;

            if (!_date.Equals(default(DateTimeOffset)))
            {
                parameter = "&" + Name + "=" + FormatDate(_date);
            }

            return parameter;
        }

        public static string FormatDate(DateTimeOffset date)
        {
            return string.Format("{0:yyyy-MM-dd}", date);
        }
    }
}