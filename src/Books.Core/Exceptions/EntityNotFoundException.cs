namespace Books.Core.Exceptions;

internal abstract class EntityNotFoundException<T> : CustomException
{
    public T Id { get;  }
    public EntityNotFoundException(T id, string message) : base(message)
    {
        Id = id;
    }
}
