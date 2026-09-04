using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configurações do GDD - Norget")]
    [Tooltip("Vida total inicial baseada nos estágios de sapato da galinha.")]
    [SerializeField] private int maxHealth = 850;
    private int currentHealth;

    private void Awake()
    {
        // Inicializamos a vida da galinha conforme especificado no GDD
        currentHealth = maxHealth;
    }

    // Acionado pelo Input System quando o jogador aperta as teclas de Ground Attack (J/K)[cite: 1].
    public void OnGroundAttack(InputAction.CallbackContext context)
    {
        // 'performed' garante que o código execute exatamente no gatilho do input,
        // evitando chamadas duplicadas (started, performed, canceled).
        if (context.performed)
        {
            Debug.Log("Norget acionou o Ground Attack na Down Lane (DL)!");
            // Lógica de acerto da Hitzone inferior será implementada na próxima etapa.
        }
    }

    // Acionado pelo Input System quando o jogador aperta as teclas de Jump & Jump Attack (D/F)[cite: 1].
    public void OnJumpAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Norget acionou o Jump & Jump Attack na Up Lane (UL)!");
            // Lógica de pulo e acerto da Hitzone superior será implementada na próxima etapa.
        }
    }
}
