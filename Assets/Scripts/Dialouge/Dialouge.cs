using Doublsb.Dialog;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.ComponentModel;
public class TextScript : MonoBehaviour
{
    public DialogManager dialogManager;
    public PlayerMove playerMove;
    public int textNumber;
    public bool inText;
    public DialogData dialog;
    public string textName;
    public GameObject dialogBox;
    public bool startText;
    [SerializeField] public GameObject selectedObject;
    public Image characterFace;
    public Sprite[] faceList;
    public bool FirstScene = true;

    [SerializeField] Diagnosis diagnosis;
    [SerializeField] Inventory inventory;

    [SerializeField] public bool skipText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<Inventory>();

        Scene scene = SceneManager.GetActiveScene();
        if(scene == SceneManager.GetSceneByName("Beach")) 
        {
            if (FirstScene) 
            {

                playerMove.canMove = false;
                //Starts the very first cutscene
                inText = true;
                startText = true;
                textNumber = 0;
                StartingScene();
                textName = "Starting Scene";
                startText = false;
                FirstScene = false;
            }
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        //Advances text number when in a scene
        if (Input.GetKeyDown(KeyCode.K) && inText && dialogManager.textDone)
        {
            dialogManager.Delay = .05f;

            textNumber = textNumber + 1;
            startText = true;

        }
        if (Input.GetKeyDown(KeyCode.K) && inText && !dialogManager.textDone)
        {
           
            dialogManager.Delay = 0;
            dialogManager.textDone = true;
            StartCoroutine(skipDelay());
        }
        if (startText) 
        {
            inventory.canInventory = false;
            inText = true;
            switch (textName)
            { 
                case "Starting Scene":

                    StartingScene();
                    break;
                case "Chest":
                    
                    Chest();
                    break;
                case "OpenedChest":
                    OpenedChest();
                    break;
                case "MeadChest":
                    MeadChest();
                    break;
                case "GoblinRouge":
                    GoblinRougeDiagnosis();
                    break;
            }
            startText = false;
        }
       
    }
    //Text for the first scene
    public void StartingScene() 
    {
        switch (textNumber)
        {
            case 0:
                playerMove.canMove = false;
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[1];
                dialog = new DialogData("What are we, some kind of Seven Scourges?", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[3];

                dialog = new DialogData("Literally some kind of Seven Scourges prototype?", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 2:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = 0;
                inventory.canInventory = true;
                break;
        }
    }

    //Diagnosis
    public void GoblinRougeDiagnosis()
    {
        inText = true;
        switch (textNumber)
        {
            case 0:

                dialog = new DialogData("This is a Goblin Rouge. HP is 2, attack is 2, and defense is 0.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();

                dialog = new DialogData("They're the lowest ranking members of the Green Brigade. Pretty much the definition of fodder.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 2:
                dialogManager.Hide();

                dialog = new DialogData("They don't know how knives work, so they just kinda throw them and hope for the best.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 3:
                dialogManager.Hide();

                dialog = new DialogData("While they lack in power, they make up for it in number... Kinda.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 4:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                textNumber = 0;

                diagnosis.EndAttackForReal();

                break;
        }
    }
    //For Chest
    public void Chest() 
    {

        switch (textNumber)
        {
            case 0:
                dialogManager.Hide();
                characterFace.enabled = true;
                playerMove.canMove = false;
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];
                dialog = new DialogData("This chest is filled with meat. Somehow, it's not spoiled.", "Cetus");
             
                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("You took some dried meat.", "Cetus");
                inventory.itemList.Add("Dried Meat");
                dialogManager.Show(dialog);
              
                break;
            case 2:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = -1;
                selectedObject.name = "OpenedChest";
                inventory.canInventory = true;

                break;
          
        }
    }

    public void OpenedChest() 
    {
        switch (textNumber) 
        {
            case 0:
                dialogManager.Hide();
                characterFace.enabled = true;
                playerMove.canMove = false;
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];
                dialog = new DialogData("It's empty.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 2:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = -1;
                inventory.canInventory = true;

                break;
        }

        }
    public void MeadChest()
    {
        switch (textNumber)
        {
            case 0:
                dialogManager.Hide();
                characterFace.enabled = true;
                playerMove.canMove = false;
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];
                dialog = new DialogData("The chest is frothing with mead.", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("It's just kinda... In there.", "Cetus");

                dialogManager.Show(dialog);

                break;
            case 2:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("You scoop it all in a bottle.", "Cetus");
                inventory.itemList.Add("Mead");

                dialogManager.Show(dialog);

                break;
            case 3:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = -1;
                selectedObject.name = "OpenedChest";
                inventory.canInventory = true;

                break;
        }
    }

    public IEnumerator skipDelay() 
    {
        Debug.Log("Huh");
               

        yield return new WaitForSeconds(.001f);
       
        dialogManager.textDone = true;
    }
}
