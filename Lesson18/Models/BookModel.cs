using Lesson18.Data.Models;

namespace Lesson18.Models;

public class BookModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Book;
}
