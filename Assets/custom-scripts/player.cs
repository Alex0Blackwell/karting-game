using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
  public float BestLapTime { get; private set; } = Mathf.Infinity;

  public float LastLapTime { get; private set; } = 0;
  public float CurrentLapTime { get; private set; } = 0;
  public int CurrentLap { get; private set; } = 0;

  private float lapTimerTimestamp;
  private int lastCheckpointPassed = 0;

  private Transform checkpointsParent;
  private int checkpointCount;
  private int checkpointLayer;

    public GameObject lapTimeEntryPrefab;
    public Transform lapTimeListPanel;
    private List<GameObject> lapTimeEntries = new List<GameObject>();
    public float spacing = 10f;
    public float numberOfLaps = 5;
    public GameObject lapTimes;
    public GameObject racePanel;
    public GameObject mobileUI;
    private float  tempLapTime;

  // Start is called before the first frame update
  void Awake() {

    checkpointsParent = GameObject.Find("Checkpoints").transform;
    checkpointCount = checkpointsParent.childCount;
    checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    // carController = GetComponent<carController>();
  }

  void StartLap()
    {
        if(tempLapTime != LastLapTime )
        {
            tempLapTime = LastLapTime;
        }
        else
        {
            tempLapTime = LastLapTime = 0;
        }
        
        AddLapTimeEntry(tempLapTime, CurrentLap);
        Debug.Log("StartLap!");
    CurrentLap++;
    lastCheckpointPassed = 1;
    lapTimerTimestamp = Time.time;

        if (CurrentLap == numberOfLaps + 1)
        {
            CurrentLap--;
            Time.timeScale = 0;
            lapTimes.SetActive(true);

#if UNITY_ANDROID || UNITY_IOS
            mobileUI.SetActive(false);
#endif
        }

    }

  void EndLap() {

    LastLapTime = Time.time - lapTimerTimestamp;
    BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    Debug.Log("Lap time was " + LastLapTime);
    Debug.Log("Best time is " + BestLapTime);
        
  }

  void OnTriggerEnter(Collider collider) {
    if(collider.gameObject.layer == checkpointLayer) {
      // only care about colliding with the layer that has checkpoints
      if(collider.gameObject.name == "1") {
        // crossed the finish line
        if(lastCheckpointPassed == checkpointCount)
          EndLap();
        // regardless of how the lap is finished, a new lap should start
        StartLap();
      }
      // any of the other checkpoints, make sure they're in order
      else if(collider.gameObject.name == (lastCheckpointPassed+1).ToString())
        lastCheckpointPassed++;
    }
  }

  // Update is called once per frame
  void Update() {
    CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

    }

    public void AddLapTimeEntry(float lapTime, int lapNumber)
    {
        if (lapNumber == 0)
        {
            return;
        }

        GameObject lapTimeEntry = Instantiate(lapTimeEntryPrefab, lapTimeListPanel);
        Text lapTimeText = lapTimeEntry.GetComponent<Text>();
        if(lapTime.ToString("F2") == "0.00")
        {
            lapTimeText.text = "Lap " + lapNumber + ": " + "DNF";
        }
        else
        {
            lapTimeText.text = "Lap " + lapNumber + ": " + lapTime.ToString("F2") + " seconds";
        }

        int lapCount = lapTimeListPanel.childCount;
        Vector3 entryPosition = new Vector3(0, -lapCount * (lapTimeEntry.GetComponent<RectTransform>().rect.height + spacing), 0);
        lapTimeEntry.GetComponent<RectTransform>().anchoredPosition = entryPosition;

        // Adjust the scroll rect's content size to fit the new entry
        RectTransform contentRect = lapTimeListPanel.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, (lapCount + 1) * (lapTimeEntry.GetComponent<RectTransform>().rect.height + spacing));
    }

    /*public void ResetCurrentLap()
    {
        CurrentLapTime = 0;
    }*/
}
