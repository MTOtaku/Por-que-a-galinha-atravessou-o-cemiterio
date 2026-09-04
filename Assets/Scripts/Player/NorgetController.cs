using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NorgetController : MonoBehaviour {
   public bool IsAirbone {get; private set;}

   [Header("Pulo")]
   [Tooltip("Tempo total no ar")]
   public float jumpDuration = 1.0f;
   
   [Tooltip("Quão Rapido sobre até o pico do pulo")]
   public float riseDuration = 0.15f;

   [Tooltip("Altura do Pulo em Unidades do Unity (Pra mover o sprite de baixo pra cima)")]
   public float jumpHeight = 1.5f;
   
   [Header("Referencias Animação")]
   public Animator animator;
   
   [Header("Input System")]
   public InputActionReference attackUpAction;
   
   private Coroutine jumpRoutine;
   private Vector3 groundPosition;

   void Awake(){
      groundPosition = transform.position;
   }

   void OnEnable(){
      attackUpAction.action.Enable();
      attackUpAction.action.performed += OnAttackUp;
   }

   void OnDisable(){
      attackUpAction.action.performed -= OnAttackUp;
      attackUpAction.action.Disable();
   }

   void OnAttackUp(InputAction.CallbackContext ctx){
      if (!IsAirbone) Jump();
   }
   
   public void Jump(){
      print("Jump");
      if (jumpRoutine != null) StopCoroutine(jumpRoutine); 
      jumpRoutine = StartCoroutine(JumpRoutine());
   }

   private IEnumerator JumpRoutine(){
      IsAirbone = true;
      if (animator != null) animator.SetBool("IsAirbone", true);
      
      Vector3 peakPosition = groundPosition + Vector3.up * jumpHeight;
      Vector3 startPos = transform.position;

      float elapsed = 0f;
      while (elapsed < riseDuration) {
         elapsed += Time.deltaTime;
         transform.position = Vector3.Lerp(startPos, peakPosition, elapsed / riseDuration);
         yield return null;
      }
      transform.position = peakPosition;
      
      float fallDuration = jumpDuration - riseDuration;
      elapsed = 0f;
      while (elapsed < fallDuration) {
         elapsed += Time.deltaTime;
         transform.position = Vector3.Lerp(peakPosition, groundPosition, elapsed / fallDuration);
         yield return null;
      }
      transform.position = groundPosition;
      
      IsAirbone = false;
      if (animator != null) animator.SetBool("IsAirbone", false);
      print("Aterrissou");
   }

   public void PlayReaction(Judgement? judgement) {
      print(judgement == null ? "Reação: Miss" : $"Reação: {judgement}");

      if (animator == null) return; // sem Animator ainda, só o log acima já serve pro teste

      if (judgement == null) animator.SetTrigger("Miss");
      else if (judgement == Judgement.Perfect) animator.SetTrigger("Perfect");
      else animator.SetTrigger("Good");
   }
}
