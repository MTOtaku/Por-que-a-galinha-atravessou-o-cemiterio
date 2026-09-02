using System;
using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    // Aqui é os timings das notas em segundos para "Perfect" e "Good"
    public float perfectWindow = 0.05f;
    public float goodWindow = 0.12f;

    private void Update()
    {
        //Aqui é os input, devo fazer algo como "config de escolher o botão que queira"
        if (Input.GetKeyDown(KeyCode.A)) TryHit(NoteType.Air);
        if (Input.GetKeyDown(KeyCode.S)) TryHit(NoteType.Ground);
    }

    void TryHit(NoteType type)
    {
        NoteHittable[] notes = FindObjectsOfType<NoteHittable>();
        foreach (var note in notes)
        {
            if (note.type == type && note.InHitZone)
            {
                note.MarkAsHit();
                JudgementSystem.Instance.RegisterHit(Judgement.Perfect, note);
                return;
                //Fora disso já é miss
            }
        }
    }
}