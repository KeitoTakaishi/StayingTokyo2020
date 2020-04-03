using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Fluid : MonoBehaviour
{
    [SerializeField] int num;
    bool isUniform;
    bool isSource;
    bool isVortex;
    [SerializeField] float flowVelocity;
    [SerializeField] [Range(0.0f, 2.0f * Mathf.PI)] float ang;
    [Range(-7.0f, 7.0f)] public float gamma;//vortex
    [Range(-5.0f, 5.0f)] public float q;//source

    
    

    [SerializeField] ComputeShader cs;
    [SerializeField] int instancingCount;
    const int BLOCK_SIZE = 64;
    int kernel;
    int threadGroupSize;


    public RenderTexture buffer;

    void Start()
    {
        buffer.Release();
        buffer.enableRandomWrite = true;
        buffer.Create();
        kernel = cs.FindKernel("Init");
        cs.SetTexture(kernel, "flowBuffer", buffer);
        cs.Dispatch(kernel, buffer.width/32, buffer.height/32, 1);
        kernel = cs.FindKernel("Calc");
        cs.SetVector("texSize", new Vector4(buffer.width, buffer.height, 0, 0));
        
    }

    void Update()
    {
        cs.SetFloat("flowVelocity", MidiReciever.knobs[5] * flowVelocity);
        cs.SetFloat("ang", ang);
        
        cs.SetFloat("gamma", MidiReciever.knobs[6] * 14.0f - 7.0f);
        cs.SetFloat("q", MidiReciever.knobs[7] * 10.0f -5.0f);
        //cs.SetFloat("gamma", gamma);
        //cs.SetFloat("q", q);
        cs.SetTexture(kernel, "flowBuffer", buffer);
        cs.Dispatch(kernel, buffer.width / 32, buffer.height / 32, 1);
    }
}
