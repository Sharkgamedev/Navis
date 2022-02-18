using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Conversation")]
public class ConversationData : ScriptableObject
{
    public string SuspectName;

    [TextArea(3, 5)]
    public string OpeningText;

    public List<ChoiceData> Choices = new List<ChoiceData>();
}
