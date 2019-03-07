using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;

namespace Dow.Core.Tool
{
    public class RazorEngineTemplate
    {
        public static string  GetValue(string templateSource, string key, Type modelType = null, object model = null)
        {
            return Engine.Razor.RunCompile(templateSource, key, modelType, model);
        }

        public static string GetValue(string filePath, Type modelType = null, object model = null)
        {
            var templateSource = File.ReadAllText(filePath);
            var key = Path.GetFileNameWithoutExtension(filePath);
            return Engine.Razor.RunCompile(templateSource, key, modelType, model);
        }

       // public static 
    }
}
