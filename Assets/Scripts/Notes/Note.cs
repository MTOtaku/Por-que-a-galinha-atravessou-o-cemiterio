using UnityEngine;

public class Note : MonoBehaviour {
    public NoteType Type {get; private set;}
    public float TargetTime {get; private set;} // Momento exato em segundos da musica que deveria ser acertada

    private Vector3 startPos, EndPos;
    private float travelTime;
    private float spawnTime;

    public void Init(NoteType type, float targetTime, Vector3 start, Vector3 end, float travel)
    {
        Type = type;
        TargetTime = targetTime;
        startPos = start;
        EndPos = end;
        travelTime = travel;
        spawnTime = (float)Conductor.Instance.SongPositionInSeconds;
    }

    void Update()
    {
        float elapsed = (float)Conductor.Instance.SongPositionInBeats - spawnTime;
        float t = elapsed / travelTime;
        transform.position = Vector3.Lerp(startPos, EndPos, t);
        
        //se passou da hitzone é não foi acertada, dá miss
        if (t > 1.15f)
        {
            JudgementSystem.Instance.RegisterMiss();
            Destroy(gameObject);
        }
    }
}