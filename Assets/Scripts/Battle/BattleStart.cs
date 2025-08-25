using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "Battle";
    [SerializeField] private GameObject uiCanvas; // Assign your Canvas in the inspector
    [SerializeField] DontDestroyOnLoad dontDestroyOnLoad;

    private bool isTransitioning = false;

    private void Start()
    {
        // If not assigned, try to find the canvas automatically
        if (uiCanvas == null)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTransitioning)
        {
            SceneManager.LoadScene("battle");

        }
    }

  

}
