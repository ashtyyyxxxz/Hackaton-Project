using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    static public QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private List<Quest> quests;

    public Quest GetQuest(string name)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].name == name) return quests[i];
        }
        return null;
    }

    public Quest CreateQuest(string name, string description,UnityEvent onQuestCompleted)
    {
        Quest quest = new Quest();
        quest.name = name;
        quest.description = description;
        quest.onQuestCompleted = onQuestCompleted;
        quests.Add(quest);

        return quest;
    }

}

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public bool isCompleted = false;
    public UnityEvent onQuestCompleted;

    public void CompleteQuest()
    {
        isCompleted = true;
        onQuestCompleted?.Invoke();
    }
}
