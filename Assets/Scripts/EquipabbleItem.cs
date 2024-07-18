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
        if (Input.GetMouseButtonDown(0) && !InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen && !SelectionManager.Instance.handIsVisible){
            SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwingSound);
            animator.SetTrigger("hit");
        }
    }

    public void GetHit(){
        GameObject selectedTree = SelectionManager.Instance.selectedTree;

        if (selectedTree!= null){
            SoundManager.Instance.PlaySound(SoundManager.Instance.chopSound);
            selectedTree.GetComponent<ChoppableTree>().GetHit();
        }
    }
}
