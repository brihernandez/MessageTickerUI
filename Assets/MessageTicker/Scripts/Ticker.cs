using UnityEngine;
using System.Collections.Generic;

namespace Ticker
{
    public class Ticker : MonoBehaviour
    {
        [Header("Required Prefab")]
        [Tooltip("Must be assigned for Ticker to work.")]
        [SerializeField] private TickerItem tickerItemPrefab = null;

        [Header("Options")]
        [Tooltip("When number of items exceeds this number, the oldest items on the ticker will be deleted.")]
        [SerializeField] private int maxNumberOfItem = 10;

        [Tooltip("When true, messages will time out after a given lifespan. When false, messages will stay on the ticker until they are pushed off by new messages.")]
        [SerializeField] private bool allowMessageTimeout = true;

        private Queue<TickerItem> items = new Queue<TickerItem>();

        private static event System.Action<string> OnTickerShowMessageSimple;
        private static event System.Action<string, float, Color> OnTickerShowMessage;
        private static event System.Action OnTickerClear;

        // ================================
        // STATIC FUNCTIONS
        // ================================

        /// <summary>
        /// Adds a new message to the ticker with values for lifetime and color
        /// taken from the prefab.
        /// </summary>
        public static void AddItem(string text)
        {
            if (OnTickerShowMessageSimple != null)
                OnTickerShowMessageSimple.Invoke(text);
        }

        /// <summary>
        /// Adds a new message to the ticker with custom values for lifetime and color.
        /// </summary>
        /// <param name="lifespan">How long the message will be visible</param>
        /// <param name="color">Color of the text</param>
        public static void AddItem(string text, float lifespan, Color color)
        {
            if (OnTickerShowMessage != null)
                OnTickerShowMessage.Invoke(text, lifespan, color);
        }

        /// <summary>
        /// Clears the ticker of all messages.
        /// </summary>
        public static void Clear()
        {
            if (OnTickerClear != null)
                OnTickerClear.Invoke();
        }

        // ================================
        // INSTANCE FUNCTIONS
        // ================================

        private void Awake()
        {
            if (tickerItemPrefab == null)
                Debug.LogError("Ticker - No TickerItem prefab assigned!");
        }

        private void OnEnable()
        {
            OnTickerShowMessage += AddNewTicketItem;
            OnTickerShowMessageSimple += AddNewTicketItem;
            OnTickerClear += ClearMessages;
        }

        private void OnDisable()
        {
            OnTickerShowMessage -= AddNewTicketItem;
            OnTickerShowMessageSimple -= AddNewTicketItem;
            OnTickerClear -= ClearMessages;
        }

        private void AddNewTicketItem(string text)
        {
            if (tickerItemPrefab == null)
                return;

            var item = Instantiate(tickerItemPrefab, transform);
            item.SetText(text);

            if (!allowMessageTimeout)
                item.SetTimeToLive(float.MaxValue);

            AddItemToQueue(item);
        }

        private void AddNewTicketItem(string text, float lifespan, Color color)
        {
            if (tickerItemPrefab == null)
                return;

            var item = Instantiate(tickerItemPrefab, transform);
            item.SetTimeToLive(allowMessageTimeout ? lifespan : float.MaxValue);
            item.SetText(text);
            item.SetColor(color);

            AddItemToQueue(item);
        }

        private void AddItemToQueue(TickerItem item)
        {
            // Set up event so that if this item expires, Ticker knows to remove it
            // from the queue.
            item.OnItemExpired += DequeueTickerItem;

            // Maintain the max number of items that can be displayed.
            items.Enqueue(item);
            while (items.Count > maxNumberOfItem)
                items.Dequeue().DestroyTickerItem();
        }

        private void ClearMessages()
        {
            foreach (var item in items)
                item.DestroyTickerItem();

            items.Clear();
        }

        private void DequeueTickerItem()
        {
            items.Dequeue();
        }
    }
}