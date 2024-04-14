/// <summary>
/// This interface allows for objects of different types to be pooled in a collision call.
/// The return value lets the method call know if an object it damaged is going to be destroyed.
/// </summary>
public interface ITakeDamage
{
    bool TakeDamage(int amount);
}
