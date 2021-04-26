using UnityEngine;

namespace Bog.Assets.Scripts.Dungeon
{
    [CreateAssetMenu(fileName = "mew RoomScriptable", menuName = "RoomScriptable", order = 0)]
    public class RoomScriptable : ScriptableObject
    {
        public Texture2D[] room;
        public int count;
    }
}