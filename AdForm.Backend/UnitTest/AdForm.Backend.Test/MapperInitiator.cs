using AdFormAssignment.Business;
using AutoMapper;
using NUnit.Framework;

namespace AdForm.Backend.Test
{
    /// <summary>
    /// Automapper initiator.
    /// </summary>
    public class MapperInitiator
    {
        protected MapperInitiator()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            Mapper = mappingConfig.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}