using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] QuestLine[] enteranceLine;

    [SerializeField] StartPoint[] startPoints;
    void Start()
    {
        Vector3 p = transform.position;
        SceneName lastScene = GameManager.Instance.GetLastScene();

        foreach (StartPoint point in startPoints)
        {
            if (point.fromScene == lastScene)
            {
                p = point.transform.position;
                break;
            }
        }
        PlayerController.Instance.transform.position = p;

        if (enteranceLine != null && enteranceLine.Length > 0)
        {
            GameManager.Instance.TryPlayEnteranceLine(enteranceLine[0]);
        }
    }
}


[System.Serializable]
public class StartPoint
{
    public Transform transform;
    public SceneName fromScene;
}