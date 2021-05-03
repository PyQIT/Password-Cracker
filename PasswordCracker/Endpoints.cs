using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                if (jsonString.Contains("bruteforcePattern"))
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    string tmp = jsonString.Substring(jsonString.IndexOf("password\":") + 11);
                    User.SetPassword(tmp.Substring(0, tmp.IndexOf("\"}")));

                    string tmpPattern = jsonString.Substring(jsonString.IndexOf("bruteforcePattern\":") + 20);

                    Bruteforce.setRange(tmpPattern.Substring(0, tmpPattern.IndexOf("\",\"password\"")));

                    string password = Bruteforce.CrackPassword();

                    stopWatch.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    if(password != null)
                        return Response.AsJson("Bruteforce attack detected - Password: " + password + " - Time: " + elapsedTime);

                    return Response.AsJson("Bruteforce attack detected - Password not found - Time: " + elapsedTime);
                }
                else
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    if (jsonString.Contains("password\":\""))
                    {
                        User.SetPassword(jsonString.Remove(0,11));
                    }
                    else
                    {
                        List<string> dictionary = JsonConvert.DeserializeObject<List<string>>(jsonString);
                        Dictionary.setPackage(dictionary);

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

                            return Response.AsJson("Dictionary attack detected - Password found: " + crackResult + " - Time: " + elapsedTime2);
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

                            return Response.AsJson("Dictionary attack detected - Password not found" + " - Time: " + elapsedTime2);
                        }
                    }

                    return Response.AsJson("Dictionary attack detected - Password received");
                }

            });

        }
    }
}
