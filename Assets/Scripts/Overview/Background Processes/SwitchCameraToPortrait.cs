using UnityEngine;

public class SwitchCameraToPortrait : MonoBehaviour
{
    [SerializeField] GameObject portraitMode;
    [SerializeField] GameObject landScapeMode;
    private Vector3 cameraOffset = new Vector3(0, 0, -10);

    public bool IsPortrait;

    // Update is called once per frame
    void Update()
    {
        switch (Input.deviceOrientation) {
            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                transform.position = portraitMode.transform.position + cameraOffset;
                break;
            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                transform.position = landScapeMode.transform.position + cameraOffset;
                break;
            default:
                break;
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (IsPortrait)
        //     {
        //         transform.position = portraitMode.transform.position + cameraOffset;
        //     }
        //     else
        //     {
        //         transform.position = landScapeMode.transform.position + cameraOffset;
        //     }
        //     IsPortrait = !IsPortrait;
        // }
    }
}
