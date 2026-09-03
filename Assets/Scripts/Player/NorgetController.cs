using UnityEngine;
using System.Collections;

public class NorgetController : MonoBehaviour {
   public bool IsAirbone {get; private set;}

   [Header("Pulo")] public float jumpDuration = 0.4f;
   
   [Header("Referencias Animação")]
   public Animator animator;
   
   private Coroutine jumpRoutine;

   void Update(){
      if (Input.GetKeyDown(KeyCode.A) && !IsAirbone) {
         Jump();
      }
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
}
