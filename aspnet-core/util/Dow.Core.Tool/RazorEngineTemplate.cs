using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
namespace Dow.Core.Tool
{
    public class RazorEngineTemplate
    {
        public static string  GetValue(string templateSource, string key, Type modelType = null, object model = null)
        {
            try
            {
                return Engine.Razor.RunCompile(templateSource, key, modelType, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
