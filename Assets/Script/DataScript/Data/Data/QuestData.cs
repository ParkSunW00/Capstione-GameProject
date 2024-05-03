using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    private int questId;
    private int nextQuestId;
    private int previousQuestId;
    private int npcId;
    private string questName;
    private string questDetail;

    public void print()
    {
        Debug.Log("Now Quest num : " + questId);
    }
    
    
    public int QuestId
    {
        get => questId;
        set => questId = value;
    }

    public int NextQuestId
    {
        get => nextQuestId;
        set => nextQuestId = value;
    }

    public int PreviousAuestId
    {
        get => previousQuestId;
        set => previousQuestId = value;
    }

    public int NpcId
    {
        get => npcId;
        set => npcId = value;
    }

    public string QuestName
    {
        get => questName;
        set => questName = value;
    }

    public string QuestDetail
    {
        get => questDetail;
        set => questDetail = value;
    }
}
