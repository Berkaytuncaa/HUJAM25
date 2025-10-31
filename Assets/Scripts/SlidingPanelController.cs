using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SlidingPanelController : MonoBehaviour
{
    private Animator _slidePanelAnim;
    private readonly int _slidePanelOpenAnimHash = Animator.StringToHash("SlidingPanelOpenAnim");
    private readonly int _slidePanelCloseAnimHash = Animator.StringToHash("SlidingPanelCloseAnim");
    
    
    [SerializeField] private GameObject slidingPanelOpen;
    private void Awake()
    {
        DressUIController.OnTopButtonPressedEvent.AddListener(InvokeSlidePanelAnim);
        _slidePanelAnim = GetComponent<Animator>();
    }

    private void InvokeSlidePanelAnim()
    {
        if (slidingPanelOpen.activeSelf)
        {
            PlayAnimBackward();
        }
        else
        {
            PlayAnimForward();
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

    private void OnSlideAnimEnd()
    {
        Debug.Log("Slide Anim End");
        slidingPanelOpen.SetActive(true);
    }
}