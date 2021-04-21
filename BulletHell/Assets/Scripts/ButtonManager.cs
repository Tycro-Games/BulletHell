using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class ButtonManager : MonoBehaviour
    {
        [HideInInspector]
        public List<DungeonButton> dungeonButtons = new List<DungeonButton>();

        [SerializeField]
        private Sprite[] HowManyToGo = null;

        public Sprite current = null;

        private int index = 0;
        private DoorManager doorManager = null;

        public event Action<Sprite> OnChange = null;

        private void Start()
        {
            doorManager = GetComponent<DoorManager>();
        }

        public void RemoveOne(DungeonButton button)
        {
            dungeonButtons.Remove(button);
            current = HowManyToGo[index++];
            OnChange(current);
            if (dungeonButtons.Count == 0)
            {
                doorManager.Activate();
            }
        }
    }
}