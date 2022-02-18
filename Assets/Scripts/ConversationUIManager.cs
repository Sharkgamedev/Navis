using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ConversationUIManager : MonoBehaviour 
{
    public TextMeshProUGUI ConversationText;
    public TextMeshProUGUI CharacterNameText;
    public CanvasGroup ConversationBox;
    public ScrollRect ScrollView;

    public ConversationData Opener;

    public GameObject CloseButton;

    public ConversationData CurrentConv = null;

    public void Start ()
    {
        ShowConversation(Opener);
    }

    [System.Serializable]
    public class ChoiceButton
    {
        public TextMeshProUGUI ButtonText;
        public CanvasGroup ButtonView;
    }

    public List<ChoiceButton> ChoiceButtons = new List<ChoiceButton>();

    public void ShowConversation(ConversationData Data)
    {
        if (CurrentConv != null) return; // We are already in a conversation

        ScrollView.verticalNormalizedPosition = 1.0f;
        CurrentConv = Data;
        CharacterNameText.text = Data.SuspectName;
        ConversationText.text = Data.OpeningText;

        ConversationBox.DOFade(1.0f, 0.5f);

        for (int i = 0; i < 4; i++)
        {
            if (i < Data.Choices.Count)
            {
                ChoiceButtons[i].ButtonView.DOFade(1.0f, 0.5f);
                ChoiceButtons[i].ButtonText.text = Data.Choices[i].ChoiceText;
            }
            else
            {
                ChoiceButtons[i].ButtonView.DOFade(0.0f, 0.5f);
            }
        }
    }

    public void ChooseChoice(int Num)
    {
        ScrollView.verticalNormalizedPosition = 1.0f;
        if (Num > CurrentConv.Choices.Count)
        {
            Debug.LogWarning("Out of bounds..");
            return;
        }

        if (CurrentConv.Choices[Num].EndImmedietly)
        {
            if (CurrentConv.Choices[Num].ConversationText == "QUIT") Application.Quit();

            CurrentConv = null;
            ConversationBox.DOFade(0.0f, 0.5f);
            return;
        }

        ConversationText.text = CurrentConv.Choices[Num].ConversationText;
        for (int i = 0; i < 4; i++)
            ChoiceButtons[i].ButtonView.DOFade(0.0f, 0.5f);
        
        CloseButton.SetActive(true);
    }

    public void CloseConversation()
    {
        CloseButton.SetActive(false);
        ConversationBox.DOFade(0.0f, 0.5f);

        CurrentConv = null;
    }
}
