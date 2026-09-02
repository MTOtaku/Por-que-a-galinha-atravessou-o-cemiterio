using System;
using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    // Aqui é os timings das notas em segundos para "Perfect" e "Good"
    public float perfectWindow = 0.05f;
    public float goodWindow = 0.12f;

    private void Update() {
        //Aqui é os input, devo fazer algo como "config de escolher o botão que queira"
        if (Input.GetKeyDown(KeyCode.A)) TryHit(NoteType.AttackUp);
        if (Input.GetKeyDown(KeyCode.S)) TryHit(NoteType.AttackDown);
    }

    void TryHit(NoteType type)
    {
        Note closest = FindClosestNote(type);
        if (closest == null) return;

        float diff = Mathf.Abs((float)Conductor.Instance.SongPositionInSeconds - closest.TargetTime);

        if (diff <= perfectWindow)
            JudgementSystem.Instance.RegisterHit(Judgement.Perfect, closest);
        else if (diff <= goodWindow)
            JudgementSystem.Instance.RegisterHit(Judgement.Good, closest);
        // nem precisa falar q ta fora, ele so n registra msm e vira miss sozinho
    }

    Note FindClosestNote(NoteType type) {
        Note[] activeNotes = FindObjectsOfType<Note>();
        Note closest = null;
        float closestDiff = float.MaxValue;

        foreach (var n in activeNotes)
        {
            if (n.Type != type) continue;
            float diff = Mathf.Abs((float)Conductor.Instance.SongPositionInSeconds - n.TargetTime);
            if (diff < closestDiff)
            {
                closestDiff = diff;
                closest = n;
            }
        }
        return closest;
    }
}
