using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : SingletonBehavior<SystemManager>
{
    public readonly static string CASTLE_TAG = "Castle";

    public Timer timer = new Timer();

    [SerializeField] private int remainedBucket = 3;

    public Vector2 castlePos;

    protected override void OnAwake()
    {
        castlePos = GameObject.FindGameObjectWithTag(CASTLE_TAG).transform.position;
    }

    private void Update()
    {
        timer.TimeUpdate();
    }

    public void GetObstacles()
    {
        ObstacleTag[] tags = gameObject.GetComponentsInChildren<ObstacleTag>();

        foreach(ObstacleTag tag in tags)
        {

        }
    }

    
}
