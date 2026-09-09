using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MidiChartImporter : EditorWindow {
    [System.Serializable]
    public class MidiNoteData {
        public string pitch;
        public float time;
    }

    [System.Serializable]
    public class MidiNoteListWrapper {
        public List<MidiNoteData> notes;
    }

    [System.Serializable]
    public class PitchMapping {
        public string pitch;      // precisa bater exatamente com o nome exportado pelo Python (ex: "C#2")
        public GameObject prefab; // Note_Air, Note_Ground, Obstacle_Air, Obstacle_Ground...
    }

    public TextAsset midiJsonFile;
    public Transform noteTrack;
    public RhythmSettings rhythm;
    public List<PitchMapping> mappings = new List<PitchMapping>();

    [MenuItem("Tools/Ritmo Galinha/Importar Chart do MIDI")]
    public static void ShowWindow() {
        GetWindow<MidiChartImporter>("Importar Chart MIDI");
    }

    void OnGUI() {
        SerializedObject so = new SerializedObject(this);
        EditorGUILayout.PropertyField(so.FindProperty("midiJsonFile"));
        EditorGUILayout.PropertyField(so.FindProperty("noteTrack"));
        EditorGUILayout.PropertyField(so.FindProperty("rhythm"));
        EditorGUILayout.PropertyField(so.FindProperty("mappings"), true);
        so.ApplyModifiedProperties();

        EditorGUILayout.Space();
        if (GUILayout.Button("Gerar Notas na Cena")) {
            GerarNotas();
        }
    }

    void GerarNotas() {
        if (midiJsonFile == null || noteTrack == null) {
            Debug.LogError("Preencha o arquivo JSON e o NoteTrack antes de gerar.");
            return;
        }

        MidiNoteListWrapper data = JsonUtility.FromJson<MidiNoteListWrapper>(midiJsonFile.text);

        int criadas = 0;
        foreach (var nota in data.notes) {
            GameObject prefab = BuscarPrefab(nota.pitch);
            if (prefab == null) {
                Debug.LogWarning($"Nenhum prefab mapeado pra nota {nota.pitch}, pulando.");
                continue;
            }

            GameObject instancia = (GameObject)PrefabUtility.InstantiatePrefab(prefab, noteTrack);
            instancia.transform.SetParent(noteTrack, false);

            NoteTimingHandle timing = instancia.GetComponent<NoteTimingHandle>();
            if (timing != null) {
                if (rhythm != null) timing.rhythm = rhythm;
                timing.timeInSeconds = nota.time; // dispara o OnValidate e já posiciona certo
            }

            criadas++;
        }

        Debug.Log($"{criadas} notas criadas dentro de {noteTrack.name}.");
    }

    GameObject BuscarPrefab(string pitch) {
        foreach (var m in mappings) {
            if (m.pitch == pitch) return m.prefab;
        }
        return null;
    }
}