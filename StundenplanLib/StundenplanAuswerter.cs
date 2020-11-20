using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Stundenplan
{
    public class StundenplanAuswerter
    {
        private static int start = 0;
        private static string path; // C:\Users\Luis Laptop\source\repos\HelloWorld\HelloWorld\stundenplan.xml
        public static void StundenPlan()
        {
            //Settings();
            path = @"C:\Users\Luis Laptop\source\repos\HelloWorld\HelloWorld\stundenplan.xml";
            string exit = "";
            while (exit != "exit")
            {
                StundenplanAuswerter.AuswaertenByDay();
                exit = Console.ReadLine();
            }
        }
    private static void AuswaertenByDay()
        {
            DayOfWeek doW = DateTime.Now.DayOfWeek;
            XDocument xml = new XDocument();
            xml = XDocument.Load(path);
            List<XElement> el = new List<XElement>();

            foreach (XElement item in xml.Root.Elements(doW.ToString()))
            {
                foreach (XElement fach in item.Nodes())
                {
                    //Console.WriteLine(fach.Attribute("Name").Value + " Start:" + DateTime.Parse(fach.Attribute("Start").Value).Hour);
                    TimeSpan start = TimeSpan.Parse(fach.Attribute("Start").Value);
                    TimeSpan end = TimeSpan.Parse(fach.Attribute("Ende").Value);
                    TimeSpan current = TimeSpan.Parse("10:45");//TimeSpan.Parse(string.Concat(DateTime.Now.Hour, ":", DateTime.Now.Minute));

                    if (0 > current.CompareTo(end) && 0 <= current.CompareTo(start))
                    {
                        string url;
                        XAttribute cuEl = fach.Attribute("Link");
                        if(cuEl != null)
                        {
                             
                             url = cuEl.Value;
                             OeffneURL(url);
                        }
                        else
                        {
                            Console.WriteLine("Für dieses Fach gibt es keinen Link.\n Soll LEA geöffnet werden?[j/n]");
                            string antwort = Console.ReadLine();
                            if(antwort == "j")
                            {
                                OeffneURL(@"https://lea.hochschule-bonn-rhein-sieg.de/");
                            }
                          
                        }
                        
                        
                        return;
                    }

                    
                }
               
            }
            Console.WriteLine("Im Moment keine Vorlesung(Programm mit exit beenden)");
        }

        private static void OeffneURL(string url)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", url);
        }
        private static void Settings()
        {
            if (start == 0)
            {
                Console.WriteLine("Bitte konfiguriere deinen Stundenplan \n Speicherort (absoluter Pfad):");
                path = @Console.ReadLine();
                
            }
        }
    }
}
