using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquipabbleItem : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InventorySystem.Instance.isOpen == false && !CraftingSystem.Instance.isOpen){
            animator.SetTrigger("hit");
        }
    }
}
