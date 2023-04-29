using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EvtSystem;

public class InteractableChange : EvtSystem.Event //example of a custom event, remember: it inherits from event
{
    public Interactable interactable;
}

public class PlayerInteract : EvtSystem.Event //Event of type PlayerInteract
{
    public Vector3 interactPosition;
    public Vector3 interactDirection;
    public float interactDistance;
}

public class ShowDialogueText : EvtSystem.Event
{
    public string text;
    public CharacterID id;
    public float duration;
}

public class PlayAudio : EvtSystem.Event
{
    public AudioClip clipToPlay;
}

public struct ResponseData
{
    public string text;
    public int karmaScore;

    public UnityAction buttonAction;
}

public class ShowResponses : EvtSystem.Event
{
    public ResponseData[] responses;
}

public class DisableUI : EvtSystem.Event
{

}

