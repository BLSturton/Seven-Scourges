using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CooldownWipe : MonoBehaviour
{
    [SerializeField] private float cooldownDuration = 3f;
    [SerializeField] private bool startHidden = true;

    [SerializeField] BattleManager battleManager;
    [SerializeField] BattleMove battleMove;
    private Material cooldownMaterial;
    private float cooldownTimer;
    public bool isOnCooldown = false;

    void Start()
    {
        cooldownTimer = battleMove.ultCoolDown;
        // Get the renderer and create a material instance (to avoid modifying the original)
        Renderer rend = GetComponent<Renderer>();
        cooldownMaterial = rend.material;

        if (startHidden)
            gameObject.SetActive(false);
    }


    public void StartCooldown()
    {
        gameObject.SetActive(true);
        cooldownTimer = cooldownDuration;
        isOnCooldown = true;
        cooldownMaterial.SetFloat("_FillAmount", 1f); // Reset to full
    }

    void Update()
    {
        
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            float fillAmount = Mathf.Clamp01(cooldownTimer / cooldownDuration);
            cooldownMaterial.SetFloat("_FillAmount", fillAmount);

            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                gameObject.SetActive(false);
            }
        }
        if (battleManager.turnReset == true) 
        {
            isOnCooldown = false;
            gameObject.SetActive(false);
           
        }
    }
    
}