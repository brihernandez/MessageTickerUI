using UnityEngine;
using UnityEngine.UI;

namespace Ticker
{
    public class TickerItem : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private Text tickerText = null;

        [Header("Options")]
        [Tooltip("How long the ticker message will stay visible.")]
        [SerializeField] private float timeToLive = 10f;

        [Tooltip("The time it takes for the message to fade out. For best results, this should be less than Time To Live value.")]
        [SerializeField] private float fadeTime = 1f;

        private Color fullColor = Color.white;

        public event System.Action OnItemExpired;

        private void Awake()
        {
            if (tickerText == null)
                Debug.LogError("TickerItem - Missing reference to text component!");
        }

        private void Update()
        {
            if (tickerText == null)
                return;

            timeToLive -= Time.unscaledDeltaTime;
            if (timeToLive <= fadeTime)
            {
                float fadeValue = (fadeTime - timeToLive) / fadeTime;
                fadeValue = Mathf.Clamp01(fadeValue);
                tickerText.color = Color.Lerp(fullColor,
                                              new Color(fullColor.r, fullColor.g, fullColor.b, 0f), 
                                              fadeValue);

                if (timeToLive <= 0f)
                {
                    if (OnItemExpired != null)
                        OnItemExpired.Invoke();

                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Sets the text of the ticker item.
        /// </summary>
        public void SetText(string text)
        {
            if (tickerText != null)
                tickerText.text = text;
        }

        /// <summary>
        /// Sets the color of the ticker item's text.
        /// </summary>
        public void SetColor(Color color)
        {
            if (tickerText != null)
                tickerText.color = color;

            fullColor = color;
        }

        /// <summary>
        /// Sets how long the ticker item has before it erases itself.
        /// </summary>
        public void SetTimeToLive(float time)
        {
            timeToLive = time;
        }

        /// <summary>
        /// Sets how long it takes for the ticker time to fade away. For best results,
        /// this should be a lower value than the time to live.
        /// </summary>
        public void SetFadeTime(float time)
        {
            fadeTime = time;
        }

        public void DestroyTickerItem()
        {
            Destroy(gameObject);
        }
    }
}
