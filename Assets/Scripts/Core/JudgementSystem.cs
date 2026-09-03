using UnityEngine;

public enum Judgement {Perfect, Good, Miss}
public class JudgementSystem : MonoBehaviour {
    public static JudgementSystem Instance;
    public NorgetController norget; //Colocar no inspetor dps

    public int score = 0;
    public int combo = 0;
    public float molejo = 100f;
    public float molejoLossOnMiss = 15f;

    void Awake() => Instance = this;
    public void RegisterHit(Judgement judgement, NoteHittable note) {
        combo++;
        score += judgement == Judgement.Perfect ? 100 : 50;
        print($"{judgement} Hit - Judgement System");
        if (norget != null) norget.PlayReaction(judgement);
    }

    public void RegisterMiss() {
        combo = 0;
        molejo -= molejoLossOnMiss;
        if (norget != null) norget.PlayReaction(null);
        
        if (molejo <= 0) {
            // game over aq
        }
    }
}
