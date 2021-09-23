namespace MonsterMaker.Terminal
{
    using MonsterMaker.Terminal.Util;
    using System;
    using System.Collections.Generic;

    class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();

            // fetch Name
            string MonsterNameAtribute = "Monster name";
            var name = Input.GetStringValue(MonsterNameAtribute);
            parameters.Add(MonsterNameAtribute, new Parameter { Name = MonsterNameAtribute, Type = typeof(string), Value = name });

            foreach (var (key, item) in parameters)
            {
                Console.WriteLine($"We got this: {item.Value} for {key}");
            }

            Console.ReadLine();
        }

        internal struct Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public Type Type { get; set; }
        }
    }
}
