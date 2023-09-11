using Lesson18.Data.Models;

namespace Lesson18.Models;

public class AccessoriesModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Accessories;
}
