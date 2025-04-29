using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue : BaseNPC {

   [SerializeField] DialogueGraph[] dialogues;
   [SerializeField] int indexDialogue = 0;
   public Chat chat {get; private set;}
   NPC npc ;
   public bool isTalking = false;
   [SerializeField] Vector2 destination;
   public FindPathHandle findPathHandle;

   public void SetDestination( Vector2 destination , Action onMoveEnd ){
      findPathHandle.SetDestination( new Vector2i( Mathf.FloorToInt(destination.x), Mathf.FloorToInt(destination.y)) , onMoveEnd );
   }
   protected override void Awake(){
      base.Awake();
      chat =  transform.GetComponentInChildren<Core>().GetCoreComponent<Chat>();
      npc = GetComponent<NPC>();
      findPathHandle = GetComponent<FindPathHandle>();
   }
   
   public override void Interact(){
      if ( isTalking ) return;
      if (dialogues.Length == 0) return;

      if (dialogues[indexDialogue] == null) return;
      isTalking = true;
      NodeParser.instance.StartDialogue(this,dialogues[indexDialogue]);

   }
   public void ShowNPC(bool isShow){
      SetInteractable(isShow);
      npc.Show(isShow);
   }

   public void NextDialogue(){
      indexDialogue++;
      if (indexDialogue >= dialogues.Length) indexDialogue = dialogues.Length - 1;
   }

}