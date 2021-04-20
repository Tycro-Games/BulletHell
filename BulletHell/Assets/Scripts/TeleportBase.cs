using UnityEngine;

namespace Bog
{
    public class TeleportBase : MonoBehaviour
    {
        protected bool canTele = true;
        protected RoomTriggerStart roomTrigger = null;

        public void CheckEnemies()
        {
            if (RoomTriggerStart.currentEnemies.Count != 0)
                canTele = false;
        }

        public virtual void Activate()
        {
            canTele = true;
        }

        protected void Init()
        {
            roomTrigger = GetComponentInParent<RoomTriggerStart>();
            roomTrigger.OnStart += CheckEnemies;
            roomTrigger.OnEnd += Activate;
        }

        private void OnDisable()
        {
            roomTrigger.OnEnd -= Activate;
            roomTrigger.OnStart -= CheckEnemies;
        }
    }
}