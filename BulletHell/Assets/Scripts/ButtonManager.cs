using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bog
{
    public class ButtonManager : MonoBehaviour
    {
        [HideInInspector]
        public List<DungeonButton> dungeonButtons = new List<DungeonButton>();

        [SerializeField]
        private Sprite[] HowManyToGo = null;

        [HideInInspector]
        public Sprite current = null;

        [SerializeField]
        private Sprite end = null;

        private int index = 0;
        private DoorManager doorManager = null;

        public event Action<Sprite> OnChange = null;

        public event Action<Sprite> OnEnd = null;

        [SerializeField]
        private UnityEvent OnEnding = null;

        private void Awake()
        {
            doorManager = GetComponent<DoorManager>();
            current = HowManyToGo[index++];
        }

        public void RemoveOne(DungeonButton button)
        {
            dungeonButtons.Remove(button);
            current = HowManyToGo[index++];
            OnChange?.Invoke(current);
            if (dungeonButtons.Count == 0)
            {
                OnEnding?.Invoke();
                OnEnd?.Invoke(end);
                doorManager.Activate();
            }
        }
    }
}