using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)){
            SelectQuickSlot(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)){
            SelectQuickSlot(2);
        }else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)){
            SelectQuickSlot(3);
        }else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)){
            SelectQuickSlot(4);
        }else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)){
            SelectQuickSlot(5);
        }else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)){
            SelectQuickSlot(6);
        }else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)){
            SelectQuickSlot(7);
        }
    }

    private void SelectQuickSlot(int v)
    {
        if (checkIfSlotIsFull(v)){
            if (selectedNumber != v){
                selectedNumber = v;

                if (selectedItem!= null){
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = getSelectedItem(v);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;
                foreach (Transform child in numbersHolder.transform){
                    child.transform.Find("Text (Legacy)").GetComponent<Text>().color = Color.gray;
                }
                Text toBeChanged = numbersHolder.transform.Find("number"+v.ToString()).transform.Find("Text (Legacy)").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }else{
                selectedNumber = -1;

                if (selectedItem!= null){
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                foreach (Transform child in numbersHolder.transform){
                    child.transform.Find("Text (Legacy)").GetComponent<Text>().color = Color.gray;
                }
            }
        }
    }

    private GameObject getSelectedItem(int slotNum)
    {
        return quickSlotsList[slotNum - 1].transform.GetChild(0).gameObject;
    }

    private bool checkIfSlotIsFull(int slotNum)
    {
        if (quickSlotsList[slotNum - 1].transform.childCount > 0){
            return true;
        }
        return false;
    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        InventorySystem.Instance.ReCalculateList();
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}