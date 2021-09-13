using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject frogPrefab;
    [SerializeField] Text attemptText;
    [SerializeField] Text completedText;
    [SerializeField] int targetFrameRate;

    private int _completedRuns;
    private int _attemptCounter;

    public static GameController instance;

    //Static Instance of GameController
    void Awake()
    {
        if (instance == null) {
          instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set FrameRate
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        //Init Counters
        _attemptCounter = 0;
        _completedRuns = 0;

        //Spawn frog
        SpawnFrog();
    }

    //Spawn new frog object
    public void SpawnFrog() {
      Instantiate(frogPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    //Increment attempts Counter
    public void IncrementAttemptCounter() {
      _attemptCounter++;
      attemptText.text = "Attempts: " + _attemptCounter.ToString();
    }

    //Increment CompleteRuns Counter
    public void IncrementCompleteRuns() {
      _completedRuns++;
      completedText.text = "Completed: " + _completedRuns.ToString();
    }


}
