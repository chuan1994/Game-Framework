using UnityEngine;

public class BGMController : MonoBehaviour {

    [SerializeField]
    AudioClip DefaultBackGroundMusic;

    int currentLevel;

    private static BGMController instance = null;

    public static BGMController Instance {
        get { return instance; }
    }

    // Use this for initialization
    void Start() {
        playMusic(DefaultBackGroundMusic);
    }

    // Update is called once per frame
    void Update() {

    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
        void playMusic(AudioClip clip) {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}