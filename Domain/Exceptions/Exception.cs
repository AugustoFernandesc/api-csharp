namespace MinhaApi.Domain.Exceptions;

// Uma exceção simples que herda de Exception
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
}