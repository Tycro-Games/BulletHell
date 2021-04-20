using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class ButtonManager : MonoBehaviour
    {
        [HideInInspector]
        public List<DungeonButton> dungeonButtons = new List<DungeonButton>();

        private DoorManager doorManager = null;

        private void Start()
        {
            doorManager = GetComponent<DoorManager>();
        }

        public void RemoveOne(DungeonButton button)
        {
            dungeonButtons.Remove(button);
            if (dungeonButtons.Count == 0)
            {
                doorManager.Activate();
            }
        }
    }
}