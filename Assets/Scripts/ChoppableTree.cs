using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxHealth;
    public float treeHealth;
    public Animator animator;

    private void Start() {
        treeHealth = treeMaxHealth;
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            playerInRange = false;
        }
    }

    public void GetHit(){
        StartCoroutine(hit());
    }

    private IEnumerator hit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("shake");
        treeHealth -= 1;
        if (treeHealth <= 0){
            treeIsDead();
        }
    }

    private void treeIsDead()
    {
        Vector3 treePosition = transform.position;
        Destroy(transform.parent.transform.parent.gameObject);
        canBeChopped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"), new Vector3(treePosition.x, treePosition.y+0.1f, treePosition.z), Quaternion.Euler(0,0,0));

    }

    private void Update() {
        if (canBeChopped){
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }
}
