using System;
using UnityEngine;

public class NoteTrackScroller : MonoBehaviour
{
    [Header("Configuração")] public float unitsPerBeat = 2f; // distancia no editor = 1 beat da musica

    private float speed;

    private void Start()
    {
        float secPerBeat = 60f / Conductor.Instance.bpm;
        speed = unitsPerBeat / secPerBeat;
    }

    private void Update() {
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }
}
