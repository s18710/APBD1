using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Give me an url");

            var url = Console.ReadLine();
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(url);


            string  result = string.Join(",",ExtractEmails(content));
            Console.WriteLine(result);
       
        }

        public static string[] ExtractEmails(String str)
        {
            string RegexPattern = @"\b[A-Z0-9._-]+@[A-Z0-9][A-Z0-9.-]{0,61}[A-Z0-9]\.[A-Z.]{2,6}\b";

            // Find matches
            System.Text.RegularExpressions.MatchCollection matches
                = System.Text.RegularExpressions.Regex.Matches(str, RegexPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            string[] MatchList = new string[matches.Count];

            // add each match
            int c = 0;
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                MatchList[c] = match.ToString();
                c++;
            }

            return MatchList;
        }
    }
}
