using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DK.UI
{
    public class VoiceToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        [Header("Visibility Settings")]
        [SerializeField] private GameObject objectToHide;

        [Header("Text Settings")]
        [SerializeField] private GameObject onTextBox;
        [SerializeField] private GameObject offTextBox;

        private void OnEnable()
        {
            UpdateUI(toggle.isOn);

            toggle.onValueChanged.AddListener(UpdateUI);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(UpdateUI);
        }

        private void UpdateUI(bool toggleValue)
        {
            PlayerPrefs.SetInt("Voice", toggleValue ? 1 : 0);
            PlayerPrefs.Save();

            if (toggleValue)
            {
                if (objectToHide != null) objectToHide.SetActive(false);

                if (onTextBox != null) onTextBox.SetActive(true);
                if (offTextBox != null) offTextBox.SetActive(false);
            }
            else
            {
                if (objectToHide != null) objectToHide.SetActive(true);

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
            return toggle.isOn;
        }
    }
}