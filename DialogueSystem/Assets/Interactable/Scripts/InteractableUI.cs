using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EvtSystem;

public class InteractableUI : MonoBehaviour
{
    GameObject target = null;

    RectTransform UItransform;
    Vector3 UIdefaultScale;

    private void Start()
    {
        EventDispatcher.AddListener<InteractableChange>(changeInteractable);
        
        UItransform = GetComponent<RectTransform>();
        UIdefaultScale = UItransform.localScale;
    }
    
    void Update()
    {
        if (target != null)
        {
            UItransform.localScale = UIdefaultScale; //unhide ui if not null
            positionUI(target.transform.position, Camera.main);
        }
        else
        {
            UItransform.localScale = Vector3.zero; //hide the ui if null
        }
    }

    void changeInteractable(InteractableChange evt)
    {
        target = evt.interactable.gameObject;
    }

    void positionUI(Vector3 targetPos, Camera camera)
    {
        Vector2 newPos = camera.WorldToScreenPoint(targetPos);
        float distance = camera.WorldToScreenPoint(targetPos).z;

        if (distance < 0) newPos *= -1; //fixes a bug if the target is behind you

        newPos.x = Mathf.Clamp(newPos.x, 0, camera.pixelWidth);
        newPos.y = Mathf.Clamp(newPos.y, 0, camera.pixelHeight);

        UItransform.position = newPos;
    }
}
