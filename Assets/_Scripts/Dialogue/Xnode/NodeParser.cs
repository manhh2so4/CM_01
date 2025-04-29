using System;
using System.Collections;
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
    Coroutine coroutineNodeParser;
    #endregion 
    public void StartDialogue(NPC_Dialogue npc,DialogueGraph _graph)
    {

        graph = _graph;
        dialogueTrigger = npc;
        dialogueTrigger.SetInteractable(false);
        chatNPC = npc.chat;
        canNextNode = true;
        ShowUI(true);
        ParseNodeHandel();

    }
    void ShowUI(bool isShow){
        dialogueALL.SetActive(isShow);
    }
    void Start()
    {

        chatPlayer = PlayerManager.GetPlayer().GetComponentInChildren<Core>().GetCoreComponent<Chat>();
        dialogueParent.SetActive(false);
        
        nextButton.onClick.AddListener(() =>  NextNode() );
        quitButton.onClick.AddListener(() =>  QuitDialogue() );
    }
    
    void ParseNodeHandel(){
        if(coroutineNodeParser != null){
            StopCoroutine(coroutineNodeParser);
        }
        coroutineNodeParser = StartCoroutine(ParseNode());
    }
    IEnumerator ParseNode(){

        switch(graph.Current.GetNodeType()){
            
            case NodeType.StartNode:
                
                NextNode();
                break;

            case NodeType.ChoiceDialogueNode:
                ChoiceDialogueNode node = (ChoiceDialogueNode)graph.Current;
                ShowChoices(false);
                canNextNode = false;

                speaker.text = GetChat(node.speaker).NameChat;
                dialogue.text = "...";

                GetChat(node.speaker).SetUpChat(node.DialogueText,()=>{
                    canNextNode = true;
                    ShowChoices(true);
                    UpdateChoiceList(node);

                });
   
                break;

            case NodeType.DialogueNode:

                DialogueNode dialogueNode = (DialogueNode)graph.Current;
                ShowChoices(false);
                canNextNode = false;
                speaker.text = GetChat(dialogueNode.speaker).NameChat;
                dialogue.text = dialogueNode.DialogueText;

                GetChat(dialogueNode.speaker).SetUpChat( dialogueNode.DialogueText ,()=>{
                    canNextNode = true;
                }); 
                yield return new WaitUntil(() => canNextNode == true);
                yield return new WaitForSeconds(2f);
                NextNode();
#region event
                break;
            case NodeType.EventNode:

                ((EventNode)graph.Current).InvokeEvent();
                NextNode();
                break;

            case NodeType.ShowUINode:
                ShowUI( ((ShowUINode)graph.Current).isShow );
                NextNode();
                break;

            case NodeType.GotoPointNode:

                bool isdone = false;
                dialogueTrigger.SetDestination( ((GotoPointNode)graph.Current).destination , ()=>{
                    isdone = true;
                    Debug.Log("isdone");
                });
                yield return new WaitUntil(() => isdone == true);
                yield return new WaitForSeconds(0.5f);
                NextNode();
                break;
            case NodeType.WaitingNode:
                yield return new WaitForSeconds( ((WaitingNode)graph.Current).time );
                NextNode();
                break;
            case NodeType.HideCharNode:
                
                float duration = ((HideCharNode)graph.Current).duration;
                dialogueTrigger.ShowNPC(false);
                yield return new WaitForSeconds(duration);
                dialogueTrigger.ShowNPC(true);
                NextNode();

                break;
#endregion

            case NodeType.ActionNode:
                ((BaseActionNode)graph.Current).Trigger();
                NextNode();
                break;
            case NodeType.ConditionNode:
                ConditionBaseNode conditionNode = (ConditionBaseNode)graph.Current;
                
               
                if(conditionNode.tryPass){
                    bool success = false;
                    while( success == false ){
                        success = conditionNode.CheckCondition();
                        yield return new WaitForSeconds(0.5f);
                    }
                }

                BaseNode baseNode = conditionNode.Trigger();
                NextNode(baseNode);
                break;

            case NodeType.ExitNode:
                ExitDialogue( ((ExitNode)graph.Current).exitType );
                break;
        }
    }

    void ExitDialogue( ExitType exitType ){
        if(coroutineNodeParser != null){
            StopCoroutine(coroutineNodeParser);
        }
        switch(exitType){
            case ExitType.LoopDialogue:
                    graph.Start();
                break;
            case ExitType.NextDialogue:
                    dialogueTrigger.NextDialogue();
                    graph.Start();
                break;
            case ExitType.StopDialogue:
                NodePort port = graph.Current.GetOutputPort("exit");

                if (port.IsConnected){
                    graph.Current = port.Connection.node as BaseNode;
                }
                break;
        }

        dialogueALL.SetActive(false);
        dialogueTrigger.isTalking = false;
        dialogueTrigger.SetInteractable(true);

        dialogueTrigger = null;
        chatNPC = null;
        graph = null;
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
#region ChoiceDialogue
    void ShowChoices(bool show){
        if(show){
            buttonParent.gameObject.SetActive(true);
            dialogueParent.SetActive(false);
        }else{
            buttonParent.gameObject.SetActive(false);
            dialogueParent.SetActive(true);
        }
    }
    public void AnswerClicked(int clickedIndex){ 
        ChoiceDialogueNode choice = (ChoiceDialogueNode)(graph.Current);
        NodePort port = choice.GetPort("Answers " + clickedIndex);
        if (port.IsConnected){
            graph.Current = port.Connection.node as BaseNode;
            ResetUI();
            ParseNodeHandel();
        }else{
            Common.LogError("ERROR: ChoiceDialogue port is not connected");
        }
    }
    private void UpdateChoiceList(ChoiceDialogueNode newSegment){
        speaker.text = "Player";
        int answerIndex = 0;
        buttonParent.RemoveAllChild();

        foreach (string answer in newSegment.Answers){
            GameObject btn = Instantiate(buttonPrefab, buttonParent); //spawns the buttons 
            btn.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            int index = answerIndex;
            btn.GetComponentInChildren<Button>().onClick.AddListener((() => { AnswerClicked(index);}));
            answerIndex++;
        }
    }
#endregion
    void NextNode(BaseNode node){
        graph.Current = node;
        ResetUI();
        ParseNodeHandel();
    }
    void NextNode(){
        if( canNextNode == false ){
            ParseNodeHandel();
            return;
        }
        ResetUI();

        if(graph == null) return;
        NodePort exitPort = graph.Current.GetOutputPort("exit");
        if(exitPort == null) return;

        if (exitPort.IsConnected){
            graph.Current = exitPort.Connection.node as BaseNode;
        }else{
            Common.LogError("ERROR: No exit port connected");
        }
        ParseNodeHandel();
    }
    void QuitDialogue(){
        canNextNode = false;
        ExitDialogue(ExitType.None);
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