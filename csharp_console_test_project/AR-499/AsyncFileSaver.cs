using System.IO;
using System.Threading;

namespace AR_499
{
    public static class AsyncFileSaver
    {
        static public async void ReadWriteAsync(string stringForWrite)
        {
            System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: начало асинхронного метода");

            using (StreamWriter streamWriter = new StreamWriter("backup.txt", false))
            {
                await streamWriter.WriteLineAsync(stringForWrite);
                System.Console.WriteLine($"количество потоков: {System.Diagnostics.Process.GetCurrentProcess().Threads.Count}");
            }
            using (StreamReader streamReader = new StreamReader("backup.txt"))
            {
                string targetString = await streamReader.ReadToEndAsync();
                System.Console.WriteLine("\nсчитанная строка:" + targetString);
            }

            System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: конец асинхронного метода");
            System.Console.WriteLine($"количество потоков: {System.Diagnostics.Process.GetCurrentProcess().Threads.Count}");
        }
    }
}
