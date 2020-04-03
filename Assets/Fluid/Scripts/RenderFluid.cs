using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class RenderFluid : MonoBehaviour
{
    [SerializeField] Vector2 particleLife;

    #region instancingParams
    [SerializeField] int instancingCount;
    ComputeBuffer argsBuffer;
    private uint[] args = new uint[5];
    [SerializeField] Mesh srcMesh;
    [SerializeField] Material instancingMat;
    #endregion

    struct Parameters
    {
        Vector3 pos;
        Vector2 life;
        public Parameters(Vector3 p, Vector2 l)
        {
            pos = p;
            life = l;
        }
    };

    #region ComputeShader
    [SerializeField] ComputeShader cs;
    const int BLOCK_SIZE = 64;
    ComputeBuffer parameterBuffer;
    int kernel;
    int threadGroupSize;
    #endregion

    public Vector2 simulationSpace;
    public Fluid fluid;

    void Start()
    {
        InitInstancingParameter();
        CreateComputeBuffer();
        kernel = cs.FindKernel("PosUpdate");
        cs.SetVector("simulationSpace", simulationSpace);
    }

    
    void Update()
    {
        cs.SetFloat("time", Time.realtimeSinceStartup);
        if(fluid.q != 0.0f && fluid.gamma != 0.0f)
        {
            cs.SetFloat("isSource", 1.0f);
        } else
        {
            cs.SetFloat("isSource", 0.0f);
        }

        cs.SetTexture(kernel, "flowBuffer", fluid.buffer);
        cs.SetBuffer(kernel, "posBuffer", parameterBuffer);
        cs.Dispatch(kernel, instancingCount / 64, 1, 1);
        instancingMat.SetBuffer("paramsBuffer", parameterBuffer);
        Graphics.DrawMeshInstancedIndirect(srcMesh, 0, instancingMat, new Bounds(Vector3.zero, Vector3.one * 32.0f), argsBuffer);
    }

    private void InitInstancingParameter()
    {
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        args[0] = srcMesh.GetIndexCount(0);
        args[1] = (uint)instancingCount;
        args[2] = srcMesh.GetIndexStart(0);
        args[3] = srcMesh.GetBaseVertex(0);
        args[4] = 0;
        argsBuffer.SetData(args);
    }

    private void CreateComputeBuffer()
    {
        parameterBuffer = new ComputeBuffer(instancingCount, Marshal.SizeOf(typeof(Parameters)));
        //Vector3[] pos = new Vector3[instancingCount];
        //Vector2[] life = new Vector2[instancingCount];
        Parameters[] parames = new Parameters[instancingCount];

        float halfSim = 0.5f * simulationSpace.y;
        for(int i = 0; i < instancingCount; i++)
        {
            
            Vector3 p = new Vector3(-0.5f * simulationSpace.x + Random.Range(0, 20f), 0, Random.Range(-halfSim, halfSim));
            //pos[i] = p;

            float _ = Random.Range(particleLife.x, particleLife.y);
            Vector2 l = new Vector2(_, _);
            //life[i] = l;

            parames[i] = new Parameters(p, l);
        }

        parameterBuffer.SetData(parames);

    }

}
