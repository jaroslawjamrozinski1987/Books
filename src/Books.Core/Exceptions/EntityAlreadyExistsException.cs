namespace Books.Core.Exceptions;

internal abstract class EntityAlreadyExistsException<T> : CustomException
{
    public T Id { get; }
    public EntityAlreadyExistsException(T id, string message) : base(message)
    {
        Id = id;
    }
}
