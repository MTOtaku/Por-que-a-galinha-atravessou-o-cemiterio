using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NorgetController : MonoBehaviour {
   public bool IsAirbone {get; private set;}

   [Header("Pulo")] public float jumpDuration = 1.0f;
   
   [Header("Referencias Animação")]
   public Animator animator;
   
   [Header("Input System")]
   public InputActionReference attackUpAction;
   
   private Coroutine jumpRoutine;


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
      
      yield return new WaitForSeconds(jumpDuration);
      
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
   // Chamar quando a vida chegar em 0, somente pra dar a tela de gameover
   public void Die(){
      
   }
}
