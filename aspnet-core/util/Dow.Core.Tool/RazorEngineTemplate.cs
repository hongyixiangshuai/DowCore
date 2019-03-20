using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
namespace Dow.Core.Tool
{
    public class RazorEngineTemplate
    {
        public  string  GetValue(string templateSource, string key, Type modelType = null, object model = null)
        {
            return Engine.Razor.RunCompile(templateSource, key, modelType, model);
        }

        public string GetValue(string filePath, Type modelType = null, object model = null)
        {
            var key = Path.GetFileNameWithoutExtension(filePath);

            var templateSource = File.ReadAllText(filePath);
            
            return Engine.Razor.RunCompile(templateSource, key, modelType, model);
        }

       // public static 
    }
}
