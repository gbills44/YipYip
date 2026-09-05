using UnityEngine;

public class OverlayControls : MonoBehaviour
{
    [SerializeField] private GameObject voiceControlsOverlay;
    [SerializeField] private GameObject btnControlsOverlay;
    [SerializeField] private PlayerController playerController;

    private GameObject selectedOverlay;
    private UnityEngine.Vector3 overlayPos = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerController.get_VoiceToggle())
        {
            selectedOverlay = voiceControlsOverlay;
        }
        else
        {
            selectedOverlay = btnControlsOverlay;
        }

        CreateOverlay(selectedOverlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateOverlay(GameObject p_overlay)
    {
        Instantiate(p_overlay, overlayPos, Quaternion.identity);
    }
}
