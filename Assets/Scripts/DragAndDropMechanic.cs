using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropMechanic : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _image;
    private Vector3 _startPosition;
    private Transform _originalParent;
    private int _originalSiblingIndex;

    [Header("Snapping Settings")]
    [SerializeField] private RectTransform _snapTarget;   
    [SerializeField] private float _snapRange = 100f;
    
    [SerializeField] private RectTransform otherTorso;
    [SerializeField] private bool isTorso;

    void Start()
    {
        _image = GetComponent<Image>();
        _startPosition = transform.position;
        _originalParent = transform.parent;
        _originalSiblingIndex = transform.GetSiblingIndex();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.color = new Color32(255,255,255,150);
        transform.SetParent(_originalParent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.color = new Color32(255, 255, 255, 255);

        if (_snapTarget != null && RectTransformUtility.RectangleContainsScreenPoint(_snapTarget, transform.position)) //Vector3.Distance(transform.position, _snapTarget.position) <= _snapRange)
        {
            Debug.Log("Should Snap");
            SnapToTarget();
        }
        else
        {
            Debug.Log("Should Not Snap");
            ResetPosition();
        }

        //transform.SetParent(_originalParent);
    }

    private void SnapToTarget()
    {
        if (isTorso)
        {
            otherTorso.GetComponent<DragAndDropMechanic>().ResetPosition();
        }
        transform.position = _snapTarget.position;
        _snapTarget.gameObject.SetActive(true);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0); // Make the dragged item invisible after snapping
    }

    private void ResetPosition()
    {
        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(_originalSiblingIndex);
        transform.position = _startPosition;
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255); 
        _snapTarget.gameObject.SetActive(false);
    }
}
