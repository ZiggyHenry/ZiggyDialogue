using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueItem : Interactable
{
    public override void OnInteract()
    {
        DialogueManager.Instance.StartDialogue("DialogueOne");
    }
}
