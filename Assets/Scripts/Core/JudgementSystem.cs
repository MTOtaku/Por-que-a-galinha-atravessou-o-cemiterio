using UnityEngine;
using System;
using System.Collections;

public enum Judgement {Perfect, Good, Miss}
public class JudgementSystem : MonoBehaviour {
    public static JudgementSystem Instance;
    public NorgetController norget; //Colocar no inspetor dps

    public int score = 0;
    public int combo = 0;
    public float health = 100f;
    public float healthLossOnMiss = 15f;
    void Awake() => Instance = this;
    public void RegisterHit(Judgement judgement, NoteHittable note) {
        combo++;
        score += judgement == Judgement.Perfect ? 100 : 50;
        print($"{judgement} Hit - Judgement System");
        if (norget != null) {
            norget.PlayReaction(judgement);
            if (note.type == NoteType.Air) norget.Jump();
        }
    }

    public void RegisterDodge(){
        combo++;
        score += 50;
        print($"Desvio - Judgement System");
        if (norget != null) norget.PlayReaction(Judgement.Good);
    }

    public static event Action OnPlayerDied;
    
    public void RegisterMiss() {
        float oldHealth = health;
        combo = 0;
        health -= healthLossOnMiss;
        print($"Vida atual: {health}, antes era: {oldHealth}");
        
        if (norget != null) norget.PlayReaction(Judgement.Miss);
        
        if (health <= 0) {
            GameOverManager.Instance.ShowGameOver();
        }
    }
}
