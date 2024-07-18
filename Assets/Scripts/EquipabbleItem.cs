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
        if (Input.GetMouseButtonDown(0) && !InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen && !SelectionManager.Instance.handIsVisible && !ConstructionManager.Instance.inConstructionMode){
            SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwingSound);
            animator.SetTrigger("hit");
            // PlayerState.Instance.currentCalories -= 10f;
            StartCoroutine(graduallyReduceCalories());
        }
    }

    IEnumerator graduallyReduceCalories(){
        for (int i = 0; i < 10; i++){
            yield return new WaitForSeconds(0.01f);
            PlayerState.Instance.currentCalories -= 1;
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
