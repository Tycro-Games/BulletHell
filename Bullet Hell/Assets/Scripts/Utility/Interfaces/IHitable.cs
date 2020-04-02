
using System.Collections;

public interface IHitable
{
    IEnumerator TakeDamage (int damage);
    
}
public interface IKillable
{
    void Die ();
}