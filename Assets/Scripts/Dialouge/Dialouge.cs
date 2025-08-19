using Doublsb.Dialog;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [SerializeField] Diagnosis diagnosis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene == SceneManager.GetSceneByName("Beach")) 
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
                case "GoblinRouge":
                    GoblinRougeDiagnosis();
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
                textNumber = -1;
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
