using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;


    int index;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // StartNextQuest();
    }

    // void StartNextQuest()
    // {
    //     questSOs[index].StartQuest();
    // }
    // public QuestSO GetCurrentQuest()
    // {
    //     return questSOs[index];
    // }

    // public void TryFinishingQuest()
    // {
    //     ItemSO item = PlayerController.Instance.GetItem();
    //      QuestSO quest = GetCurrentQuest();
    //     if (quest.TryFinishingQuest(item, out ItemSO output))
    //     {
    //         PlayerController.Instance.ClearItem(quest.owner.transform);
    //         ItemController.DropItem(output, quest.owner.transform.position);
    //         index++;
    //          StartNextQuest();
    //     }
    //     else
    //     {
    //         Debug.LogError("mismatch");
    //     }
    // }
}

[System.Serializable]
public class QuestLine
{
    public string line;
    public QuestLineOwner owner;
}

public enum QuestLineOwner
{
    Player,
    SpiderMom,
    Steve,
    VendingMachine,
    Fridge,
    Mouse,
    Cat,
    Dog
}