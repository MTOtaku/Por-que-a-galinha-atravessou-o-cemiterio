using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    [Tooltip("Distância (em unidades) do centro da hitZone considerada Perfect. O restante, enquanto ainda dentro da HitZone, conta como Good.")]
    public float perfectDistance = 0.3f;
    
    private void Update() {
        //Aqui é os input, devo fazer algo como "config de escolher o botão que queira"
        if (Input.GetKeyDown(KeyCode.A)) TryHit(NoteType.Air);
        if (Input.GetKeyDown(KeyCode.S)) TryHit(NoteType.Ground);
    }

    void TryHit(NoteType type) {
        NoteHittable[] notes = FindObjectsOfType<NoteHittable>();
        foreach (var note in notes) {
            if (note.type == type && note.shape == NoteShape.Tap && note.InHitZone) {
                float distance = note.DistanceToHitZoneCenter();
                Judgement judgement = distance <= perfectDistance ? Judgement.Perfect : Judgement.Good;
                note.MarkAsHit();
                JudgementSystem.Instance.RegisterHit(judgement, note);
                return;
                //Fora disso já é miss
            }
        }
    }
}