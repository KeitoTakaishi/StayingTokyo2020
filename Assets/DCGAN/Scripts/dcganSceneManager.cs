using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class dcganSceneManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] dcgan_gpuparticle gpuparticle;
    [SerializeField] Material skyMat;
    bool camBG = false;

    static int beat;
    Vector2 beats;//prev, cur
    void Start()
    {
        beats = Vector2.zero;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            camBG = !camBG;
            if(camBG)
            {
                RenderSettings.skybox = skyMat; 
                cam.clearFlags = CameraClearFlags.Skybox;
            } else
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
            }
        }else if(Input.GetKeyDown(KeyCode.W))
        {
            gpuparticle.calcFlag = !gpuparticle.calcFlag;
        }

        beats.y = OscData.beat;
        if(beats.x != beats.y)
        {
            beat = 1;
        } else
        {
            beat = 0;
        }
        beats.x = beats.y;
        Debug.Log(beat);
    }
}
