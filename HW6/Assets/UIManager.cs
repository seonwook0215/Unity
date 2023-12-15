using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public  bool GameIsPaused = false;
    public GameObject PauseCanvas;
    Vector3 startPos=new Vector3(0,0,0);
    Vector3 endPos = new Vector3(5.31089f, 3f, 92f);
    Quaternion quaternion;
    public GameObject SceneCamera;
    public GameObject MainCamera;
    private bool isTransitioning;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        StartCoroutine(StartCutScene());
    }
    IEnumerator StartCutScene()
    {
        GameIsPaused = true;
        yield return new WaitForSecondsRealtime(0.1f);
        //Time.timeScale = 0f;
        StartCoroutine(TransitionCameras(endPos,quaternion));
        yield return new WaitForSecondsRealtime(6f);

        //Time.timeScale = 1f;
        GameIsPaused = false;
        MainCamera.SetActive(true);
        SceneCamera.SetActive(false);
        
    }
    void Update()
    {
        if (GameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    IEnumerator TransitionCameras(Vector3 targerPosition, Quaternion targetRotation)
    {
        isTransitioning = true;
        
        Vector3 initialPosition = SceneCamera.transform.position;
        Quaternion initialRotation = SceneCamera.transform.rotation;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, targerPosition);
        while (isTransitioning)
        {
            float distanceCovered;
            distanceCovered = (Time.time - startTime) * 50f;

            float journeyFraction = distanceCovered / journeyLength;
            SceneCamera.transform.position = Vector3.Lerp(initialPosition, targerPosition, journeyFraction);
            SceneCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, journeyFraction);
            if (journeyFraction >= 1.0f)
            {
                
                isTransitioning = false;
            }
            yield return null;
        }

    }

}
