using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DressUIController : MonoBehaviour
{
    public static readonly UnityEvent<ButtonTypes> OnButtonPressedEvent = new();
    public void OnTopButtonPressed()
    {
        OnButtonPressedEvent.Invoke(ButtonTypes.TopButton);
        Debug.Log("Top Button Pressed");
    }

    public void OnBodyButtonPressed()
    {
        OnButtonPressedEvent.Invoke(ButtonTypes.BodyButton);
        Debug.Log("Body Button Pressed");
    }

    public void OnGlovesButtonPressed()
    {
        OnButtonPressedEvent.Invoke(ButtonTypes.GlovesButton);
        Debug.Log("Gloves Button Pressed");
    }
    
    public void OnBottomButtonPressed()
    {
        OnButtonPressedEvent.Invoke(ButtonTypes.BottomButton);
        Debug.Log("Bottom Button Pressed");
    }
    
}
