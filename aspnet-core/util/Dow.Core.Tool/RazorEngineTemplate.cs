using RazorEngine;
using RazorEngine.Templating;
using System;

namespace Dow.Core.Tool
{
    public class RazorEngineTemplate
    {
        public static string  GetValue(string templateSource, string key, Type modelType = null, object model = null)
        {
            return Engine.Razor.RunCompile(templateSource, key, modelType, model);
        }
    }
}
