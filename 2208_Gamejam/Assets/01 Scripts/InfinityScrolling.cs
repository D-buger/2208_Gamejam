using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfinityScrolling : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        float XOffset = horizontalSpeed * Timer.realTime;
        float YOffet = verticalSpeed * Timer.realTime;
        _image.material.mainTextureOffset = new Vector2(XOffset, YOffet);
    }
}
