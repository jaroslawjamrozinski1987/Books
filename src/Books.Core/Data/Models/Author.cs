
namespace Books.Core.Data.Models;

internal record Author(string FirstName, string LastName)
{
    public Author(Author author)
    {
        this.FirstName = author.FirstName;
        this.LastName = author.LastName;
    }
}
