using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<string> itemList;
    [SerializeField] public List<GameObject> itemVisual;
    [SerializeField] public GameObject inventoryPicker;

    [SerializeField] public bool inventoryOn;
    [SerializeField] public bool inventoryPickerOn;
    [SerializeField] public bool canInventory;
    [SerializeField] GameObject inventoryImage;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] public int selectedItem;
    [SerializeField] public int maxItem;
    [SerializeField] public bool itemPicked;
    [SerializeField] public int partyListMax;
    [SerializeField] public int partySelected;

    [SerializeField] public GameObject cetusHP;
    [SerializeField] public GameObject raticHP;
    [SerializeField] public GameObject cetusHPSpawn1;
    [SerializeField] public GameObject raticHPSpawn1;
    [SerializeField] public GameObject cetusHPSpawn2;
    [SerializeField] public GameObject raticHPSpawn2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryOn = false;
        inventoryPickerOn = false;
        inventoryImage.SetActive(false);
        inventoryPicker.SetActive(false);
        cetusHP.transform.position = cetusHPSpawn1.transform.position;
        raticHP.transform.position = raticHPSpawn1.transform.position;
    }
    private void Awake()
    {



    }

    // Update is called once per frame
    void Update()
    {
        maxItem = itemList.Count;
        if(itemPicked == false) 
        {
            if (Input.GetKeyDown(KeyCode.K) && inventoryPickerOn && maxItem != 0)
            {
                cetusHP.transform.position = cetusHPSpawn2.transform.position;
                raticHP.transform.position = raticHPSpawn2.transform.position;
                cetusHP.transform.SetAsLastSibling();
                raticHP.transform.SetAsLastSibling();
                inventoryPicker.transform.position = cetusHP.transform.position - new Vector3(100, -70);

                itemPicked = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && inventoryPickerOn)
            {
                if (selectedItem == maxItem && maxItem != 0)
                {
                    selectedItem = 1;
                    inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

                }
                else if(maxItem != 0)
                {
                    selectedItem++;
                    inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

                }
            }
            if (Input.GetKeyDown(KeyCode.A) && inventoryPickerOn)
            {
                if (selectedItem == 1 && maxItem != 0)
                {
                    selectedItem = maxItem;
                    inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

                }
                else if (maxItem != 0)
                {
                    selectedItem--;
                    inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

                }
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.K) && inventoryPickerOn && maxItem != 0) 
            {
                UseItem();
            } 
            if (Input.GetKeyDown(KeyCode.D) && inventoryPickerOn && maxItem != 0)
            {
                if (partySelected == partyListMax)
                {
                    partySelected = 1;
                    inventoryPicker.transform.position = cetusHP.transform.position - new Vector3(100, -70);

                }
                else
                {
                    partySelected++;
                    inventoryPicker.transform.position = raticHP.transform.position - new Vector3(100, -70);

                }
            }
            if (Input.GetKeyDown(KeyCode.A) && inventoryPickerOn)
            {
                if (partySelected == 1)
                {
                    partySelected = 2;
                    inventoryPicker.transform.position = raticHP.transform.position - new Vector3(100, -70);

                }
                else
                {
                    partySelected--;
                    inventoryPicker.transform.position = cetusHP.transform.position - new Vector3(100, -70);

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && !inventoryOn && canInventory && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle"))
        {
            selectedItem = 1;
            inventoryPicker.transform.position = itemVisual[selectedItem - 1].transform.GetChild(0).transform.position;

            playerMove.canMove = false;
            inventoryOn = true;
            inventoryImage.SetActive(true);
            inventoryPicker.SetActive(true);
            inventoryPickerOn = true;
            foreach (GameObject obj in itemVisual)
            {
                if (obj == itemVisual[0] && itemList.Count >= 0)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[0];

                }
                if (obj == itemVisual[1] && itemList.Count >= 1)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[1];

                }
                if (obj == itemVisual[2] && itemList.Count >= 2)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[2];
                }
                if (obj == itemVisual[3] && itemList.Count >= 3)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[3];
                }
                if (obj == itemVisual[4] && itemList.Count >= 4)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[4];
                }
                if (obj == itemVisual[5] && itemList.Count >= 5)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[5];
                }
                if (obj == itemVisual[6] && itemList.Count >= 6)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[6];
                }
                if (obj == itemVisual[7] && itemList.Count >= 7)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[7];
                }
                if (obj == itemVisual[8] && itemList.Count >= 8)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[8];
                }
                if (obj == itemVisual[9] && itemList.Count >= 9)
                {
                    obj.GetComponent<TextMeshProUGUI>().text = itemList[9];
                }

            }

        }
            if (Input.GetKeyDown(KeyCode.J) && inventoryOn && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Battle"))
            {
                inventoryImage.SetActive(false);
                inventoryPicker.SetActive(false);

                inventoryOn = false;
                inventoryPickerOn = false;
                playerMove.canMove = true;
            cetusHP.transform.position = cetusHPSpawn1.transform.position;
            raticHP.transform.position = raticHPSpawn1.transform.position;
            cetusHP.transform.SetAsFirstSibling();
            raticHP.transform.SetAsFirstSibling();
            itemPicked = false;
        }
        
    }

    public void UseItem() 
    {
        if (itemList[selectedItem - 1] == "Dried Meat") 
        {
            Debug.Log("Meating my shit");
        }
        if (itemList[selectedItem - 1] == "Mead")
        {
            Debug.Log("Meading my shit");
        }
        itemList.Remove(itemList[selectedItem - 1]);

        inventoryImage.SetActive(false);
        inventoryPicker.SetActive(false);

        foreach (GameObject obj in itemVisual) 
        {
            obj.GetComponent<TextMeshProUGUI>().text = " ";

        }


        inventoryOn = false;
        inventoryPickerOn = false;
        playerMove.canMove = true;
        cetusHP.transform.position = cetusHPSpawn1.transform.position;
        raticHP.transform.position = raticHPSpawn1.transform.position;
        cetusHP.transform.SetAsFirstSibling();
        raticHP.transform.SetAsFirstSibling();
        itemPicked = false;
    }
}
