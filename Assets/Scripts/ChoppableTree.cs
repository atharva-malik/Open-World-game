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

    private void Start() {
        treeHealth = treeMaxHealth;
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
        treeHealth -= 1;
    }

    private void Update() {
        if (canBeChopped){
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }
}
