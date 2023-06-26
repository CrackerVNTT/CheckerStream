using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CodeExam.Pantalla
{
    public class Presentacion
    { 
        public static void ConsolaE(string mensaje, object data = null)
        {
            Console.Write("     [", Color.White);
            Console.Write(data, Color.Orange);
            Console.Write("]", Color.White);
            for (int i = 0; i < mensaje.Length; i++)
            {
                 

                Console.Write(mensaje[i], Color.GhostWhite);
                Thread.Sleep(60);
            }
           
        }


    }
}
