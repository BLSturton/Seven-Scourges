using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "Battle";
    [SerializeField] private GameObject uiCanvas; // Assign your Canvas in the inspector
    [SerializeField] DontDestroyOnLoad dontDestroyOnLoad;
    [SerializeField] public Vector3 playerReturn;
    [SerializeField] bool battleStart;

    [SerializeField] Inventory inventory;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject thisEnemy;
    [SerializeField] public GameObject[] enemyTroops;

    private bool isTransitioning = false;

    [SerializeField] public bool fightDelay;
    private void Start()
    {
        fightDelay = false;
        StartCoroutine(startDelay());
        inventory = GameObject.FindWithTag("InventorySystem").gameObject.GetComponent<Inventory>();
         
        // If not assigned, try to find the canvas automatically
        if (uiCanvas == null)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
        if(inventory.battleStart == true && inventory.fightingEnemy == thisEnemy.ToString()) 
        {
            Player = GameObject.FindWithTag("Player");
            Player.transform.position = inventory.playerTransform;
            Destroy(gameObject);
            Player = null;
            inventory.battleStart = false;

        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTransitioning && fightDelay)
        {
            Player = collision.gameObject;
            inventory.playerTransform = Player.transform.position;
            inventory.enemyTroops = enemyTroops;
            battleStart = true;
            inventory.battleStart = true;
            inventory.fightingEnemy = thisEnemy.ToString();
            SceneManager.LoadScene("battle");
            

        }
    }

    public IEnumerator startDelay() 
    {
        yield return new WaitForSeconds(.3f);
        fightDelay = true;
    }
  }

