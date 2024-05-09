using FlowerShop_API.Models.Dto;

namespace FlowerShop_API.Data
{
    public class FlowerStore
    {
        public static List<FlowerDto> flowerList = new List<FlowerDto>
        {
            new FlowerDto{Id=1, Name="Rose"},
            new FlowerDto{Id=2, Name="Coin"}
        };
    }
}
