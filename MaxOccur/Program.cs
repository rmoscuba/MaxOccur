using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxOccur
{

    class Result
    {

        // Calculate total Day Wage with extra hours
        public static Decimal totalDayWage(String timeStart, String timeEnd, Decimal hourlyWage, Decimal extraFactor)
        {
            TimeSpan _timeStart = TimeSpan.Parse(timeStart);
            TimeSpan _timeEnd = TimeSpan.Parse(timeEnd);

            TimeSpan _timeNormalStart = TimeSpan.Parse("8:00");
            TimeSpan _timeNormalEnd = TimeSpan.Parse("18:00");
            TimeSpan _normalHoursSpan = _timeNormalEnd - _timeNormalStart;
            Decimal _normalHours = (Decimal)_normalHoursSpan.TotalHours;

            TimeSpan _extraSpanStart = _timeNormalStart - _timeStart;
            Decimal _extraHoursStart = (Decimal)_extraSpanStart.TotalHours;

            TimeSpan _extraSpanEnd = _timeEnd - _timeNormalEnd;
            Decimal _extraHoursEnd = (Decimal)_extraSpanEnd.TotalHours;

            Decimal _extraHours = 0;
            if (_extraHoursStart < 0)
                _normalHours += _extraHoursStart;
            else
                _extraHours += _extraHoursStart;
            if (_extraHoursEnd < 0)
                _normalHours += _extraHoursEnd;
            else
                _extraHours += _extraHoursEnd;

            Decimal _dayWage = Math.Round(hourlyWage * (_normalHours + _extraHours * extraFactor), 2);
            return _dayWage;
        }

        /* 
         * Return the character that occurs the most in a given string. 
         * If more than one character  occurs the same number of times, 
         * then return all the characters in a comma-separated string.
         * 
         * Pure LINQ version.
         * 
         */
        public static string maxOccurPureLINQ(string s)
        {
            var group = s.GroupBy(c => c).Select(x => new { Key = x.Key, Value = x.Count() }).GroupBy(x => x.Value);
            var maxKey = group.Max(x => x.Key);
            List<char> items = group.Where(x => x.Key == maxKey).First().Select(x => x.Key).ToList();
            if (items.Any() && maxKey != 1)
            {
                items.Sort();
                return string.Join(", ", items);
            }
            else
            {
                return "No se repiten";
            }
        }

        /* 
         * Return the character that occurs the most in a given string. 
         * If more than one character  occurs the same number of times, 
         * then return all the characters in a comma-separated string.
         * 
         * Iterative version.
         * 
         */
        public static string maxOccur(string s)
        {

            // Dictionary of chars count
            IDictionary<char, int> charsCount = new Dictionary<char, int>();
            foreach (char c in s)
            {
                try
                {
                    charsCount[c] += 1;
                }
                catch
                {
                    charsCount.Add(c, 0);
                }

            }

            IOrderedEnumerable<KeyValuePair<char, int>> oderedCharsCount = charsCount.OrderByDescending(key => key.Value);

            List<char> repeatedChars = new List<char>();
            var currentItem = oderedCharsCount.FirstOrDefault();
            foreach (KeyValuePair<char, int> charCount in oderedCharsCount)
            {
                if (currentItem.Value != 0 && currentItem.Value == charCount.Value) {
                    repeatedChars.Add(charCount.Key);
                } 
                else
                {
                    break;
                }
            }

            repeatedChars.Sort();

            if (repeatedChars.Any()) {
                string commaSeparated = string.Join(", ", repeatedChars);
                return commaSeparated;
            } 
            else
            {
                return "No se repiten";
            }

        }

        public static void assertTrue(bool b)
        {
            if (b) Console.WriteLine("Ok"); else Console.WriteLine("Error");
        }

    }


    class Program
    {
        private static Random random = new Random();

        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void Main(string[] args)
        {

            Result.assertTrue(Result.totalDayWage("8:00", "18:00", 1, 2) == 10);
            Result.assertTrue(Result.totalDayWage("7:00", "19:00", 1, 2) == 14);
            Result.assertTrue(Result.totalDayWage("7:00", "17:00", 1, 2) == 11);
            Result.assertTrue(Result.totalDayWage("9:00", "19:00", 1, 2) == 11);
            Result.assertTrue(Result.totalDayWage("9:00", "17:00", 1, 2) == 8);

            string longString = randomString(1024);

            Result.assertTrue(Result.maxOccurPureLINQ(longString) == Result.maxOccur(longString));

            Result.assertTrue(Result.maxOccurPureLINQ("Computer Science") == "e");

            Result.assertTrue(Result.maxOccurPureLINQ("Edabit") == "No se repiten");

            Result.assertTrue(Result.maxOccurPureLINQ("system admin") == "m, s");

            Result.assertTrue(Result.maxOccurPureLINQ("the quick brown fox jumps over the lazy dog") == " ");

            Result.assertTrue(Result.maxOccur("Computer Science") == "e");

            Result.assertTrue(Result.maxOccur("Edabit") == "No se repiten");

            Result.assertTrue(Result.maxOccur("system admin") == "m, s");

            Result.assertTrue(Result.maxOccur("the quick brown fox jumps over the lazy dog") == " ");


        }
    }
}
