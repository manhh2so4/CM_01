using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour{
    public static NodeParser instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    DialogueGraph graph; 
    [SerializeField] GameObject dialogueALL;
    [SerializeField] GameObject dialogueParent;
    [SerializeField] TextMeshProUGUI speaker;
    [SerializeField] TextMeshProUGUI dialogue; 

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    [SerializeField] Button nextButton;
    [SerializeField] Button quitButton;
    
    public void StartDialogue(NPC _NPC,DialogueGraph _graph)
    {
        graph = _graph;
        graph.Start();
        dialogueALL.SetActive(true);  
        NextNode(); 
          
    }
    void Start()
    {
        dialogueParent.SetActive(false);
        nextButton.onClick.AddListener(() => NextNode());
    }
    public void AnswerClicked(int clickedIndex){ 
        ChoiceDialogueNode choice = (ChoiceDialogueNode)(graph.current);
        NodePort port = choice.GetPort("Answers " + clickedIndex);
        if (port.IsConnected){
            graph.current = port.Connection.node as BaseNode;
            ParseNode();
        }else{
            Debug.LogError("ERROR: ChoiceDialogue port is not connected");
        }
    }
    void ParseNode(){
        switch (graph.current.GetNodeType()){
            case NodeType.StartNode:

                ResetUI();
                break;

            case NodeType.ChoiceDialogueNode:

                ShowChoices(true);
                UpdateChoiceList(graph.current as ChoiceDialogueNode);
                break;

            case NodeType.DialogueNode:

                DialogueNode b = graph.current as DialogueNode;
                ShowChoices(false);
                speaker.text = b.speakerName;
                dialogue.text = b.dialogueLine;
                break;

            case NodeType.ExitNode:

                dialogueALL.SetActive(false);
                graph.Start(); 
                ResetUI();
                break;
        }
    }
    void ShowChoices(bool show){
        if(show){
            buttonParent.gameObject.SetActive(true);
            dialogueParent.SetActive(false);
        }else{
            buttonParent.gameObject.SetActive(false);
            dialogueParent.SetActive(true);
        }
    }
    private void UpdateChoiceList(ChoiceDialogueNode newSegment){
        dialogue.text = newSegment.DialogueText;
        speaker.text = newSegment.speakerName;
        int answerIndex = 0;
        foreach (Transform child in buttonParent){
            Destroy(child.gameObject);
        }
        foreach (string answer in newSegment.Answers){
            GameObject btn = Instantiate(buttonPrefab, buttonParent); //spawns the buttons 
            btn.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            int index = answerIndex;
            btn.GetComponentInChildren<Button>().onClick.AddListener((() => { AnswerClicked(index);}));
            answerIndex++;
        }
    }
    public void NextNode(){
        ResetUI();
        
        NodePort exitPort = graph.current.GetOutputPort("exit");
        if (exitPort.IsConnected){
            graph.current = exitPort.Connection.node as BaseNode;

        }else{
            Debug.LogError("ERROR: No exit port connected");
        }
        ParseNode();
    }

    void ResetUI()
    {
        speaker.text ="";
        dialogue.text = "";
        foreach (Transform child in buttonParent){
            Destroy(child.gameObject);
        }
    }
}