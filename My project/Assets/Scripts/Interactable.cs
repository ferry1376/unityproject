using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Add or remove an InteractionEvent component to his game object.
    public bool useEvents;
    //message displayed to player when looking at an interactable.
    [SerializeField]
    public string promptMessage;

    public virtual string onLook()
    {
        return promptMessage;
    }

    //this function will be called from our player.
    public void BaseInteract()
    {
        if(useEvents)
           GetComponent<InteractionEvent>().onInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //we wont have any code written in this function
        //this is a function to be overridden by our subclasses.
    }
    


}
