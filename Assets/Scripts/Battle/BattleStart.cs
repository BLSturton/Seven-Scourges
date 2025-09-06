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

    [SerializeField] public GameObject[] enemyTroops;

    private bool isTransitioning = false;

    private void Start()
    {
        inventory = GameObject.FindWithTag("InventorySystem").gameObject.GetComponent<Inventory>();
         
        // If not assigned, try to find the canvas automatically
        if (uiCanvas == null)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
        if(inventory.battleStart == true) 
        {
            Player = GameObject.FindWithTag("Player");
            Player.transform.position = inventory.playerTransform;
            inventory.battleStart = false;
            Destroy(this.gameObject);
            Player = null;
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTransitioning)
        {
            Player = collision.gameObject;
            inventory.playerTransform = Player.transform.position;
            inventory.enemyTroops = enemyTroops;
            battleStart = true;
            inventory.battleStart = true;
            SceneManager.LoadScene("battle");
            

        }
    }



}
