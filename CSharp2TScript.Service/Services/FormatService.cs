using CSharp2TScript.Service.Helpers;
using CSharp2TScript.Service.Interface;

namespace CSharp2TScript.Service.Services
{
    public class FormatService : IFormatService
    {
        private static string[] numberFormats = { "long", "int" }; //define csharp number formats
        public string CSharp2TScript(string cSharp)
        {
            try
            {
               
                string tScript = string.Empty;

                //iterate through each property/name
                int classCounter = 0;
                foreach (var item in SanitizedAndSplitPerProperty(cSharp))
                {
                    string dataType = item[0];
                    string propertyName = item[1];

                    if (string.IsNullOrEmpty(dataType) || string.IsNullOrEmpty(propertyName))
                        continue;

                    if (dataType.Contains("class"))
                    {
                        if (classCounter > 0)
                            tScript += "}\r\n \r\n";

                        tScript += "export interface " + propertyName + " {";
                        classCounter++;
                    }
                    else
                    {
                        if (dataType.Contains("List"))
                        {
                            dataType = dataType.Replace("<", string.Empty)
                                            .Replace(">", string.Empty)
                                            .Replace("List", string.Empty) + "[]";
                        }

                        if (dataType.Contains("?"))
                        {
                            dataType = dataType.Replace("?", string.Empty);
                            propertyName += "?";
                        }

                        foreach (var numFormats in numberFormats)
                        {
                            dataType = dataType.Replace(numFormats, "number");
                        }

                        tScript += $"    { propertyName.ToCamelCase()}: {dataType};";
                    }
                    tScript += "\r\n";
                }

                tScript += "}";
                return tScript;

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid CSharp Class Format");
                return string.Empty;
            }
        }

        public static IEnumerable<string[]> SanitizedAndSplitPerProperty(string str) 
        {
            var sanitizedStr =
                   str.Replace("{", string.Empty)
                   .Replace("}", string.Empty)
                   .Replace("get;", string.Empty)
                   .Replace("set;", string.Empty)
                   .Replace("\r\n", string.Empty)
                   .Split("public")
                   .Select(s => s.Trim())
                   .Where(x => !string.IsNullOrEmpty(x))
                   .Select(s => s.Split(" "));

            return sanitizedStr;
        }
    }
}
