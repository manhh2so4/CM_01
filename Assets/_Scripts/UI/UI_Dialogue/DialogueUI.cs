using UnityEngine;
using HStrong.Dialogue;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueUI : MonoBehaviour {
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI AIText;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject AIResponse;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI conversantName;
    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        playerConversant.onUpdated += UpdateUI;
        nextButton.onClick.AddListener(() => playerConversant.Next());
        quitButton.onClick.AddListener(() => playerConversant.Quit());
        UpdateUI();
    }

    private void UpdateUI()
    {
        gameObject.SetActive(playerConversant.IsActive());
        if (!playerConversant.IsActive())
        {
            return;
        }
        conversantName.text = playerConversant.GetCurrentConversantName();
        AIResponse.SetActive(playerConversant.IsChoosing() == false);
        choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
        if(playerConversant.IsChoosing())
        {
            BuildChoiceList();
        }else{
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }

    }

    void Next()
    {
        playerConversant.Next();
        UpdateUI();
    }
    private void BuildChoiceList()
    {
        choiceRoot.DetachChildren();
        foreach (DialogueNode2 choice in playerConversant.GetChoices())
        {
            GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
            var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
            textComp.text = choice.GetText();
            Button button = choiceInstance.GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                playerConversant.SelectChoice(choice);
                UpdateUI();
            });
        }
    }
}