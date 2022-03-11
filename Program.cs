using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YaProfi22PIprefinal
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var data = File.ReadAllLines("data.txt").Where(l => l.Length>1).Select(l => Regex.Split(l, @"\s{2,}")).Select(l => new Tuple<DateTime, string, double>(DateTime.Parse(l[0]), l[1], double.Parse(l[2], System.Globalization.CultureInfo.InvariantCulture)));
            var input = Regex.Split(Console.ReadLine(), @"\s{2,}");
            var input1 = new Tuple<DateTime, string, double>(DateTime.Parse(input[0]), input[1], double.Parse(input[2], System.Globalization.CultureInfo.InvariantCulture));
            var searchL = data.Where(d => d.Item1.Hour == input1.Item1.Hour).Average(d => d.Item3);
            var searchR = ((input1.Item1.Minute == 0 && input1.Item1.Second == 0) ? data.Where(d => d.Item1.Hour == input1.Item1.Hour) : data.Where(d => d.Item1.Hour == input1.Item1.Hour + 1)).Average(d => d.Item3);
            var aspect = input1.Item1.Minute / 60;
            var res = searchL * (1 - aspect) + searchR * aspect;
            Console.WriteLine((input1.Item3 > res * 0.9 && input1.Item3 < res * 1.3) ? "Трафик в пределах нормы" : (input1.Item3 < res * 0.9) ? "Трафик ниже нормы" : "Трафик выше нормы");
        }
    }
}
