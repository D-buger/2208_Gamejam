using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool IsDrag { get; private set; } = false;
    public float DraggedTime { get; private set; } = 0f;
    public Vector2 DraggedPos { get; private set; } = Vector2.zero;
    public Vector2 OriMousePos { get; private set; }
    public Vector2 MousePos { get; private set; }

    private Obstacle _rayColliderObject;
    private string _rayColliderTag;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OriMousePos = MousePos;
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero);
            
            IsDrag = true;
            DraggedTime = 0;

            if (hit.collider)
            {
                if (hit.collider.CompareTag(SystemManager.CASTLE_TAG))
                    SystemManager.Instance.UseBucket();
                else
                {
                    _rayColliderObject = hit.collider.gameObject?.GetComponent<Obstacle>();
                    _rayColliderTag = _rayColliderObject?.GetComponent<ObstacleTag>().Tag;

                    _rayColliderObject.MouseDown(MousePos);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            DraggedTime += Time.deltaTime;

            _rayColliderObject?.OnDrag(MousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IsDrag = false;
            if(!_rayColliderObject)
            {
                RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero);

                if (hit.collider)
                {
                    _rayColliderObject = hit.collider.gameObject?.GetComponent<Obstacle>();
                    _rayColliderTag = _rayColliderObject?.GetComponent<ObstacleTag>().Tag;

                }
            }

            _rayColliderObject?.MouseUp(MousePos);

            _rayColliderObject = null;
            _rayColliderTag = null;
            DraggedTime = 0f;
        }
    }

}

/*
 * 
 * click : seagull, wave
 * drag : crab
 * swipe : beachball
 * 
 */