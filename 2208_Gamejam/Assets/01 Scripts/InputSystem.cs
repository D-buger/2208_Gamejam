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

#if UNITY_STANDALONE_WIN
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
            DraggedPos = (MousePos - OriMousePos).normalized;

            SetCollObj();

            _rayColliderObject?.OnDrag(MousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IsDrag = false;
            SetCollObj();

            _rayColliderObject?.MouseUp(MousePos);
            GameManager.Instance.ChangeCursorToDefense(false);

            _rayColliderObject = null;
            _rayColliderTag = null;
            DraggedTime = 0f;
        }
    }
#elif UNITY_ANDROID

    Touch touch;
    private void Update()
    {
        if (Input.touchCount > 0)
            touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            MousePos = Camera.main.ScreenToWorldPoint(touch.position);
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

                    _rayColliderObject?.MouseDown(MousePos);
                }
            }
        }

        if(touch.phase == TouchPhase.Moved)
        {
            MousePos = Camera.main.ScreenToWorldPoint(touch.position);

            DraggedTime += Time.deltaTime;
            DraggedPos = (MousePos - OriMousePos).normalized;

            SetCollObj();

            _rayColliderObject?.OnDrag(MousePos);
        }

        if(touch.phase == TouchPhase.Ended)
        {
            MousePos = Camera.main.ScreenToWorldPoint(touch.position);
            IsDrag = false;
            SetCollObj();

            _rayColliderObject?.MouseUp(MousePos);
            GameManager.Instance.ChangeCursorToDefense(false);

            _rayColliderObject = null;
            _rayColliderTag = null;
            DraggedTime = 0f;
        }
    }
#endif

    private void SetCollObj()
    {
        if (!_rayColliderObject)
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero);

            if (hit.collider)
            {
                _rayColliderObject = hit.collider.gameObject?.GetComponent<Obstacle>();
                _rayColliderTag = _rayColliderObject?.GetComponent<ObstacleTag>().Tag;

            }
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