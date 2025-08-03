using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.UI;

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

    public Image characterFace;
    public Sprite[] faceList;
    public bool FirstScene = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Starts the very first cutscene
        inText = true;
        startText = true;
        textNumber = 0;
        StartingScene();
        textName = "Starting Scene";
        startText = false;
       FirstScene = false;
       

    }

    // Update is called once per frame
    void Update()
    {
        //Advances text number when in a scene
        if (Input.GetKeyDown(KeyCode.K) && inText)
        {
            textNumber = textNumber + 1;
            switch (textName)
            {
                case "Starting Scene":
                    StartingScene();
                    break;
                
            }

        }
        if (startText) 
        {
            
            inText = true;
            switch (textName)
            { 
                case "Starting Scene":
                    StartingScene();
                    break;
                case "Chest":
                    
                    Chest();
                    break;
                case "Evil Chest":
                    EvilChest();
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
                dialog = new DialogData("Oh I'm scourging it", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[3];

                dialog = new DialogData("Literally scourging my shit rn", "Cetus");
                dialogManager.Show(dialog);
                break;
            case 2:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = 0;
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
                dialog = new DialogData("It's a chest.", "Cetus");
             
                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("There's probably something inside, but Bryce hasn't added in chest functionality yet.", "Cetus");

                dialogManager.Show(dialog);
              
                break;
            case 2:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("Maybe he will later.", "Cetus");
                dialogManager.Show(dialog);

                break;
            case 3:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = -1;
                break;
        }
    }
    public void EvilChest()
    {
        switch (textNumber)
        {
            case 0:
                dialogManager.Hide();
                characterFace.enabled = true;
                playerMove.canMove = false;
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];
                dialog = new DialogData("It's a chest...", "Cetus");

                dialogManager.Show(dialog);
                break;
            case 1:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("... Or so you thought.", "Cetus");

                dialogManager.Show(dialog);

                break;
            case 2:
                dialogManager.Hide();
                characterFace.GetComponent<UnityEngine.UI.Image>().sprite = faceList[7];

                dialog = new DialogData("It's actually an EVIL CHEST!!!!!!!!!!", "Cetus");
                dialogManager.Show(dialog);

                break;
            case 3:
                dialogManager.Hide();
                characterFace.enabled = false;
                textName = "None";
                inText = false;
                playerMove.canMove = true;
                textNumber = -1;
                break;
        }
    }
}
