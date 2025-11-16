using UnityEngine;
using System.Collections;

public class StartSequenceManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject readyPanel;
    public GameObject setPanel;
    public GameObject goPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Timing")]
    public float startDuration = 2f;
    public float readyDuration = 1.5f;
    public float setDuration = 1.5f;
    public float goDuration = 1f;
    public float gameDuration = 60f;

    [Header("Gameplay")]
    public GhostSpawner ghostSpawner;   // assign in Inspector
    public Transform playerCamera;      // VR headset transform

    void Start()
    {
        // Disable gameplay until sequence ends
        ghostSpawner.gameObject.SetActive(false);

        // Hide all panels
        startPanel.SetActive(false);
        readyPanel.SetActive(false);
        setPanel.SetActive(false);
        goPanel.SetActive(false);

        // Begin sequence
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        // Position the UI in front of the player
        PositionPanel(startPanel);
        PositionPanel(readyPanel);
        PositionPanel(setPanel);
        PositionPanel(goPanel);

        // START
        startPanel.SetActive(true);
        yield return new WaitForSeconds(startDuration);
        startPanel.SetActive(false);

        // READY
        readyPanel.SetActive(true);
        yield return new WaitForSeconds(readyDuration);
        readyPanel.SetActive(false);

        // SET
        setPanel.SetActive(true);
        yield return new WaitForSeconds(setDuration);
        setPanel.SetActive(false);

        // GO
        goPanel.SetActive(true);
        yield return new WaitForSeconds(goDuration);
        goPanel.SetActive(false);

        // Now enable gameplay
        ghostSpawner.gameObject.SetActive(true);

        yield return new WaitForSeconds(gameDuration);

        if (losePanel.activeInHierarchy)
            yield break; // Player has already lost

        // Show win panel
        PositionPanel(winPanel);
        winPanel.SetActive(true);

        ghostSpawner.gameObject.SetActive(false);
    }

    private void PositionPanel(GameObject panel)
    {
        if (panel == null || playerCamera == null)
            return;

        panel.transform.position =
            playerCamera.position + playerCamera.forward * 2f;

        panel.transform.rotation =
            Quaternion.LookRotation(playerCamera.forward);
    }
}
