using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bog
{
    public class RoomTriggerStart : MonoBehaviour
    {
        private bool first = true;

        public event Action OnStart;

        [SerializeField]
        private UnityEvent Start;

        public event Action OnEnd;

        private void Awake()
        {
            currentEnemies = new List<GameObject>();
        }

        public static List<GameObject> currentEnemies;

        public void FirstEnter()
        {
            if (first)
            {
                print(name + " " + "works");
                first = false;
                OnStart?.Invoke();
            }
        }

        public void End()
        {
            if (currentEnemies.Count == 0)
            {
                OnEnd?.Invoke();
            }
        }
    }
}