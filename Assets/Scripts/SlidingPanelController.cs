using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SlidingPanelController : MonoBehaviour
{
    private Animator _slidePanelAnim;
    private readonly int _slidePanelOpenAnimHash = Animator.StringToHash("SlidingPanelOpenAnim");
    private readonly int _slidePanelCloseAnimHash = Animator.StringToHash("SlidingPanelCloseAnim");
    
    private ButtonTypes _currentButtonType;
    
    
    [SerializeField] private GameObject slidingPanelOpen;
    private void Awake()
    {
        DressUIController.OnButtonPressedEvent.AddListener(InvokeSlidePanelAnim);
        _slidePanelAnim = GetComponent<Animator>();
    }

    private void InvokeSlidePanelAnim(ButtonTypes buttonType)
    {
        Debug.Log("I'm here");
        if (slidingPanelOpen.activeSelf)
        {
            PlayAnimBackward();
        }
        else
        {
            PlayAnimForward();
            _currentButtonType = buttonType;
        }

        return;

        void PlayAnimForward()
        {
            _slidePanelAnim.Play(_slidePanelOpenAnimHash);
        }
        void PlayAnimBackward()
        {
            _slidePanelAnim.Play(_slidePanelCloseAnimHash);
        }
    }

    private void OnSlideOpenAnimEnd()
    {
        Debug.Log("Slide Open Anim End");
        slidingPanelOpen.SetActive(true);
        switch (_currentButtonType)
        {
            case ButtonTypes.TopButton:
                var topPanelChild = slidingPanelOpen.transform.Find("TopPanel").gameObject;
                topPanelChild.SetActive(true);
                foreach (var child in topPanelChild.GetComponentsInChildren<Transform>(true))
                {
                    Debug.Log(child.gameObject.name);
                    child.gameObject.SetActive(true);
                }
                break;
            case ButtonTypes.BodyButton:
                var bodyPanelChild = slidingPanelOpen.transform.Find("BodyPanel").gameObject;
                bodyPanelChild.SetActive(true);
                foreach (var child in bodyPanelChild.GetComponentsInChildren<Transform>(true))
                {
                    Debug.Log(child.gameObject.name);
                    child.gameObject.SetActive(true);
                }
                break;
            case ButtonTypes.GlovesButton:
                var glovesPanelChild = slidingPanelOpen.transform.Find("GlovesPanel").gameObject;
                glovesPanelChild.SetActive(true);
                foreach (var child in glovesPanelChild.GetComponentsInChildren<Transform>(true))
                {
                    Debug.Log(child.gameObject.name);
                    child.gameObject.SetActive(true);
                }
                break;
            case ButtonTypes.BottomButton:
                var bottomPanelChild = slidingPanelOpen.transform.Find("BottomPanel").gameObject;
                bottomPanelChild.SetActive(true);
                foreach (var child in bottomPanelChild.GetComponentsInChildren<Transform>(true))
                {
                    Debug.Log(child.gameObject.name);
                    child.gameObject.SetActive(true);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException("IDK What you did but congrats");
        }
    }

    private void OnSlideCloseAnimStart()
    {
        Debug.Log("Slide Close Anim End");
        foreach (var child in slidingPanelOpen.GetComponentsInChildren<Transform>())
        {
            child.gameObject.SetActive(false);
        }
        slidingPanelOpen.SetActive(false);
    }
}