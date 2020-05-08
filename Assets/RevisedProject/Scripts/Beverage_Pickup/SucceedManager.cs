using UnityEngine;

public class SucceedManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ArmController arm = other.gameObject.GetComponent<ArmController>();
        if (arm != null && arm.hasCup)
        {
            SuccessEvent();
        }
        if (arm != null && !arm.hasCup)
        {
            FailEvent();
        }
        Destroy(other.gameObject);
    }

    public delegate void DrinkGrabbed();
    public static event DrinkGrabbed SuccessEvent;
    public static event DrinkGrabbed FailEvent;
}
