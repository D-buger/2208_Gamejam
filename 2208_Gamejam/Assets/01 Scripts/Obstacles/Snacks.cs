using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snacks : MonoBehaviour
{
    private float _speed;
    private Vector2 _snackBagPos;
    private Transform _targetPos;

    public Snacks SetSnacks(Transform targetPos)
    {
        transform.position = _snackBagPos;
        _targetPos = targetPos;
        gameObject.SetActive(true);
        return this;
    }

    public Snacks SetDisable(Vector2 snackOriPos, float speed)
    {
        _snackBagPos = snackOriPos;
        _speed = speed;
        gameObject.SetActive(false);
        return this;
    }
    public Snacks SetDisable()
    {
        gameObject.SetActive(false);
        transform.position = _snackBagPos;
        return this;
    }
    public void SnacksUpdate()
    {
        if(Vector2.Distance(transform.position, _targetPos.position) < 0.5f)
        {
            SystemManager.Instance.snack.PutInSnack(this);
            _targetPos.gameObject.GetComponent<Seagull>().SetDisable();

        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, Time.time * _speed * 100);
    }
}
