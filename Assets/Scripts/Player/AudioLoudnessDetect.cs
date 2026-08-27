using UnityEngine;

public class AudioLoudnessDetect : MonoBehaviour
{

    public int sampleWindow = 64;
    private AudioClip micClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MicToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if(startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // calc loudness
        float totalLoudness = 0;
        float meanLoudness = 0;
        for(int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        // Need extreme(s) check
        meanLoudness = totalLoudness / sampleWindow;
        return meanLoudness;
    }

    public void MicToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        Debug.Log(microphoneName);
        micClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float MicrophoneLoudness()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), micClip);
    }
}
