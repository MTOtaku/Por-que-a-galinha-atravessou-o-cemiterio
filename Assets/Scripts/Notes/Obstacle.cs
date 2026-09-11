using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
    [Tooltip(
        "Reaproveita Air/Ground, mas aqui significa a altura do obstaculo, não a tecla - Ex: Ground = Pule pra desviar, Air = Mantenha no chão pra desviar")]
    public NoteType type;

    [Tooltip("Tempo até o obstaculo ser destruido")]
    public float destroyDelay = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other){
        if (!other.CompareTag("Player")) return;
        
        NorgetController norget = other.GetComponent<NorgetController>();
        if (norget == null) return;
        
        bool desviou = 
            (type == NoteType.Ground && norget.IsAirbone) || 
            (type == NoteType.Air && !norget.IsAirbone) ;//|| 
            //(type == NoteType.Air && norget.IsMidAir);

        if (desviou) {
            JudgementSystem.Instance.RegisterDodge();
            print("Desviou");
            StartCoroutine(DestroyAfterDelay());
        }
        else {
            JudgementSystem.Instance.RegisterMiss();
            print("Bateu no Obstaculo");
            Destroy(gameObject);
        }
        
    }

    private IEnumerator DestroyAfterDelay(){
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
