using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!Directory.Exists("output.sql"))
                Directory.CreateDirectory("output.sql");

            if (!Directory.Exists("output.xml"))
                Directory.CreateDirectory("output.xml");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            foreach (var file in Directory.EnumerateFiles("binary", "*.bss"))
            {
                Console.WriteLine("Reading {0}", file);

                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(IBssEntry).IsAssignableFrom(p));

                foreach (var type in types)
                {
                    var tp = type.GetAttributeValue((BssTableAttribute x) => x.Name == Path.GetFileNameWithoutExtension(file));
                    if (tp)
                    {
                        var instance = (IBssEntry) Activator.CreateInstance(type);
                        using (var ms = File.OpenRead(file))
                        using (var br = new BinaryReader(ms))
                        {
                            try
                            {
                                instance.ReadTable(br);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\tFailed to read table.");
                            }
                        }
                        break;
                    }
                }
            }

            Console.WriteLine("Done!!!");
            Console.ReadLine();
        }
    }
}
