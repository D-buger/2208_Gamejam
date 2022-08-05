using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Obstacle
{
    [Space(20)]
    [SerializeField] private float speed = 1;

    private Vector2 _target;

    private bool _isReturnToStart = false;

    private void Start()
    {
        _damage = new InstantDeath();
        _target = new Vector2(SystemManager.Instance.castlePos.x, transform.position.y);
    }

    public override void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public void Dragging(Vector2 ori,Vector2 pos)
    {
        float min = ori.x > _appearPos.x ? _appearPos.x : ori.x;
        float max = ori.x < _appearPos.x ? _appearPos.x : ori.x;
        transform.position = new Vector2(Mathf.Clamp(pos.x, min, max), transform.position.y);
    }
}
