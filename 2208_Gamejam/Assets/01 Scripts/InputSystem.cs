using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hit.collider.GetComponent<ObstacleTag>().Tag

public class InputSystem : MonoBehaviour
{
    public bool _isDrag = false;
    public Vector2 _draggedPos = Vector2.zero; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);



        }

        if (Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonUp(0))
        {

        }
    }

}

/*
 * 
 * 
 * 
 * 
 */