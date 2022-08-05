using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEntry : MonoBehaviour
{
    [Space(10)]
    [Header("Bools")]
    [SerializeField]
    private bool animateOnStart = false;
    [SerializeField]
    private bool animateOnEnabled = false;
    [SerializeField]
    private bool offset = false;

    [Space(10)]
    [Header("Timing")]
    [SerializeField]
    private float delay = 0;

    [SerializeField]
    private float effectTime = 1;

    [SerializeField]
    private bool isLoop = false;

    [Space(10)]
    [Header("Scale")]
    [SerializeField]
    private Vector3 startScale;

    [SerializeField]
    private AnimationCurve scaleCurve;


    [Space(10)]
    [Header("Position")]
    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private AnimationCurve posCurve;

    Vector3 endScale;

    Vector3 endPos;

    private void Awake()
    {
        if (animateOnEnabled)
            animateOnStart = false;
        SetupVariables();
    }

    private void Start()
    {
        if (animateOnStart)
        {
            if (isLoop)
            {
                StartCoroutine(LoopAnim());
            }
            else
            {
                StartCoroutine(Animation());
            }
        }
    }

    private void OnEnable()
    {
        if (animateOnEnabled)
        {
            if (isLoop)
            {
                StartCoroutine(LoopAnim());
            }
            else
            {
                StartCoroutine(Animation());
            }
        }
    }


    void SetupVariables()
    {
        endScale = transform.localScale;
        endPos = transform.localPosition;
        if (offset)
        {
            startPos += endPos;
        }
    }

    IEnumerator Animation()
    {
        transform.localPosition = startPos;
        transform.localScale = startScale;
        yield return new WaitForSecondsRealtime(delay);
        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;
        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);
            Vector3 tempScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(perc));
            Vector3 tempPos = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            yield return null;
        } while (perc < 1);
        transform.localScale = endScale;
        transform.localPosition = endPos;
        yield return null;
    }

    IEnumerator LoopAnim()
    {
        transform.localPosition = startPos;
        transform.localScale = startScale;
        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;
        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);
            Vector3 tempScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(perc));
            Vector3 tempPos = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            yield return null;
        } while (perc < 1);
        transform.localScale = endScale;
        transform.localPosition = endPos;
        time = 0;
        perc = 0;
        lastTime = Time.realtimeSinceStartup;
        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = 1 - Mathf.Clamp01(time / effectTime);
            Vector3 tempScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(perc));
            Vector3 tempPos = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            yield return null;
        } while (perc > 0);

        yield return null;

        StartCoroutine(LoopAnim());
    }


}