using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FogBugz.Exporter.API;
using FogBugz.Exporter.API.APIModels;
using System.Linq;
using System.IO;

namespace FogBugz.Exporter
{
    class Program
    {
        public const int FetchCasesTryCount = 3;
        public const int CreateFilesTryCount = 3;

        public static string Domain { get; set; }

        public static string Token { get; set; }

        public static List<Case> Cases = new List<Case>();

        static void Main(string[] args)
        {
            var httpClient = new HttpClient();

            #region Login
            Console.WriteLine("Domain:");
            Domain = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("E-Mail:");
            var email = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Password:");

            var password = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(true);

                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            }
            while (key != ConsoleKey.Enter);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Logging in...");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{Domain}.manuscript.com/api/logon");

            var variables = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var contentString = JsonConvert.SerializeObject(variables);

            request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

            string responseContent = null;

            try
            {
                var httpResponse = httpClient.SendAsync(request);

                httpResponse.Wait();

                responseContent = httpResponse.Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                HandleException(new Exception($"Login failed: {ex.Message}."));
                return;
            }

            var response = JsonConvert.DeserializeObject<Response<LoginReponseData>>(responseContent);

            try
            {
                response.Check("Login failed:");
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }

            Token = response.Data.Token;
            #endregion

            #region FetchCases
            Console.WriteLine();
            Console.WriteLine("Fetching Cases...");

            var fromDate = new DateTime(2018, 2, 1);

            var timeSpan = DateTime.Today - fromDate;

            var months = Math.Abs((DateTime.Today.Month - fromDate.Month) + 12 * (DateTime.Today.Year - fromDate.Year));

            if (System.Diagnostics.Debugger.IsAttached) months = 1;

            var currentMonth = fromDate;

            double progress = 0;

            var progressPrefix = "Progress:";

            for (int i = 0; i < months; i++)
            {
                progress = (double)i / months * 100d;

                UpdateProgress($"{progressPrefix} {progress:n2} %");

                if (!TryAction(FetchCasesTryCount, () =>
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{Domain}.manuscript.com/api/search");

                    var variables = new Dictionary<string, object>
                    {
                        ["token"] = Token,
                        ["q"] = CreateQueryString(currentMonth),
                        ["cols"] = Helper.CaseListExportColumns
                    };

                    var contentString = JsonConvert.SerializeObject(variables);

                    request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

                    string responseContent = null;

                    try
                    {
                        var httpResponse = httpClient.SendAsync(request);

                        httpResponse.Wait();

                        responseContent = httpResponse.Result.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Fetching Cases failed: {ex.Message}.", ex);
                    }

                    var response = JsonConvert.DeserializeObject<Response<ListCasesResponseData>>(responseContent);

                    response.Check("Fetching Cases failed:");

                    Cases.AddRange(response.Data.Cases);
                }, 100))
                {
                    Logoff(httpClient);
                    return;
                }

                currentMonth = currentMonth.AddMonths(1);

                System.Threading.Thread.Sleep(100);
            }

            progress = 100;

            UpdateProgress($"{progressPrefix} {progress:n2} %");

            Console.WriteLine();
            #endregion

            #region CreateFiles
            Console.WriteLine();
            Console.WriteLine("Creating case files...");

            var directoryName = "Cases";

            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);

            for (int i = 0; i < Cases.Count; i++)
            {
                var apiCase = Cases[i];

                var exportCase = apiCase.ToExportCase();

                if (!TryAction(CreateFilesTryCount, () =>
                {
                    try
                    {
                        File.WriteAllText(Path.ChangeExtension(Path.Combine(directoryName, exportCase.Number.ToString()), "json"), JsonConvert.SerializeObject(exportCase, Formatting.Indented));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"File {exportCase.Number} could not be created: {ex.Message}", ex);
                    }
                }))
                {
                    Logoff(httpClient);
                    return;
                }
            }
            #endregion

            Logoff(httpClient);

            httpClient.Dispose();

            Console.WriteLine();
            Console.WriteLine("Export finished");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static bool TryAction(int tryCount, Action action, int delayMiliseconds = 0)
        {
            bool failed = true;

            var attempt = 0;

            while (failed && attempt < tryCount)
            {
                if (attempt > 0) Console.WriteLine("trying again...");

                try
                {
                    action.Invoke();

                    failed = false;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }

                if (delayMiliseconds > 0) System.Threading.Thread.Sleep(delayMiliseconds);

                attempt++;
            }

            return !failed;
        }

        private static string CreateQueryString(DateTime currentMonth)
        {
            if (System.Diagnostics.Debugger.IsAttached) return "case:10283";

            return $"opened:\"{currentMonth.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("en-US"))} {currentMonth.Year}\"";
        }

        private static void UpdateProgress(string progressDisplay)
        {
            Console.Write($"\r{progressDisplay}");
        }

        private static void HandleException(Exception ex)
        {
            Console.WriteLine(ex);
            Console.ReadLine();
        }

        private static void Logoff(HttpClient httpClient)
        {
            if (Token == null) return;

            Console.WriteLine();
            Console.WriteLine("Logging off...");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{Domain}.manuscript.com/api/logoff");

            var variables = new Dictionary<string, string>
            {
                ["token"] = Token
            };

            var contentString = JsonConvert.SerializeObject(variables);

            request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

            string responseContent;

            try
            {
                var httpResponse = httpClient.SendAsync(request);

                httpResponse.Wait();

                responseContent = httpResponse.Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                HandleException(new Exception($"Logoff failed: {ex.Message}.", ex));
                return;
            }

            var response = JsonConvert.DeserializeObject<EmptyResponse>(responseContent);

            response.Check("Logoff failed:");

            Token = null;
        }
    }
}