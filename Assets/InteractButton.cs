using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : Interactable
{
    public string id;
    public override void Interact()
    {
        base.Interact();
        gameObject.GetComponentInParent<GameObject>().SetActive(false);
    }

    private void OnPointerEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnPointerExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    public override string getId() => id; 
}
