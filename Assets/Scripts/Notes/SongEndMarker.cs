using UnityEngine;

public class SongEndMarker : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other){
        if (!other.CompareTag("HitZone")) return;

        if (VictoryManager.Instance != null) VictoryManager.Instance.ShowVictory();
        
        Destroy(gameObject);
    }
    
    #if  UNITY_EDITOR // Isso aq é interessante, so mostra no editor, fica invisivel in game
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up * 2, transform.position + Vector3.down * 2);
        UnityEditor.Handles.Label(transform.position + Vector3.up * 2.2f, "FIM DA MUSICA");
    }
    
    #endif
}