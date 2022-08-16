using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snack : MonoBehaviour
{
    private event UnityAction _snacksUpdate;

    [SerializeField] private Transform snackBagPos;
    [SerializeField] private float speed;
    private Stack<Snacks> _snacks = new Stack<Snacks>();

    private void Awake()
    {
        if (!snackBagPos)
            snackBagPos = transform;
        Snacks[] getSnacks = transform.GetComponentsInChildren<Snacks>();

        foreach(Snacks snaacks in getSnacks)
        {
            _snacks.Push(snaacks.SetDisable(transform.position, speed)) ;
        }
    }

    private void Update()
    {
        _snacksUpdate?.Invoke();
    }

    public void GetSnack(Transform target)
    {
        Snacks snack = _snacks.Pop().SetSnacks(target);
        _snacksUpdate += snack.SnacksUpdate;
    }

    public void PutInSnack(Snacks snack)
    {
        _snacks.Push(snack.SetDisable());
        _snacksUpdate -= snack.SnacksUpdate;
    }


}
