using System;

namespace PhilHarmoniq.Formulator.Example.Services
{
    public class GuidService : IGuidService
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
