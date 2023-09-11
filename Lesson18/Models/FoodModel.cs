using Lesson18.Data.Models;

namespace Lesson18.Models;

public class FoodModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Food;
}
