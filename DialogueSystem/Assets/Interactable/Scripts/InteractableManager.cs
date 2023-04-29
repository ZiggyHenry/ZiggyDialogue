using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvtSystem;

public class InteractableManager
{
    #region singleton

    private static InteractableManager _instance = null;

    private InteractableManager()
    {
        EventDispatcher.AddListener<PlayerInteract>(InteractTrigger); //calls interact trigger when PlayerInteract happens
    }

    public static InteractableManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new InteractableManager();
            }
            return _instance;
        }
    }

    #endregion

    private List<Interactable> _interactables = new List<Interactable>();

    Interactable previousInteractable;

    public void RegisterInteractable(Interactable item)
    {
        _interactables.Add(item);
    }

    public void RemoveInteractable(Interactable item)
    {
        _interactables.Remove(item);
    }

    private void InteractTrigger(PlayerInteract evt)
    {
        InteractWithObjects(evt.interactPosition, evt.interactDirection, evt.interactDistance);
    }

    private void InteractWithObjects(Vector3 interactPosition, Vector3 interactDirection,
        float interactDistance)
    {
        float closestDistance = interactDistance;
        Interactable closestInteractable = null;

        foreach(Interactable item in _interactables)
        {
            float distance = Vector3.Distance(item.transform.position, interactPosition);
            float cosAngle = Vector3.Dot(interactDirection,
                                         item.transform.position - interactPosition);
            if (distance < closestDistance && cosAngle >= 0.0f)
            {
                closestDistance = distance;
                closestInteractable = item;
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.OnInteract();
        }

        if (closestInteractable != previousInteractable)
            EventDispatcher.Raise(
                new InteractableChange
                {
                    interactable = closestInteractable
                });

        previousInteractable = closestInteractable;
    }
}