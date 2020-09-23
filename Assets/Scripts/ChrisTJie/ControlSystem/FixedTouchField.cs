using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Vector2 _TouchDist;
    [HideInInspector] public Vector2 _PointerOld;
    [HideInInspector] protected int _PointerId;
    private bool _Pressed;

    // Update is called once per frame
    void Update()
    {
        if (_Pressed)
        {
            if (_PointerId >= 0 && _PointerId < Input.touches.Length)
            {
                _TouchDist = Input.touches[_PointerId].position - _PointerOld;
                _PointerOld = Input.touches[_PointerId].position;
            }
            else
            {
                _TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _PointerOld;
                _PointerOld = Input.mousePosition;
            }
        }
        else _TouchDist = new Vector2();
    }

    private void OnMouseDown()
    {
        _Pressed = true;
    }

    private void OnMouseUp()
    {
        _Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _Pressed = true;
        _PointerId = eventData.pointerId;
        _PointerOld = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _Pressed = false;
    }
}
