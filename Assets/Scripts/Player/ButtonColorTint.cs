using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonColorChange : MonoBehaviour, IPointerDownHandler
{
    private Image image;
    private Color originalColor;

    public Color cooldownColor = Color.gray;
    public float cooldownTime = 2f;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(CooldownVisual());
    }

    IEnumerator CooldownVisual()
    {
        image.color = cooldownColor;

        yield return new WaitForSeconds(cooldownTime);

        image.color = originalColor;
    }
}
