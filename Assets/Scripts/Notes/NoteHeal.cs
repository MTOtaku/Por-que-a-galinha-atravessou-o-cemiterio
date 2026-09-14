using UnityEngine;

public class NoteHeal : MonoBehaviour {
    [Tooltip("Air = precisa estar no ar pra coletar, Ground = precisa estar no chão")]
    public NoteType type;
    public float healAmount = 40f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        NorgetController norget = other.GetComponent<NorgetController>();
        if (norget == null) return;

        bool naLaneCerta = (type == NoteType.Air && norget.IsAirbone) ||
                           (type == NoteType.Ground && !norget.IsAirbone);

        if (naLaneCerta) {
            JudgementSystem.Instance.Heal(healAmount);
        }

        Destroy(gameObject);
    }
}