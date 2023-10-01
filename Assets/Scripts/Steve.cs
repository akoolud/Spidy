using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steve : QuestGiver
{
    [SerializeField] QuestLine[] preQuestLines;
    [SerializeField] QuestLine[] pastQuestLines;
    [SerializeField] ItemSO item;
    int itemQuantity = 1;

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
        else if (itemQuantity > 0)
        {
            ItemController.DropItem(item, transform.position);
            itemQuantity--;
        }
        else if (pastQuestLines != null)
        {
            QuestLine questLine = pastQuestLines[pastQuestLineIndex % pastQuestLines.Length];
            if (questLine.owner == QuestLineOwner.Player)
            {
                player.PlayQuestBubble(questLine);
            }
            else
            {
                this.PlayQuestBubble(questLine);
            }
            pastQuestLineIndex++;
        }
    }

}
