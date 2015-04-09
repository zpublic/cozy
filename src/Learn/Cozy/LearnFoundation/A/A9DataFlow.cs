using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Cozy.LearnFoundation.A
{
    class A9DataFlow
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            ActionBlockSample();
            SourceAndTargetBlocksSample();

            var target = SetupPipeline();
            target.Post("../..");
        }

        private static void ActionBlockSample()
        {
            var processInput = new ActionBlock<string>(s =>
            {
                Console.WriteLine("user input: {0}", s);
            });

            processInput.Post("111");
            processInput.Post("222");
        }

        private static void SourceAndTargetBlocksSample()
        {
            Task t1 = Task.Run(() => Producer());
            Task t2 = Task.Run(() => Consumer());
            Task.WaitAll(t1, t2);
        }

        static BufferBlock<string> buffer = new BufferBlock<string>();

        static void Producer()
        {
            buffer.Post("333");
            buffer.Post("444");
        }

        static async void Consumer()
        {
            while (true)
            {
                string data = await buffer.ReceiveAsync();

                Console.WriteLine("user input: {0}", data);
            }
        }



        static ITargetBlock<string> SetupPipeline()
        {

            var fileNames = new TransformBlock<string, IEnumerable<string>>(path =>
            {
                try
                {
                    return GetFileNames(path);
                }
                catch (OperationCanceledException)
                {
                    return Enumerable.Empty<string>();
                }
            });

            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(input =>
            {
                try
                {
                    return LoadLines(input);
                }
                catch (OperationCanceledException)
                {
                    return Enumerable.Empty<string>();
                }
            });

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines2 =>
            {
                return GetWords(lines2);
            });

            var display = new ActionBlock<IEnumerable<string>>(coll =>
            {
                foreach (var s in coll)
                {
                    Console.WriteLine(s);
                }
            });

            fileNames.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            // fileNames.LinkTo(loadLines, fn => fn.Count() > 0);

            return fileNames;
        }

        static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        yield return word;
                    }
                }
            }
        }

        static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Console.WriteLine("LoadLines {0}", line);
                        yield return line;
                    }
                }
            }
        }

        static IEnumerable<string> GetFileNames(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                Console.WriteLine("GetFileNames {0}", fileName);
                yield return fileName;
            }
        }
    }
}
