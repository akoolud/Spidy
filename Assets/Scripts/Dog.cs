using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : QuestGiver
{
    [SerializeField] QuestLine[] preQuestLines;
    [SerializeField] QuestLine[] pastQuestLines;
    [SerializeField] ItemSO outputitem;
    [SerializeField] ItemSO inputItem;


    int preQuestLineIndex;
    int pastQuestLineIndex;
    public override void Interact(PlayerController player)
    {
        if (preQuestLines != null && preQuestLineIndex < preQuestLines.Length)
        {
            QuestLine questLine = preQuestLines[preQuestLineIndex];
            if (questLine.owner == QuestLineOwner.Player)
            {
                player.PlayQuestBubble(questLine);
            }
            else
            {
                this.PlayQuestBubble(questLine);
            }
            preQuestLineIndex++;
        }
        else if (player.HasItem() && player.GetItem() == inputItem)
        {
            PlayerController.Instance.ClearItem(transform);
            ItemController.DropItem(outputitem, transform.position);
        }
        else if (pastQuestLines != null && pastQuestLineIndex < pastQuestLines.Length)
        {
            QuestLine questLine = pastQuestLines[pastQuestLineIndex];
            if (questLine.owner == QuestLineOwner.Player)
            {
                player.PlayQuestBubble(questLine);
            }
            else
            {
                this.PlayQuestBubble(questLine);
            }
        }
    }
}
