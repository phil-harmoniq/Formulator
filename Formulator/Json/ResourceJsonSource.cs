using Microsoft.Extensions.Configuration;

namespace Formulator
{
    public class ResourceJsonSource : IConfigurationSource
    {
        private readonly string _fileName;
        private readonly bool _optional;

        public ResourceJsonSource(string fileName, bool optional)
        {
            _fileName = fileName;
            _optional = optional;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResourceJsonProvider(_fileName, _optional);
        }
    }
}
