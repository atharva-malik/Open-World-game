using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject slotToEquip;
    public GameObject inventoryScreenUI;
    public bool isOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList(){
        foreach (Transform child in inventoryScreenUI.transform){
            if (child.CompareTag("Slot"))
                slotList.Add(child.gameObject);
        }
    }

    public void AddToInventory(string ItemName){
        slotToEquip = findNextEmptySlot();
        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(ItemName), slotToEquip.transform.position, slotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(slotToEquip.transform);
        itemList.Add(ItemName);
    }

    public bool checkIfFull(){
        foreach (GameObject slot in slotList){
            if (slot.transform.childCount == 0){
                return false;
            }
        }
        return true;
    }

    public GameObject findNextEmptySlot(){
        foreach (GameObject slot in slotList){
            if (slot.transform.childCount == 0){
                return slot;
            }
        }
        return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if(!CraftingSystem.Instance.isOpen)
                Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void RemoveItem(string ItemName, int amountToRemove)
    {
        int counter = amountToRemove;
        for (var i = slotList.Count - 1; i >= 0; i--){
            if(slotList[i].transform.childCount > 0){
                if (slotList[i].transform.GetChild(0).name == ItemName + "(Clone)" && counter != 0){
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }
    }

    public void ReCalculateList()
    {
        itemList.Clear();
        foreach (GameObject slot in slotList){
            if (slot.transform.childCount > 0){
                string name = slot.transform.GetChild(0).name;
                string result = name.Replace("(Clone)", "");
                itemList.Add(result);
            }
        }
    }
}