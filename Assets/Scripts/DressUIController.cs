using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DressUIController : MonoBehaviour
{
    public static readonly UnityEvent OnTopButtonPressedEvent = new UnityEvent();
    public void OnTopButtonPressed()
    {
        OnTopButtonPressedEvent.Invoke();
        Debug.Log("Top Button Pressed");
    }
    
    
}
