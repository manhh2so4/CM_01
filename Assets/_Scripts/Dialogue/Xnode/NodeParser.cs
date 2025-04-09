using System;
using System.Linq;
using HStrong.Quests;
using TMPro;
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
        dialogueALL.SetActive(true);
        dialogueALL.SetActive(false);
    }

    [SerializeField] DialogueGraph graph; 
    [SerializeField] bool canNextNode = true;
    #region UI_Elemnents
    [Header("____ UI Elements ____")]
    [SerializeField] GameObject dialogueALL;
    [SerializeField] GameObject dialogueParent;
    [SerializeField] TextMeshProUGUI speaker;
    [SerializeField] TextMeshProUGUI dialogue; 

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    [SerializeField] Button nextButton;
    [SerializeField] Button quitButton;
    #endregion
    #region Chat_Elemnents
    [Header("____ Chat Elements ____")]
    [SerializeField] Chat chatPlayer;
    [SerializeField] Chat chatNPC;
    [SerializeField] NPC_Dialogue dialogueTrigger;
    #endregion 
    public void StartDialogue(NPC_Dialogue npc,DialogueGraph _graph)
    {
        
        graph = _graph;
        dialogueTrigger = npc;
        chatNPC = npc.chat;
        canNextNode = true;
        graph.Start();
        ParseNode();
        
        
    }
    void Start()
    {
        chatPlayer = PlayerManager.GetPlayer().GetComponentInChildren<Core>().GetCoreComponent<Chat>();
        dialogueParent.SetActive(false);
        
        nextButton.onClick.AddListener(() =>  NextNode() );
        quitButton.onClick.AddListener(() =>  QuitDialogue() );
    }
    public void AnswerClicked(int clickedIndex){ 
        ChoiceDialogueNode choice = (ChoiceDialogueNode)(graph.current);
        NodePort port = choice.GetPort("Answers " + clickedIndex);
        if (port.IsConnected){
            graph.current = port.Connection.node as BaseNode;
            ResetUI();
            ParseNode();
        }else{
            Common.LogError("ERROR: ChoiceDialogue port is not connected");
        }
    }
    void ParseNode(){
        switch(graph.current.GetNodeType()){

            case NodeType.StartNode:

                
                dialogueALL.SetActive(true);
                NextNode();
                break;

            case NodeType.ChoiceDialogueNode:
                ChoiceDialogueNode node = (ChoiceDialogueNode)graph.current;
                ShowChoices(false);
                canNextNode = false;
                GetChat(node.speaker).SetUpChat(node.DialogueText,()=>{
                    canNextNode = true;
                    ShowChoices(true);
                    UpdateChoiceList(node);

                });

                speaker.text = GetChat(node.speaker).NameChat;
                dialogue.text = "...";
                //dialogue.text = node.DialogueText;      

                break;

            case NodeType.DialogueNode:

                DialogueNode dialogueNode = (DialogueNode)graph.current;
                ShowChoices(false);
                canNextNode = false;

                GetChat(dialogueNode.speaker).SetUpChat( dialogueNode.DialogueText ,()=>{

                    canNextNode = true;
                    Invoke("NextNode", 2f);

                }); 


                speaker.text = GetChat(dialogueNode.speaker).NameChat;
                dialogue.text = dialogueNode.DialogueText;
                
                
                
                break;
            case NodeType.EventNode:

                ((EventNode)graph.current).InvokeEvent();
                NextNode();
                break;
            case NodeType.GiverQuestNode:

                Debug.Log( "quest Gives " + ((GiverQuestNode)graph.current).GetQuests().Count() );

                foreach (QuestInfoSO questInfo in ((GiverQuestNode)graph.current).GetQuests()){
                    this.GameEvents().questEvent.AddQuestToMap(questInfo);
                }

                NextNode();
                break;
            case NodeType.ConditionQuestNode:
            
                BaseNode baseNode = ((ConditionQuestNode)graph.current).Trigger();
                NextNode(baseNode);
                break;

            case NodeType.ExitNode:

                dialogueALL.SetActive(false);
                graph.Start();

                dialogueTrigger.isTalking = false;
                dialogueTrigger = null;
                chatNPC = null;
                graph = null;
                

                break;

        }
    }
    Chat GetChat(Speaker speaker){
        switch (speaker)
        {
            case Speaker.Player:

                return chatPlayer;
                
            case Speaker.NPC:

                return chatNPC;
        }
        return null;
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
        speaker.text = "Player";
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
    void NextNode(BaseNode node){
        graph.current = node;
        ResetUI();
        ParseNode();
    }
    void NextNode(){

         
        if( canNextNode == false ){
            ParseNode();
            return;
        }

        ResetUI();

        if(graph == null) return;
        NodePort exitPort = graph.current.GetOutputPort("exit");
        if(exitPort == null) return;
        if (exitPort.IsConnected){
            graph.current = exitPort.Connection.node as BaseNode;

        }else{
            Common.LogError("ERROR: No exit port connected");
        }

        ParseNode();
    }
    void QuitDialogue(){
        canNextNode = false;
        graph.Exit();
        ResetUI();
        ParseNode();
    }
    void ResetUI()
    {
        CancelInvoke();
        speaker.text ="";
        dialogue.text = "";
        foreach (Transform child in buttonParent){
            Destroy(child.gameObject);
        }
        chatNPC?.RemoveBoxChat();
        chatPlayer?.RemoveBoxChat();
    }
}