using CodeExam.Checker;
using CodeExam.Pantalla;
using CodeExam.Utils;
using Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeExam.Texto
{
    public class PantallasOpciones
    {
        public static void Menu()
        {

            ShowInfo();
            int ThreadsFinal = Config.ThreadsAmount();
            Console.Clear();
            iText[0] = ThreadsFinal.ToString();
            ShowInfo();

            ProxyType proxyType = Config.Proxy();
            iText[1] = proxyType.ToString();
            Console.Clear();
            ShowInfo();


            bool flag = proxyType != ProxyType.No;


            if (flag)
            {
                for(; ; )
                {
                    HttpRequest.list = Config.ProxyList();

                    if(HttpRequest.list.Count > 0)
                    {
                        Console.Clear();
                        iText[2] = HttpRequest.list.Count().ToString();
                        ShowInfo();
                        break;
                    }
                }
            }


            for(; ; )
            {
                HttpRequest.coqueue = Config.Combos();

                if(HttpRequest.coqueue.Count > 0)
                {
                    break;
                }
            }
            Console.Clear();
            iText[3] = HttpRequest.coqueue.Count().ToString();
            ShowInfo();


            Program.threads = Config.GetThreads(ThreadsFinal, proxyType);

        }



        private static string infoText = "\n\n\t Thread     : [ {0} ]      \n\t Proxy Type : [ {1} ]      \n\t Proxylist  : [ {2} ]      \n\t Combolist  : [ {3} ] \n\n";

        private static string[] iText = new string[]
        {

            "-X-",
            "-X-",
            "-X-",
            "-X-",
            "-X-"
        };

        public static void ShowInfo()
        {
            Presentacion.ConsolaE(string.Format(infoText, iText[0], iText[1], iText[2], iText[3]));
        }
    }
}
