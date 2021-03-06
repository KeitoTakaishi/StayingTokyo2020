﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    SceneLoader instance;
    [SerializeField] string[] sceneName;
    [SerializeField] KeyCode[] sceneKey;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this.GetComponent<SceneLoader>();   
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        
        //if(Input.GetKeyDown(sceneKey[0]))
        if(Input.GetKeyDown(sceneKey[0]))
        {
            ///SceneManager.LoadScene("VaporWave");
            //SceneManager.LoadScene("SampleScene1");
            SceneManager.LoadSceneAsync(sceneName[0], LoadSceneMode.Additive);

        }
        else if(Input.GetKeyDown(sceneKey[1]))
        {
            ///SceneManager.LoadScene("VaporWave");
            //SceneManager.LoadScene("SampleScene1");
            SceneManager.LoadScene(sceneName[1]);

        } else if(Input.GetKeyDown(sceneKey[2]))
        {
            ///SceneManager.LoadScene("VaporWave");
            //SceneManager.LoadScene("SampleScene1");
            SceneManager.LoadScene(sceneName[2]);

        }
        /*
        else if(OscData.scene == 2 || Input.GetKeyDown(sceneKey[1]))
        {
            SceneManager.LoadScene("VaporWave");    
        }
        //else if(Input.GetKeyDown(sceneKey[2]))
         else if(OscData.scene == 3)
        {
            SceneManager.LoadScene("ALife");
        }
        //else if(Input.GetKeyDown(sceneKey[3]))
         else if(OscData.scene == 4)
        {
            SceneManager.LoadScene("GPUPolygonTrail");
        }
         //else if(Input.GetKeyDown(sceneKey[4]))
         else if(OscData.scene == 5)
        {
            //SceneManager.LoadScene("GPUPolygonTrail");
            SceneManager.LoadScene("Doom");
        } 
        */

        //OscData.scene = 0;
    }
}
