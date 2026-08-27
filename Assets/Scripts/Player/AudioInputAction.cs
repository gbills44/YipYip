using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class AudioInputAction : MonoBehaviour
{
    public AudioSource src;
    public bool b_audioDetected;
    public AudioLoudnessDetect audioDetector;
    public UnityEngine.UI.Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = audioDetector.MicrophoneLoudness();
        Debug.Log(loudness);
        if(loudness > 0)
        {
            image.color = new Color(0,0,0,1);
        }
        else
        {
            image.color = new Color(255.0f, 0, 0, 1);
        }
    }
}