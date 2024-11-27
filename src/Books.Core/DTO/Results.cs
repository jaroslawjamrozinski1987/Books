namespace Books.Core.DTO;

public record Results<T>(IList<T> Data, uint Total);
