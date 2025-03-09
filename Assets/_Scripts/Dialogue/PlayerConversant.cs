using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HStrong.Dialogue;
    public class PlayerConversant : MonoBehaviour {
        [SerializeField] string playerName;
        [SerializeField] Dialogue currentDialogue;
        Chat chat;
        DialogueNode2 currentNode = null;
        NPC currentNPC = null;
        bool isChoosing = false;
        public event Action onUpdated;
        void Start()
        {
            chat =  transform.GetComponentInChildren<Core>().GetCoreComponent<Chat>();
        }
        public void StartDialogue(NPC _NPC,Dialogue newDialogue)
        {
            currentNPC = _NPC;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            onUpdated?.Invoke();
        }
        public bool IsActive()
        {
            return currentDialogue != null;
        }
        public bool IsChoosing()
        {
            return isChoosing;
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }
        public string GetCurrentConversantName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentNPC.GetName();
            }
        }
        public IEnumerable<DialogueNode2> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }
        public void SelectChoice(DialogueNode2 chosenNode)
        {
            currentNode = chosenNode;
            isChoosing = false;
            Next();
        }
        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numPlayerResponses > 0)
            {
                isChoosing = true;
                onUpdated?.Invoke();
                chat.SetUpChat(GetText());
                return;
            }
            DialogueNode2[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Length);
            currentNode = children[randomIndex];
            onUpdated?.Invoke();
            currentNPC.chat.SetUpChat(GetText());
        }
        
        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }



        internal void Quit()
        {
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            currentNPC = null;
            onUpdated?.Invoke();
        }
    }

