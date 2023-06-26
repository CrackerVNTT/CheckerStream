using System;
using System.Collections.Generic;
using System.Threading;
using CodeExam.Texto;

namespace CodeExam
{
    internal class Program
    {

        public static List<Thread> threads;

        [STAThread]
        static void Main(string[] args)
        {
         
            PantallasOpciones.Menu();
        }

         
    }
}

