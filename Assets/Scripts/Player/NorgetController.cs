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

   [Header("Animação de Correr Por vida")]
   [Tooltip("Abaixo desses % vai pra No Shoes")]
   public float healthThresholdRun2 = 70f;
   [Tooltip("Abaixo desses % vai pra Half Shoes")]
   public float healthThresholdRun3 = 30f;

   [Header("Input System")]
   public InputActionReference attackUpAction;
   public InputActionReference attackDownAction;

   private Coroutine jumpRoutine;
   private Vector3 groundPosition;
   private int currentRunState = -1; 

   void Awake(){
      groundPosition = transform.position;
   }

   void Update(){
      UpdateRunAnimation();
   }

   void UpdateRunAnimation(){
      if (animator == null || JudgementSystem.Instance == null) return;

      float health = JudgementSystem.Instance.health;
      int newState;

      if (health <= healthThresholdRun3) newState = 2;
      else if (health <= healthThresholdRun2) newState = 1;
      else newState = 0;

      if (newState != currentRunState) {
         currentRunState = newState;
         animator.SetInteger("RunState", newState);
      }
   }

   void OnEnable(){
      attackUpAction.action.Enable();
      attackUpAction.action.performed += OnAttackUp;

      attackDownAction.action.Enable();
      attackDownAction.action.performed += OnAttackDown;
   }

   void OnDisable(){
      attackUpAction.action.performed -= OnAttackUp;
      attackUpAction.action.Disable();

      attackDownAction.action.performed -= OnAttackDown;
      attackDownAction.action.Disable();
   }

   void OnAttackUp(InputAction.CallbackContext ctx){
      if (!IsAirbone) Jump();
   }

   void OnAttackDown(InputAction.CallbackContext ctx){
      if (IsAirbone) LandInstantly();
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

      if (animator == null) return;

      if (judgement == null) animator.SetTrigger("Miss");
      else if (judgement == Judgement.Perfect) animator.SetTrigger("Perfect");
      else animator.SetTrigger("Good");
   }

   public void LandInstantly(){
      if (jumpRoutine != null) StopCoroutine(jumpRoutine);
      transform.position = groundPosition;
      IsAirbone = false;
      if (animator != null) animator.SetBool("IsAirbone", false);
   }
}