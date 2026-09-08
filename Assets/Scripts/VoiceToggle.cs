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

        [Header("Text Settings")]
        [SerializeField] private GameObject onTextBox;  
        [SerializeField] private GameObject offTextBox; 

        private void OnEnable()
        {
            UpdateSprite(toggle.isOn);

            toggle.onValueChanged.AddListener(UpdateSprite);
            PlayerPrefs.SetInt("Voice", 1);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(UpdateSprite);
            PlayerPrefs.SetInt("Voice", 0);
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

                if (onTextBox != null) onTextBox.SetActive(true);
                if (offTextBox != null) offTextBox.SetActive(false);
            }
            else
            {
                targetImage.sprite = offSprite;

                if (objectToHide != null)
                {
                    objectToHide.SetActive(true);
                }

                if (onTextBox != null) onTextBox.SetActive(false);
                if (offTextBox != null) offTextBox.SetActive(true);
            }
        }

        public void ToggleValueThroughScript()
        {
            toggle.isOn = !toggle.isOn;
        }

        public bool Get_VoiceToggleStatus()
        {
            if(toggle.isOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}