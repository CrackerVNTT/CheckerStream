using CodeExam.Checker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Request;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Data;
using CodeExam.Pantalla;
using System.Collections;

namespace CodeExam.Utils
{
    public class Config
    {

        public static int ThreadsAmount()
        {
            int Threads;

            for (; ; )
            {
                try
                {
                    
                    Presentacion.ConsolaE("Threads: ", "+");
                    Threads = Convert.ToInt32(Console.ReadLine());


                    if (Threads > 0)
                    {
                        break;
                    }
                }
                catch
                {

                    Console.Clear();
                    continue;
                }
            }

            return Threads;
        }

        public static ProxyType Proxy()
        {
            for (; ; )
            {
                try
                {
                    Presentacion.ConsolaE("HTTP", "1");
                    Console.WriteLine();
                    Presentacion.ConsolaE("SOCKS4", "2");
                    Console.WriteLine();
                    Presentacion.ConsolaE("SOCKS5", "3");
                    Console.WriteLine();
                    Presentacion.ConsolaE("NO", "4");
                    Console.WriteLine();
                    Presentacion.ConsolaE("Seleccione la tecla con el proxy que va trabajar.....", "+");

                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.WriteLine();

                    switch (key.KeyChar)
                    {
                        case '1':
                            return ProxyType.Http;
                        case '2':
                            return ProxyType.Socks4;
                        case '3':
                            return ProxyType.Socks5;
                        case '4':
                            return ProxyType.No;
                        default:
                            Presentacion.ConsolaE("Opción inválida. Por favor, selecciona una opción válida.", "-");
                            Console.WriteLine();
                            Console.Clear();
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    continue;
                }
            }
        }


        public static List<Thread> GetThreads(int amount, ProxyType proxy)
        {
            List<Thread> threadList = new List<Thread>();
            SemaphoreSlim semaphore = new SemaphoreSlim(0);

            for (int i = 0; i < amount; i++)
            {
                Thread thread = new Thread(() =>
                {
                    HttpRequest.Checker(proxy);
                    semaphore.Wait();
                });

                thread.Start();
                threadList.Add(thread);
            }

            return threadList;
        }
        public static void StopThreads(List<Thread> threads)
        { 
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

        }

        public static List<string> ProxyList()
        {
            List<string> lista = new List<string>();
            string proxy = GetUtils("ProxyList");


            if (string.IsNullOrEmpty(proxy))
            {
                Presentacion.ConsolaE("La ruta de acceso al archivo de proxies está vacía.", "ERROR");
                return lista;
            }


            foreach (string text in File.ReadAllLines(proxy))
            {
                bool flag = text.Contains(":");
                if (flag)
                {
                    lista.Add(text);
                }
            }

            return lista ?? new List<string>();
        }

        public static ConcurrentQueue<string> Combos()
        {
            ConcurrentQueue<string> cooque = new ConcurrentQueue<string>();
            string data = GetUtils("ComboList");


            if (string.IsNullOrEmpty(data))
            {
                Presentacion.ConsolaE("La ruta de acceso al archivo de proxies está vacía.", "ERROR");
                return cooque;
            }

            foreach (string text in File.ReadAllLines(data))
            {
                bool flag = text.Contains(":");

                if (flag)
                {
                    cooque.Enqueue(text);
                }
            }
            return cooque ?? new ConcurrentQueue<string>();
        }

        public static string GetUtils(string title)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Txt (*.txt) | *.txt";
                openFileDialog.Title = title;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string text in File.ReadAllLines(openFileDialog.FileName))
                    {
                        text.Replace("\t", "");
                        text.Replace("\r", "");
                        text.Trim();
                        text.Replace(" ", "");
                    }

                    filePath = openFileDialog.FileName;
                }
            }
            return filePath;
        }

    }
}
