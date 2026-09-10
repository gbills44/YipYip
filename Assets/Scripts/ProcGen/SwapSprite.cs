using UnityEngine;
using UnityEngine.UI;

public class SwapSprite : MonoBehaviour
{
    [SerializeField] Sprite spriteOne;
    [SerializeField] Sprite spriteTwo;
    [SerializeField] Sprite spriteThree;

    private float numSpritesMin = 1.0f;
    private float numSpritesMax = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randSprite = Mathf.FloorToInt(Random.Range(numSpritesMin, numSpritesMax));

        switch (randSprite)
        {
            case 0:
                this.GetComponentInChildren<SpriteRenderer>().sprite = spriteOne;
                break;
            case 1:
                this.GetComponentInChildren<SpriteRenderer>().sprite = spriteTwo;
                break;
            case 2:
                this.GetComponentInChildren<SpriteRenderer>().sprite = spriteThree;
                break;
            default:
                this.GetComponentInChildren<SpriteRenderer>().sprite = spriteOne;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
