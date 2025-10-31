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

    [Header("Snapping Settings")]
    [SerializeField] private RectTransform _snapTarget;   
    [SerializeField] private float _snapRange = 100f;

    void Start()
    {
        _image = GetComponent<Image>();
        _startPosition = transform.position;
        _originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.color = new Color32(255,255,255,150);
        transform.SetParent(_originalParent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.color = new Color32(255, 255, 255, 255);

        if (_snapTarget != null && Vector3.Distance(transform.position, _snapTarget.position) <= _snapRange)
        {
            SnapToTarget();
        }
        else
        {
            ResetPosition();
        }

        transform.SetParent(_originalParent);
    }

    private void SnapToTarget()
    {
        transform.position = _snapTarget.position;
    }

    private void ResetPosition()
    {
        transform.position = _startPosition;
    }
}
