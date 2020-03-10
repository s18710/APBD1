using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw02
{
    class Program
    {

        public static void ErrorLogging(Exception e)
        {
             string logpath = @"C:\\Users\s18710\Desktop\APBD1\cw02\logs.txt";
                if (File.Exists(logpath))
                {
                    File.Create(logpath).Dispose();
                }
                StreamWriter sw = File.AppendText(logpath);
                sw.WriteLine("Logging error");
                sw.WriteLine("Start " + DateTime.Now);
                sw.WriteLine("Error " + e.Message);
                sw.WriteLine("End " + DateTime.Now);
                sw.Close();
           
        }

        static void Main(string[] args)
        {
            try
            {
               
                    string csvPath = Console.ReadLine(); //"C:\Users\s18710\Desktop\APBD1\cw02\dane.csv"
                    string xmlPath = Console.ReadLine(); //"C:\Users\s18710\Desktop\APBD1\"
                    string format = Console.ReadLine(); //xml

                if (File.Exists(csvPath) && Directory.Exists(xmlPath))
                {
                    string[] source = File.ReadAllLines(csvPath);
                    XElement xml = new XElement("Uczelnia",
                        new XElement("studenci",
                        from str in source
                        let fields = str.Split(',')
                        select new XElement("student",
                                    new XAttribute("indexNumber", "s" + fields[4]),
                                    new XElement("fname", fields[0]),
                                    new XElement("lname", fields[1]),
                                    new XElement("birthday", fields[5]),
                                    new XElement("email", fields[6]),
                                    new XElement("mothersName", fields[7]),
                                    new XElement("fathersName", fields[8]),
                                    new XElement("studies",
                                        new XElement("name", fields[2]),
                                        new XElement("mode", fields[3])

                                        )
                                 )
                        )
                    );

                    xml.Save(String.Concat(xmlPath, "result.xml"));

                }
                else
                {
                    if (!File.Exists(csvPath))
                    {
                        throw new Exception("File does not exist");
                    }
                    if (!File.Exists(xmlPath))
                    {
                        throw new Exception("File does not exist");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogging(e);
            }
        }
    }
}
