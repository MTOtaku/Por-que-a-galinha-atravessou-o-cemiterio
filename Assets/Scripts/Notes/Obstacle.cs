using System;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [Tooltip(
        "Reaproveita Air/Ground, mas aqui significa a altura do obstaculo, não a tecla - Ex: Ground = Pule pra desviar, Air = Mantenha no chão pra desviar")]
    public NoteType type;

    private void OnTriggerEnter2D(Collider2D other){
        if (!other.CompareTag("Player")) return;
        
        NorgetController norget = other.GetComponent<NorgetController>();
        if (norget == null) return;
        
        bool desviou = (type == NoteType.Ground && norget.IsAirbone) || 
                (type == NoteType.Air && !norget.IsAirbone);

        if (desviou) {
            JudgementSystem.Instance.RegisterDodge();
            print("Desviou");
        }
        else {
            JudgementSystem.Instance.RegisterMiss();
            print("Bateu no Obstaculo");
        }
        Destroy(gameObject);
    }
}