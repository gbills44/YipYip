using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace DK.UI
{
    public class VoiceToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        [Header("Sprite Settings")]
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;

        [Header("Visibility Settings")]
        [SerializeField] private GameObject objectToHide;

        private void OnEnable()
        {
            UpdateSprite(toggle.isOn);

            toggle.onValueChanged.AddListener(UpdateSprite);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(UpdateSprite);
        }

        private void UpdateSprite(bool toggleValue)
        {
            if (toggleValue)
            {
                targetImage.sprite = onSprite;

                if (objectToHide != null)
                {
                    objectToHide.SetActive(false);
                }
            }
            else
            {
                targetImage.sprite = offSprite;

                if (objectToHide != null)
                {
                    objectToHide.SetActive(true);
                }
            }
        }

        public void ToggleValueThroughScript()
        {
            toggle.isOn = !toggle.isOn;
        }
    }
}