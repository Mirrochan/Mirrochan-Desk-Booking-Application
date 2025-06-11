namespace DeskBookingAPI.Exceptions
{
    public class EntityNotFoundException : KeyNotFoundException
    {
        public EntityNotFoundException(string entity, object key)
            : base($"Entity '{entity}' with key '{key}' was not found") { }
    }
}
