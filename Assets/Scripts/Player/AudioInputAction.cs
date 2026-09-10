//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class AudioInputAction : MonoBehaviour
{
    //public AudioSource src;

    [SerializeField] GameObject player;

    public bool b_audioDetected;
    public AudioLoudnessDetect audioDetector;
    public float loudnessSense = 20.0f;
    public float threshold = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = audioDetector.MicrophoneLoudness() * loudnessSense;
        
        if(loudness > threshold)
        {
            Debug.Log("AIA Class ifelse loudness: " + loudness);

            player.GetComponent<PlayerController>().YipBoostVoice();

        }
        else
        {
            loudness = 0;
        }
    }
}