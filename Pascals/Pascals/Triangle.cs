using System;
using System.Collections.Generic;

namespace Pascals
{
    public static class Triangle
    {
        private static bool evaluating = true;

        public static void Main(string[] args)
        {
            Console.Title = "Pascal's Triangle Evaluator";

            while (true)
            {
                evaluating = true;

                Console.WriteLine("Evaluate Pascal's Triangle at row: ");

                while (evaluating)
                    Evaluate();
            }
        }

        private static void Evaluate()
        {
            int arg = 0;
            
            arg = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Evaluating... \n");

            int startTime = Time();

            WriteLine(GetRow(arg));
            Console.WriteLine("\nCompleted in " + (Time() - startTime) + "ms\n");

            evaluating = false;
        }
        
        private static void WriteLine(string[] vs)
        {
            string line = "";
            foreach (string s in vs)
            {
                line += s + " ";
            }

            Console.WriteLine(line);
        }

        private static string[] GetRow(int i)
        {
            Dictionary<int, List<string>> tri = new Dictionary<int, List<string>>
            {
                { 0, new List<string> { "0", "1", "0" } }
            };

            for (int y = 1; y <= i; y++)
            {
                tri.Add(y, new List<string>());
                tri[y].Add("0");

                for (int x = 1; x <= y; x++)
                {
                    string v1 = tri[y - 1][x - 1];
                    string v2 = tri[y - 1][x];

                    string v = Add(v1, v2);
                    int.TryParse(v, out int val);

                    tri[y].Add(v);
                }

                tri[y].Add("1");
                tri[y].Add("0");
            }

            tri[i].RemoveAt(0);
            tri[i].RemoveAt(tri[i].Count - 1);

            return tri[i].ToArray();
        }

        private static string Add(string s1, string s2)
        {
            List<char> _1 = new List<char>(), _2 = new List<char>(), val = new List<char>();

            for (int i = 0; i < s1.Length; i++)
                _1.Add(s1[i]);
            for (int i = 0; i < s2.Length; i++)
                _2.Add(s2[i]);

            _1.Reverse();
            _2.Reverse();

            int max = _1.Count > _2.Count ? _1.Count : _2.Count;

            int overflow = 0;
            for (int i = 0; i <= max; i++)
            {
                int v1 = 0, v2 = 0;
                if (i < _1.Count)
                    int.TryParse(_1[i].ToString(), out v1);
                if (i < _2.Count)
                    int.TryParse(_2[i].ToString(), out v2);

                int v = v1 + v2 + overflow;
                if (v >= 10)
                {
                    overflow = 1;
                    v -= 10;
                }
                else
                    overflow = 0;

                val.Add(v.ToString()[0]);
            }
            val.Reverse();

            for (int i = 0; i < val.Count;)
                if (val[i] == '0')
                    val.RemoveAt(i);
                else
                    break;

            string value = "";
            foreach (char c in val)
                value += c;

            return value;
        }

        private static int Time()
        {
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second + minute * 60;
            int millis = DateTime.Now.Millisecond + second * 1000;

            return millis;
        }
    }
}