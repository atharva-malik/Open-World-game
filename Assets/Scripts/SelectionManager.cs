using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {get;set;}
    public GameObject interaction_Info_UI;
    Text interaction_text;
    public bool onTarget = false;
    public GameObject selectedObject;

    public Image centerDotImage;
    public Image handIcon;

    public bool handIsVisible;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        handIcon.enabled = false;
        centerDotImage.enabled = true;
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

                if(interactable.CompareTag("Pickable")){
                    centerDotImage.enabled = false;
                    handIcon.enabled = true;

                    handIsVisible = true;
                }
                else{
                    centerDotImage.enabled = true;
                    handIcon.enabled = false;

                    handIsVisible = false;
                }
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                centerDotImage.enabled = true;
                handIcon.enabled = false;

                handIsVisible = false;
            }
        }else{
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            centerDotImage.enabled = true;
            handIcon.enabled = false;

            handIsVisible = false;
        }
    }

    public void disableSelection()
    {
        handIcon.enabled = false;
        centerDotImage.enabled = false;
        interaction_Info_UI.SetActive(false);
        selectedObject = null;
    }

    public void enableSelection()
    {
        handIcon.enabled = true;
        centerDotImage.enabled = true;
        interaction_Info_UI.SetActive(true);
    }
}