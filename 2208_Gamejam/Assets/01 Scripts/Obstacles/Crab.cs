using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Obstacle
{
    [SerializeField] private float speed = 1;

    private Vector2 _target;

    private void Start()
    {
        _damage = new InstantDeath();
        _target = new Vector2(SystemManager.Instance.castlePos.x, transform.position.y);
    }

    public override void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }
}
