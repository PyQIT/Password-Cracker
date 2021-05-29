using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace PasswordCracker
{
    public class Endpoints : NancyModule
    {
        public Endpoints()
        {

            Post("/crack", args =>
            {
                var jsonString = this.Request.Body.AsString();
                var response = " ";

                if (jsonString.Contains("bruteforcePattern"))
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    Console.WriteLine("- Bruteforce attack detected - ");

                    string tmp = jsonString.Substring(jsonString.IndexOf("password\":") + 11);
                    User.SetPassword(tmp.Substring(0, tmp.IndexOf("\"}")));

                    string tmpPattern = jsonString.Substring(jsonString.IndexOf("bruteforcePattern\":") + 20);
                    string serverId = jsonString.Substring(jsonString.IndexOf("\"}") + 3, 8);

                    Console.WriteLine("password    " + tmp.Substring(0, tmp.IndexOf("\"}")));
                    Console.WriteLine("tmpPattern    " + tmpPattern);
                    Console.WriteLine("serverId    " + serverId);
                    Console.WriteLine("Pattern    ||" + Int32.Parse(tmpPattern.Substring(0, tmpPattern.IndexOf("\",\"password\""))) + "||");

                    Bruteforce.setRange(Int32.Parse(tmpPattern.Substring(0, tmpPattern.IndexOf("\",\"password\""))));

                    ThreadPool.QueueUserWorkItem(state => {

                        int threadTmp = 10;
                        bool test = false;
                        bool test1 = false;
                        bool test2 = false;
                        int wordsCounterTmp = 0;

                        while (true)
                        {
                            if (stopWatch.Elapsed.Seconds == 10 || stopWatch.Elapsed.Seconds == 20 || stopWatch.Elapsed.Seconds == 30)
                            {
                                if (stopWatch.Elapsed.Seconds == 10 && test == false)
                                {
                                    test = true;
                                    // Get the elapsed time as a TimeSpan value.
                                    TimeSpan ts1 = stopWatch.Elapsed;

                                    // Format and display the TimeSpan value.
                                    string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                                        ts1.Milliseconds / 10);

                                    response = response + Environment.NewLine + serverId + " - " + (Bruteforce.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                    wordsCounterTmp = Bruteforce.getWordsCounter() - wordsCounterTmp;
                                }
                                if (stopWatch.Elapsed.Seconds == 20 && test1 == false)
                                {
                                    test1 = true;
                                    // Get the elapsed time as a TimeSpan value.
                                    TimeSpan ts1 = stopWatch.Elapsed;

                                    // Format and display the TimeSpan value.
                                    string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                                        ts1.Milliseconds / 10);

                                    response = response + Environment.NewLine + serverId + " - " + (Bruteforce.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                    wordsCounterTmp = Bruteforce.getWordsCounter() - wordsCounterTmp;
                                }
                                if (stopWatch.Elapsed.Seconds == 30 && test2 == false)
                                {
                                    test2 = true;
                                    // Get the elapsed time as a TimeSpan value.
                                    TimeSpan ts1 = stopWatch.Elapsed;

                                    // Format and display the TimeSpan value.
                                    string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                                        ts1.Milliseconds / 10);

                                    response = response + Environment.NewLine + serverId + " - " + (Bruteforce.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                    wordsCounterTmp = Bruteforce.getWordsCounter() - wordsCounterTmp;
                                }
                            }
                        }
                    });

                    string password = Bruteforce.CrackPassword();

                    stopWatch.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    if (password != null)
                    {
                        Console.WriteLine(serverId + "- Password found: " + password + " - Time: " + elapsedTime);
                        return Response.AsJson(serverId + "- Bruteforce attack detected - Password found: " + password + " - Time: " + elapsedTime + response);
                    }
                    Console.WriteLine(serverId + "- Password not found - Time: " + elapsedTime);
                    return Response.AsJson(serverId + "- Bruteforce attack detected - Password not found - Time: " + elapsedTime + response);
                }
                else
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    if (jsonString.Contains("password\":\""))
                    {
                        Console.WriteLine("- Dictionary attack detected - Password received - ");
                        string tmp = jsonString.Remove(0, 11);

                        try
                        {
                            string serverId = tmp.Substring(tmp.IndexOf(" ") + 1);
                            string password = tmp.Remove(tmp.IndexOf(" "), 9);
                            password = password.Remove(password.Length - 1);
                            User.SetPassword(password);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    else
                    {
                        List<string> dictionary = JsonConvert.DeserializeObject<List<string>>(jsonString);

                        int dictionaryCapacity = dictionary.Count();

                        string serverId = dictionary[dictionaryCapacity - 1];

                        dictionary.Remove(serverId);

                        Dictionary.setPackage(dictionary);

                        ThreadPool.QueueUserWorkItem(state => {

                            int threadTmp = 10;
                            bool test = false;
                            bool test1 = false;
                            bool test2 = false;
                            int wordsCounterTmp = 0;

                            while (true)
                            {
                                if (stopWatch.Elapsed.Seconds == 10 || stopWatch.Elapsed.Seconds == 20 || stopWatch.Elapsed.Seconds == 30)
                                {
                                    if (stopWatch.Elapsed.Seconds == 10 && test == false)
                                    {
                                        test = true;
                                        // Get the elapsed time as a TimeSpan value.
                                        TimeSpan ts1 = stopWatch.Elapsed;

                                        // Format and display the TimeSpan value.
                                        string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts1.Hours, ts1.Minutes, ts1.Seconds,
                                            ts1.Milliseconds / 10);

                                        response = response + Environment.NewLine + serverId + " - " + (Dictionary.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                        wordsCounterTmp = Dictionary.getWordsCounter() - wordsCounterTmp;
                                    }
                                    if (stopWatch.Elapsed.Seconds == 20 && test1 == false)
                                    {
                                        test1 = true;
                                        // Get the elapsed time as a TimeSpan value.
                                        TimeSpan ts1 = stopWatch.Elapsed;

                                        // Format and display the TimeSpan value.
                                        string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts1.Hours, ts1.Minutes, ts1.Seconds,
                                            ts1.Milliseconds / 10);

                                        response = response + Environment.NewLine + serverId + " - " + (Dictionary.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                        wordsCounterTmp = Dictionary.getWordsCounter() - wordsCounterTmp;
                                    }
                                    if (stopWatch.Elapsed.Seconds == 30 && test2 == false)
                                    {
                                        test2 = true;
                                        // Get the elapsed time as a TimeSpan value.
                                        TimeSpan ts1 = stopWatch.Elapsed;

                                        // Format and display the TimeSpan value.
                                        string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts1.Hours, ts1.Minutes, ts1.Seconds,
                                            ts1.Milliseconds / 10);

                                        response = response + Environment.NewLine + serverId + " - " + (Dictionary.getWordsCounter() - wordsCounterTmp) + "- words checked - Time: " + elapsedTime1 + "    ";
                                        wordsCounterTmp = Dictionary.getWordsCounter() - wordsCounterTmp;
                                    }
                                }
                            }
                        });

                        string crackResult = Dictionary.TryWord();

                        if (crackResult != null)
                        {
                            stopWatch.Stop();

                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts2 = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts2.Hours, ts2.Minutes, ts2.Seconds,
                                ts2.Milliseconds / 10);

                            Console.WriteLine(serverId + " - Password found: " + crackResult + " - Time: " + elapsedTime2);
                            return Response.AsJson(serverId + "- Dictionary attack detected - Password found: " + crackResult + " - Time: " + elapsedTime2);
                        }
                        else
                        {
                            stopWatch.Stop();

                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts2 = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts2.Hours, ts2.Minutes, ts2.Seconds,
                                ts2.Milliseconds / 10);

                            Console.WriteLine(serverId + " - Password not found: " + crackResult + " - Time: " + elapsedTime2);
                            return Response.AsJson(serverId + "- Dictionary attack detected - Password not found" + " - Time: " + elapsedTime2);
                        }
                    }

                    return Response.AsJson("Dictionary attack detected - Password received");
                }

            });

        }
    }
}
