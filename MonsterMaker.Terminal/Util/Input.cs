using System;

namespace MonsterMaker.Terminal.Util
{
    internal static class Input
    {
        internal static T GetValue<T>(string valueName)
        {
            object returnValue;
            if (typeof(T) == typeof(int))
                returnValue = GetIntValue(valueName);
            else if (typeof(T) == typeof(string))
                returnValue = GetStringValue(valueName);
            else
                throw new NotImplementedException($"Type [{typeof(T)}] has not been implemented in input yet.");

            return (T)Convert.ChangeType(returnValue, typeof(T));
        }

        internal static T GetValue<T>(string valueName, Predicate<T> filter)
        {
            object returnValue;

            if (typeof(T) == typeof(int))
                returnValue = GetIntValue(valueName, (Predicate<int>) Convert.ChangeType(filter, typeof(Predicate<int>)));
            else if (typeof(T) == typeof(string))
                returnValue = GetStringValue(valueName, (Predicate<string>) Convert.ChangeType(filter, typeof(Predicate<string>)));
            else
                throw new NotImplementedException($"Type [{typeof(T)}] has not been implemented in input yet.");

            return (T)Convert.ChangeType(returnValue, typeof(T));
        }

        internal static string GetStringValue(string valueName)
        {
            do
            {
                Console.Write($"Provide a value for [{valueName}]: ");
                string result = Console.ReadLine();
                if (string.IsNullOrEmpty(result))
                    Console.WriteLine($"[{result}] is not a valid value for [{valueName}]!");
                else
                    return result;
            } while (true);
        }

        internal static string GetStringValue(string valueName, Predicate<string> filter)
        {
            do
            {
                var value = GetStringValue(valueName);
                if (filter(value))
                    return value;
                else
                    Console.WriteLine($"[{value}] does not comply with restrictions");
            } while (true);
        }

        internal static int GetIntValue(string valueName)
        {
            do
            {
                Console.WriteLine($"Provide a value for [{valueName}]: ");
                string result = Console.ReadLine();
                bool valid = int.TryParse(result, out int parsedValue);
                if (valid)
                    return parsedValue;
                else
                    Console.WriteLine($"[{result}] is not a valid value for [{valueName}]");
            } while (true);
        }

        internal static int GetIntValue(string valueName, Predicate<int> filter)
        {
            do
            {
                var value = GetIntValue(valueName);
                if (filter(value))
                    return value;
                else
                    Console.WriteLine($"[{value}] does not comply with restrictions");
            } while (true);
        }
    }
}
