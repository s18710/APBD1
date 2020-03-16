using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw02
{
    class Program
    {

        public static void ErrorLogging(Exception e)
        {
            string logpath = @"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW2\logs.txt";
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

        public static void ErrorLoggingData(string s)
        {
            string logpath = @"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW2\logs.txt";
            if (File.Exists(logpath))
            {
                File.Create(logpath).Dispose();
            }
            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine("Logging error");
            sw.WriteLine("Start " + DateTime.Now);
            sw.WriteLine("Error " + s);
            sw.WriteLine("End " + DateTime.Now);
            sw.Close();

        }

        static void Main(string[] args)
        {
            try
            {

                string csvPath = @"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW2\dane.csv";
                //Console.ReadLine(); //"C:\Users\s18710\Desktop\APBD1\cw02\dane.csv"
                //"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW2\dane.csv"
                string xmlPath = @"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW2\";
                    //Console.ReadLine(); //"C:\Users\s18710\Desktop\APBD1\"
                string format = Console.ReadLine(); //xml

                if (File.Exists(csvPath) && Directory.Exists(xmlPath))
                {
                    string[] source = File.ReadAllLines(csvPath);
                    ArrayList sourceCp = new ArrayList();
                   
                    
                    Dictionary<String, int> activeStudies = new Dictionary<String, int>();
                    for (int i = 0; i < source.Length; i++)
                    {

                        string[] fields = source[i].Split(',');

                        if (fields.Length != 9)
                        {
                            ErrorLoggingData(" Error row does not contain correct number of columns");
                        }
                        else
                        {
                            Boolean notEmpty = true;
                            for (int j = 0; j < fields.Length; j++)
                            {
                                if (fields[j].Equals("") || fields[j].Equals(""))
                                    notEmpty = false;
                            }
                            if (notEmpty)
                            {
                                sourceCp.Add(source[i]);
                                
                                if (!activeStudies.ContainsKey(fields[2]))
                                {
                                    activeStudies.Add(fields[2], 1);
                                }
                                else
                                {
                                    activeStudies[fields[2]]++;
                                }
                            }
                            else
                            {
                                ErrorLoggingData("Error some columns are empty");
                            }
                        }
                    
                    }

                    
                    source = (String[])sourceCp.ToArray(typeof(string));

                    XElement xml = new XElement("Uczelnia",
                        new XAttribute("createdAt",DateTime.Today),
                        new XAttribute("author", "Alicja Wiercinska"),
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
                        ),
                        new XElement("activeStudies",
                            from pairs in activeStudies
                            select new XElement("studies",
                            new XAttribute("name", pairs.Key),
                            new XAttribute("numberOfStudents", pairs.Value)
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
