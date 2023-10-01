using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMom : QuestGiver
{
    [SerializeField] QuestLine[] preQuestLines;
    [SerializeField] QuestLine[] pastQuestLines;
    [SerializeField] QuestLine firstFinishLine;
    [SerializeField] QuestLine secondFinishLine;
    [SerializeField] ItemSO inputItem;

    [SerializeField] ItemSO secondInputitem;

    int preQuestLineIndex;
    int pastQuestLineIndex;
    public override void Interact(PlayerController player)
    {
        if (player.HasItem() && player.GetItem() == inputItem)
        {
            // PlayerController.Instance.ClearItem(transform);
            player.PlayQuestBubble(firstFinishLine);
        }

        else if (player.HasItem() && player.GetItem() == secondInputitem)
        {
            PlayerController.Instance.ClearItem(transform);
            player.PlayQuestBubble(secondFinishLine);
        }
        else if (preQuestLines != null && preQuestLineIndex < preQuestLines.Length)
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
