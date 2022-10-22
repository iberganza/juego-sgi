using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    private new Transform camera;
    public float interactionDistance;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {     
            if (hitInfo.collider.gameObject.GetComponent<Interactable>() != null)
            {
                hitInfo.transform.GetComponent<Interactable>().Interact();
            }
        }
        
    }
}
