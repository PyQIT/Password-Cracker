using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PasswordCrackerClient
{
    public class ServersConnection
    {

        public static void PostRequestBruteForce(string url, User user, string bruteforcePattern, string serverId, Stopwatch stopWatch, int stopWatchHelper)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 10000000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"bruteforcePattern\":\"" + bruteforcePattern + "\"," +
                              "\"password\":\"" + user.GetPassword() + "\"} " +
                              serverId;

                streamWriter.Write(json);
            }

            Console.WriteLine("\n");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);

                if (result.Contains("Password found:"))
                {
                    stopWatch.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    Console.WriteLine("Total time: " + elapsedTime);
                }
                else
                {
                    stopWatchHelper++;
                    if(stopWatchHelper == 4)
                    {
                        stopWatch.Stop();

                        // Get the elapsed time as a TimeSpan value.
                        TimeSpan ts = stopWatch.Elapsed;

                        // Format and display the TimeSpan value.
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);

                        Console.WriteLine("Total time: " + elapsedTime);
                    }
                }

            }
        }

        public static void PostRequestDictionary(string url, User user, List<string> dictionary, string serverId, Stopwatch stopWatch, int stopWatchHelper)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 10000000;

            dictionary.Add(serverId);

            string dict = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(dict);
            }

            Console.WriteLine("\n");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);

                if (result.Contains("Password found:"))
                {
                    stopWatch.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    Console.WriteLine("Total time: " + elapsedTime);
                }
                else
                {
                    stopWatchHelper++;
                    if (stopWatchHelper == 1)
                    {
                        stopWatch.Stop();

                        // Get the elapsed time as a TimeSpan value.
                        TimeSpan ts = stopWatch.Elapsed;

                        // Format and display the TimeSpan value.
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);

                        Console.WriteLine("Total time: " + elapsedTime);
                    }
                }
            }
        }

        public static void PostRequestDictionaryPassword(string url, User user, string serverId)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 10000000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "password\":\"" + user.GetPassword() + " " + serverId;
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);

            }
        }
    }
}
