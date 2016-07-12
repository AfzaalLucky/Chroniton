﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Chroniton.Schedules.Cron
{
    public class CronParser
    {
        const string secondsMinutesPattern = @"([0-5]?[0-9])";
        const string hoursPattern = @"([01]?[0-9]|2[0-3])";
        const string dayOfMonthPattern = @"(0?[1-9]|[12][0-9]|3[01])";
        const string optDayOfMonth = @"(W?)|";
        const string monthPattern = @"(JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC|(0?[0-9]|1[0-2]))";
        const string dayOfWeekPattern = @"(SUN|MON|TUE|WED|THUR?|FRI|SAT|[0-6])";
        const string optDayOfWeek = @"((L|#[1-5])?)|";
        const string yearPattern = @"([0-9]{4})";
        const string hyphenCommaPattern = @"((({0}{1}(\-{0})?)(,{0}(\-{0})?)*)|(\*{3}){2})";

        static readonly Regex _reg;

        static CronParser()
        {
            string[][] daList = new string[][]
            {
                new string[] { secondsMinutesPattern, string.Empty, string.Empty, @"(/([2-6]|1[025]|[23]0))?" },
                new string[] { secondsMinutesPattern, string.Empty, string.Empty ,@"(/([2-6]|1[025]|[23]0))?"},
                new string[] { hoursPattern, string.Empty, string.Empty, @"(/([23468]|12))?"},
                new string[] { dayOfMonthPattern, optDayOfMonth, @"|L|\?", string.Empty },
                new string[] { monthPattern, string.Empty, string.Empty, string.Empty },
                new string[] { dayOfWeekPattern, optDayOfWeek, @"|\?", string.Empty },
                new string[] { yearPattern, string.Empty, string.Empty, string.Empty },
            };
            var inner =
                (from t in daList
                 select string.Format(hyphenCommaPattern, t[0], t[1], t[2], t[3]))
                .Aggregate((s1, s2) => $"{s1} {s2}");

            _reg = new Regex($"^{inner}$");
        }

        public CronParser()
        {

        }

        public CronDateFinder Parse(string cronString)
        {
            Match m = _reg.Match(cronString);
            if (!m.Success)
            {
                return null;
            }

            return new CronDateFinder()
            {
                Seconds = m.Groups[1].Value,
                Minutes = m.Groups[14].Value,
                Hours = m.Groups[27].Value,
                DayOfMonth = m.Groups[40].Value,
                Month = m.Groups[52].Value,
                DayOfWeek = m.Groups[67].Value,
                Year = m.Groups[80].Value
            };
        }
    }
}
