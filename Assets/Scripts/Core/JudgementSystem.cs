using UnityEngine;
using System;
using System.Collections;

public enum Judgement {Perfect, Good, Miss}
public class JudgementSystem : MonoBehaviour {
    public static JudgementSystem Instance;
    public NorgetController norget; //Colocar no inspetor dps

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
        if (norget != null) {
            norget.PlayReaction(judgement);
            if (note.type == NoteType.Air) norget.Jump();
        }
    }

    public void RegisterDodge(){
        combo++;
        score += 75;
        PerfectCount++;
        print($"Desvio - Judgement System");
        if (norget != null) norget.PlayReaction(Judgement.Perfect);
    }
    
    public void RegisterMiss() {
        float oldHealth = health;
        combo = 0;
        MissCount++;
        health -= healthLossOnMiss;
        print($"Vida atual: {health}, antes era: {oldHealth}");
        
        if (norget != null) norget.PlayReaction(Judgement.Miss);
        
        if (health <= 0) {
            GameOverManager.Instance.ShowGameOver();
        }
    }
}
