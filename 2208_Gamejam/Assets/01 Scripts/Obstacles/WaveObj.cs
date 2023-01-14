using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObj : MonoBehaviour
{
    private readonly string SURFBOARD_TAG = "Surfboard";

    [SerializeField] private bool moveHor;
    [SerializeField] private float speed = 2;
    private Vector2 _oriPos;
    private Vector2 _castlePos;

    public IDamgeSystem damage;
    public Wave wave;

    private bool _isGoBack = false;

    private void Start()
    {
        _castlePos = SystemManager.Instance.CastlePos;
    }
    private void OnEnable()
    {
        _oriPos = transform.position;
        _isGoBack = false;
    }
    private void Update()
    {
        if (!_isGoBack)
        {
            if(moveHor)
                transform.position = new Vector2(Vector3.MoveTowards(transform.position, _castlePos, speed * Time.deltaTime).x, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x, Vector3.MoveTowards(transform.position, _castlePos, speed * Time.deltaTime).y);
        }
        else
        {
            if (Vector2.Distance(transform.position, _oriPos) < 0.5f)
            {
                gameObject.SetActive(false);
                wave.SetAppear(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, _oriPos, speed * 3f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(SURFBOARD_TAG))
        {
            _isGoBack = true;
            collision.gameObject.SetActive(false);
        }
        else if (collision.transform.CompareTag(SystemManager.CASTLE_TAG))
        {
            _isGoBack = true;
            damage.DamageToPlayer(Vector2.zero);
        }
    }
}
