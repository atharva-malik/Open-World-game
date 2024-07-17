using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject slotToEquip;
    public GameObject inventoryScreenUI;
    public bool isOpen;
    public GameObject ItemInfoUI;

    // Pickup Popup
    public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;

    int start = 0;

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
        Cursor.visible = false;
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
        TriggerPickupPopUp(ItemName, itemToAdd.GetComponent<Image>().sprite);
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public bool checkIfFull(){
        foreach (GameObject slot in slotList){
            if (slot.transform.childCount == 0){
                return false;
            }
        }
        return true;
    }

    void TriggerPickupPopUp(string itemName, Sprite itemSprite){
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;
        start = 0;
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
        if (start > 200)
            pickupAlert.SetActive(false);
        else
            start++;
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Cursor.visible = true;
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            SelectionManager.Instance.disableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            Cursor.visible = false;
            inventoryScreenUI.SetActive(false);
            if(!CraftingSystem.Instance.isOpen)
                Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
            SelectionManager.Instance.enableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
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
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
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