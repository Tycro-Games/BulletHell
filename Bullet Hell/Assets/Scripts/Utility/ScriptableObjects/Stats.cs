using UnityEngine;
[CreateAssetMenu (fileName = "new stats", menuName = "Create staff/new stats")]
public class Stats : ScriptableObject
{
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 25;
}
