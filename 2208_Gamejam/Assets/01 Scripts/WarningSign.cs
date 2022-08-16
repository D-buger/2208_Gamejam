using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarningSign : MonoBehaviour
{
    public float remainTime;
    public event UnityAction beforeSignEnable;
    public event UnityAction afterSignDisable;

    private bool _isSignEnable = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void WarningSignEnable()
    {
        _isSignEnable = true;
        spriteRenderer.enabled = true;
        beforeSignEnable?.Invoke();
    }

    public void WarningSignDisable()
    {
        _isSignEnable = false;
        spriteRenderer.enabled = false;
        _time = 0;
        afterSignDisable?.Invoke();
    }

    private float _time = 0;
    private void Update()
    {
        if (_isSignEnable)
        {
            _time += Time.deltaTime;
            if(_time > remainTime)
            {
                WarningSignDisable();
            }
        }
    }

}
