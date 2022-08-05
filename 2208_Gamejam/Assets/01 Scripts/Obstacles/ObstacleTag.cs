using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTag : MonoBehaviour
{
    [SerializeField] private string tag;
    public string Tag => !string.IsNullOrEmpty(tag) ? tag : this.gameObject.name;
}
