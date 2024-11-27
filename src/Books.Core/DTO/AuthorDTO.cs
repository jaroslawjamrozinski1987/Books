
namespace Books.Core.DTO;

internal record AuthorDTO(string FirstName, string LastName)
{
    private AuthorDTO() : this(default, default) { }
}
