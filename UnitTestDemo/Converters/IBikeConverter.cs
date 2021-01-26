using System.Collections.Generic;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;

namespace UnitTestDemo.Converters
{
    public interface IBikeConverter
    {
        BikeDto ToDto(Bike bike); 

        IEnumerable<BikeDto> ToDtoList(IReadOnlyList<Bike> models);
    }
}
