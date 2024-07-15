using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance {get;set;}
    public GameObject interaction_Info_UI;
    Text interaction_text;
    public bool onTarget = false;
    public GameObject selectedObject;
    private void Awake() {
        if(instance != null && instance != this){
            Destroy(gameObject);
        }
        else{
            instance = this;
        }
    }

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && hit.distance <= 7 && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
        }else{
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}