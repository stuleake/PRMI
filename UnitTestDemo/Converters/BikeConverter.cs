using System.Collections.Generic;
using System.Linq;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;

namespace UnitTestDemo.Converters
{
    public class BikeConverter : IBikeConverter
    {
        public BikeDto ToDto(Bike bike)
        {
            return bike == null ? new BikeDto() : new BikeDto { Colour = bike.Colour, Model = bike.Model, Name = bike.Name };
        }

        public IEnumerable<BikeDto> ToDtoList(IReadOnlyList<Bike> models)
        {
            if (models == null || models.Any() == false)
            {
                return Enumerable.Empty<BikeDto>();
            }

            return models.Select(ToDto);
        }
    }
}
