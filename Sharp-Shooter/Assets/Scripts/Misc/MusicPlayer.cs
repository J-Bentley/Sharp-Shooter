using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); // prevents duplicate music
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}