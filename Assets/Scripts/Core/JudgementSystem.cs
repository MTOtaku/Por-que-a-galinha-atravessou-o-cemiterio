using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public enum Judgement {Perfect, Good, Miss}
public class JudgementSystem : MonoBehaviour {
    public static JudgementSystem Instance;
    public NorgetController norget; //Colocar no inspetor dps

    [Header("Efeitos de acerto/Erro")] 
    public Transform hitZone;
    public GameObject perfectEffectPrefab;
    public GameObject goodEffectPrefab;
    public GameObject missEffectPrefab;
    
    [Header("Impacto visual de notas")]
    public GameObject impactPiecePrefab;
        
    public int score = 0;
    public int combo = 0;
    public float maxHealth = 100f;
    public float health;
    public float healthLossOnMiss = 15f;

    public float PerfectCount = 0;
    public float GoodCount = 0;
    public float MissCount = 0;
    void Awake(){
        Instance = this;
        health = maxHealth;
    }
    public void RegisterHit(Judgement judgement, NoteHittable note) {
        combo++;
        score += judgement == Judgement.Perfect ? 100 : 50;
        if (judgement == Judgement.Perfect) PerfectCount++; else GoodCount++;
        
        print($"{judgement} Hit - Judgement System");
        SpawnEffect(judgement);
        SpawnImpactPiece(note);
        
        if (norget != null) {
            norget.PlayReaction(judgement);
            
            if (note.type == NoteType.Air) norget.Jump(true);
            //else if (note.type == NoteType.Ground && norget.IsAirbone) norget.LandInstantly();
        }
    }

    public void RegisterDodge(){
        combo++;
        score += 75;
        PerfectCount++;
        print($"Desvio - Judgement System");
        //SpawnEffect(Judgement.Perfect);
        if (norget != null) norget.PlayDodgeSound();
    }
    
    public void RegisterMiss() {
        float oldHealth = health;
        combo = 0;
        MissCount++;
        health -= healthLossOnMiss;
        
        SpawnEffect(Judgement.Miss);

        if (norget != null) {
            norget.PlayReaction(Judgement.Miss);
        }
        
       if (health <= 0) {
           norget.Die();
       }
    }

    public void Heal(float amount){
        float oldHealth = health;
        health = Mathf.Min((health + amount), maxHealth);
    }
    
    private void SpawnEffect(Judgement judgement){
        if (hitZone == null) return;

        GameObject prefab = judgement switch {
            Judgement.Perfect => perfectEffectPrefab,
            Judgement.Good => goodEffectPrefab,
            _ => missEffectPrefab
        };
        if (prefab != null) Instantiate(prefab, hitZone.position, Quaternion.identity);
    }
    
    private void SpawnImpactPiece(NoteHittable note){
        if (impactPiecePrefab == null) return;

        SpriteRenderer noteSprite = note.GetComponent<SpriteRenderer>();
        if (noteSprite == null || noteSprite.sprite == null) return;

        GameObject piece = Instantiate(impactPiecePrefab, note.transform.position, Quaternion.identity);
        ImpactPiece impact = piece.GetComponent<ImpactPiece>();
        if (impact != null) impact.Init(noteSprite.sprite, note.transform.lossyScale);
    }
}
