using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisonHandlingScript : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private float TimeDelayToDie = 0.25f;
    [Space()]
    
    private Movement _movementScript;

    private void Awake()
    {
        _movementScript = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start Pad":
                Debug.Log("Starting Pad!");
                break;
            case "Finish Pad":
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("Picked up fuel");
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void CrashSequence()
    {
        _movementScript.enabled = false;
        Invoke("ReloadLevel", TimeDelayToDie);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        _movementScript.enabled = true;
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nexSceneIndex = currentSceneIndex + 1;
        if (nexSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nexSceneIndex = 0;
        }
        SceneManager.LoadScene(nexSceneIndex);
    }
}
