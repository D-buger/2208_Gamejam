using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleSystem : MonoBehaviour
{
    Dictionary<string, List<Obstacle>> obstacles = new Dictionary<string, List<Obstacle>>();

    private void Awake()
    {
        ObstacleTag[] tags = gameObject.GetComponentsInChildren<ObstacleTag>();
        List<Obstacle> obstaclesByTag;
        foreach (ObstacleTag tag in tags)
        {
            Obstacle ob = tag.GetComponent<Obstacle>();
            if (!obstacles.ContainsKey(tag.Tag))
            {
                obstacles.Add(tag.Tag, new List<Obstacle>() { ob });
            }
            else
            {
                obstacles.TryGetValue(tag.Tag, out obstaclesByTag);
                obstacles.Remove(tag.Tag);
                obstaclesByTag.Add(ob);
                obstacles.Add(tag.Tag, obstaclesByTag);
            }
        }

        int num = 0;
        foreach(var n in obstacles)
        {
            num = 0;
            foreach(var m in n.Value)
            {
                num++;
            }
        }
    }
}

[System.Serializable]
public class ObstacleAppearanceRules
{
    [SerializeField] private FloatStorage randAppearTime;
    [SerializeField] private float afterDelay;
    [SerializeField] private string obstacleTagName;
    [SerializeField] private bool isAppearAll;
    
}