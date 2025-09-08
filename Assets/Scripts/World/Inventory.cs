using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<string> itemList;
    [SerializeField] public List<GameObject> itemVisual;
    [SerializeField] public GameObject inventoryPicker;

    [SerializeField] public bool inventoryOn;
    [SerializeField] public bool inventoryPickerOn;
    [SerializeField] public bool canInventory;
    [SerializeField] public GameObject inventoryImage;
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

    [SerializeField] public bool inventoryBattleUsed;
    [SerializeField] public bool battleWait;

    [SerializeField] public Vector3 playerTransform;

    [SerializeField] public bool battleStart;
    [SerializeField] public GameObject[] enemyTroops;

    //Some other shit idk
    [SerializeField] public List<string> enemyTattles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventoryOn = false;
        inventoryPickerOn = false;
        inventoryImage.SetActive(false);
        inventoryPicker.SetActive(false);
        cetusHP.transform.position = cetusHPSpawn1.transform.position;
        raticHP.transform.position = raticHPSpawn1.transform.position;
        inventoryBattleUsed = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Beach")) 
        {
            foreach(GameObject enemy in enemyTroops) 
            {
                if(enemy.tag == "GoblinRouge" && enemyTattles.Contains("GoblinRouge"))
                    {
                    enemy.transform.GetChild(0).gameObject.SetActive(true);
                    }
            }
        }




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

                itemPicked = true;
               
                inventoryPicker.transform.position = cetusHP.transform.position - new Vector3(100, -70);
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
            if(partySelected == 1 && cetusHP.GetComponent<HPWorld>().HP != cetusHP.GetComponent<HPWorld>().MaxHP) 
            {
                cetusHP.GetComponent<HPWorld>().HP = cetusHP.GetComponent<HPWorld>().HP + 2;

                    if (cetusHP.GetComponent<HPWorld>().HP > cetusHP.GetComponent<HPWorld>().MaxHP) 
                {
                    cetusHP.GetComponent<HPWorld>().HP = cetusHP.GetComponent<HPWorld>().MaxHP;
                  
                }
            }
            if (partySelected == 2 && raticHP.GetComponent<HPWorld>().HP != raticHP.GetComponent<HPWorld>().MaxHP)
            {
                raticHP.GetComponent<HPWorld>().HP = raticHP.GetComponent<HPWorld>().HP + 2;
                if (raticHP.GetComponent<HPWorld>().HP > raticHP.GetComponent<HPWorld>().MaxHP)
                {
                    raticHP.GetComponent<HPWorld>().HP = raticHP.GetComponent<HPWorld>().MaxHP;
                }
            }
        }
        if (itemList[selectedItem - 1] == "Mead")
        {
            if (partySelected == 1 && cetusHP.GetComponent<HPWorld>().SP != cetusHP.GetComponent<HPWorld>().MaxSP)
            {
                cetusHP.GetComponent<HPWorld>().SP = cetusHP.GetComponent<HPWorld>().SP + 2;
                if (cetusHP.GetComponent<HPWorld>().SP > cetusHP.GetComponent<HPWorld>().MaxSP)
                {
                    cetusHP.GetComponent<HPWorld>().SP = cetusHP.GetComponent<HPWorld>().MaxSP;
                }
            }
            if (partySelected == 2 && raticHP.GetComponent<HPWorld>().SP != raticHP.GetComponent<HPWorld>().MaxSP)
            {
                raticHP.GetComponent<HPWorld>().SP = raticHP.GetComponent<HPWorld>().SP + 2;
                if (raticHP.GetComponent<HPWorld>().SP > raticHP.GetComponent<HPWorld>().MaxSP)
                {
                    raticHP.GetComponent<HPWorld>().SP = raticHP.GetComponent<HPWorld>().MaxSP;
                }
            }
        }
        itemList.Remove(itemList[selectedItem - 1]);

        inventoryImage.SetActive(false);
        inventoryPicker.SetActive(false);

        foreach (GameObject obj in itemVisual) 
        {
            obj.GetComponent<TextMeshProUGUI>().text = " ";

        }
        inventoryBattleUsed = true;


        inventoryOn = false;
        inventoryPickerOn = false;
        playerMove.canMove = true;
        cetusHP.transform.position = cetusHPSpawn1.transform.position;
        raticHP.transform.position = raticHPSpawn1.transform.position;
        cetusHP.transform.SetAsFirstSibling();
        raticHP.transform.SetAsFirstSibling();
        itemPicked = false;

        
    }

    public void BattleOpen() 
    {
      
        if (battleWait == false) 
        {
            StartCoroutine(BattleOpenWait());
        }
       

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

    public void BattleClose() 
    {
        inventoryPicker.transform.position = itemVisual[0].transform.position;
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
    public IEnumerator BattleOpenWait() 
    {
        battleWait = false;
        yield return new WaitForSeconds(.2f);
        selectedItem = 1;
        
        battleWait = true;
    }
    
  
}
