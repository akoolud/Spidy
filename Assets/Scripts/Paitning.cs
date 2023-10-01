using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paitning : QuestGiver
{
    [SerializeField] QuestLine[] preQuestLines;
    int preQuestLineIndex;
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
        }
    }
}
