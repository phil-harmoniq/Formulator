using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Formulator
{
    public class ResourceJsonProvider : ConfigurationProvider
    {
        private readonly string _path;
        private readonly bool _optional;

        public ResourceJsonProvider(string fileName, bool optional)
        {
            _path = string.Join(".", fileName.Split('/'));
            _optional = optional;
        }

        public override void Load()
        {
            var assembly = Container.StartupAssembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{_path}");

            if (stream == null && !_optional)
            {
                throw new Exception($"Required file not found: {_path}");
            }
            else if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    Data = Flatten(reader.ReadToEnd());
                }
            }
        }

        private Dictionary<string, string> Flatten(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);
            var jTokens = jsonObject.Descendants().Where(p => p.Count() == 0);
            var results = jTokens.Aggregate(new Dictionary<string, string>(), (properties, jToken) =>
            {
                var parsedToken = jToken.Path.Replace('.', ':');
                properties.Add(parsedToken, jToken.ToString());
                return properties;
            });
            return results;
        }
    }
}
