using UnityEngine;

public class NPC_Dialogue : StaticNPC {
   [SerializeField] DialogueGraph dialogue;
   public Chat chat;
   public bool isTalking = false;
   protected override void Awake(){
      base.Awake();
      chat =  transform.GetComponentInChildren<Core>().GetCoreComponent<Chat>();
   }
   
   public override void Interact(){
      if ( isTalking ) return;
      if (dialogue == null) return;
      isTalking = true;
      NodeParser.instance.StartDialogue(this,dialogue);
   }

}