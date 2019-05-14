using UnityEngine;
using System.Collections.Generic;

namespace Ticker.Demo
{
    public class TickerItemSpawner : MonoBehaviour
    {
        [Tooltip("List of messages that will be picked from randomly when the \'S\' key is pressed.")]
        [SerializeField] private List<string> items = new List<string>();

        [Tooltip("The color of the even numbered message texts that will be added to the ticker.")]
        public Color evenColor = Color.white;

        [Tooltip("The color of the odd numbered message texts that will be added to the ticker.")]
        public Color oddColor = Color.red;

        [Tooltip("How long a spawned message will stay on the ticker.")]
        public float lifespan = 10f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S) && items.Count > 0)
            {
                int index = Random.Range(0, items.Count - 1);
                string randomMessage = items[index];
                Color color = index % 2 == 0 ? evenColor : oddColor;
                Ticker.AddItem(randomMessage, lifespan, color);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Ticker.Clear();
            }
        }
    }
}

