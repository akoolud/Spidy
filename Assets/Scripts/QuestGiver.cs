using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IHasInteract
{


    [SerializeField] string questGiverName;
    [SerializeField] BubbleController bubbleController;



    public string GetName()
    {
        return questGiverName;
    }
    public virtual bool HasQuest()
    {
        return true;
    }
    public void OnHover(bool state)
    {
        UIManager.Instance.UpdateInteractHintVisibility();
    }
    public virtual void Interact(PlayerController player)
    {
        Debug.LogError("NEEDS TO BE OVERRIDEN");
    }

    protected void PlayQuestBubble(QuestLine questLine)
    {
        bubbleController.PlayQuest(questLine);
    }
}

