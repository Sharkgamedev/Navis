using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Choice")]
public class ChoiceData : ScriptableObject
{
    public string ChoiceText;

    [TextArea(3, 5)]
    public string ConversationText;   

    public bool EndImmedietly; 
}
