using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOO;

public class BeachBall : Obstacle
{
    [SerializeField]
    private Transform[] curveTrans;
    private Vector2[] curvePointVec;
    [SerializeField]
    private int point;
    [SerializeField]
    private float speed;

    private Vector2[] curveVec;

    private void Start()
    {
        curvePointVec = new Vector2[curveTrans.Length];
        for (int i = 0; i < curveTrans.Length; i++)
            curvePointVec[i] = curveTrans[i].position;

        curveVec = Util.CurvePointsOfVectors(point, curvePointVec);
        transform.position = curveVec[0];
    }

    float time = 0;
    public override void Moving()
    {
        time += Time.deltaTime * speed;
        transform.position = curveVec[Mathf.Clamp((int)time, 0, point)];
    }


    public override void MouseDown(Vector2 mousePos)
    {

    }
    public override void OnDrag(Vector2 mousePos)
    {

    }
    public override void MouseUp(Vector2 mousePos)
    {

    }

    public override void AfterDamage()
    {
        throw new System.NotImplementedException();
    }
}
