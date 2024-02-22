using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    public float DraggedTime { get; private set; } = 0f;
    public Vector2 DraggedPos { get; private set; } = Vector2.zero;
    public Vector2 OriMousePos { get; private set; }
    public Vector2 MousePos { get; private set; }

    private Obstacle _rayColliderObject;
    
    struct sObstacleData
    {
        public Obstacle ob;
        public Vector2 startPosition;
    }

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

    Dictionary<int, sObstacleData> touchedOb = new Dictionary<int, sObstacleData>();
    private void Update()
    {
        foreach(Touch t in Input.touches)
        {
            _rayColliderObject = null;
            if (t.phase == TouchPhase.Began)
            {
                MousePos = Camera.main.ScreenToWorldPoint(t.position);
                OriMousePos = MousePos;
                RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero);

                if (hit.collider)
                {
                    if (hit.collider.CompareTag(SystemManager.CASTLE_TAG))
                        SystemManager.Instance.UseBucket();
                    else
                    {
                        sObstacleData obData;
                        _rayColliderObject = hit.collider.gameObject?.GetComponent<Obstacle>();
                        obData.ob = _rayColliderObject;
                        obData.startPosition = OriMousePos;
                        touchedOb.Add(t.fingerId, obData);

                        _rayColliderObject?.MouseDown(MousePos);
                    }
                }
            }

            if (touchedOb.ContainsKey(t.fingerId))
            {
                _rayColliderObject = touchedOb[t.fingerId].ob;
            }

            if (t.phase == TouchPhase.Moved)
            {
                MousePos = Camera.main.ScreenToWorldPoint(t.position);

                DraggedPos = (MousePos - touchedOb[t.fingerId].startPosition).normalized;

                SetCollObj();

                _rayColliderObject?.OnDrag(MousePos);
            }

            if (t.phase == TouchPhase.Ended)
            {
                MousePos = Camera.main.ScreenToWorldPoint(t.position);
                SetCollObj();

                _rayColliderObject?.MouseUp(MousePos);
                GameManager.Instance.ChangeCursorToDefense(false);

                _rayColliderObject = null;
                if (touchedOb.ContainsKey(t.fingerId))
                {
                    touchedOb.Remove(t.fingerId);
                }
            }
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