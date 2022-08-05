using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool isDrag = false;
    public Vector2 draggedPos = Vector2.zero;
    public Vector2 mousePos;

    private Obstacle _rayColliderObject;
    [SerializeField] private string _rayColliderTag;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            isDrag = true;

            if (hit.collider)
            {
                _rayColliderObject = hit.collider.gameObject?.GetComponent<Obstacle>();
                _rayColliderTag = _rayColliderObject?.GetComponent<ObstacleTag>().Tag;
                switch (_rayColliderTag)
                {
                    case "Crab":
                        _rayColliderObject.isPlay = false;
                        draggedPos = mousePos;
                        break;
                }
            }

        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            switch (_rayColliderTag)
            {
                case "Crab":
                    _rayColliderObject.GetComponent<Crab>().Dragging(draggedPos,mousePos);
                    break;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (_rayColliderTag)
            {
                case "Crab":
                    _rayColliderObject.isPlay = true;
                    break;
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