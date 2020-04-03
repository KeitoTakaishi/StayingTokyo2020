using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preview : MonoBehaviour
{
    [SerializeField] GameObject previewUI;
    bool isVisible = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isVisible = !isVisible;
            Debug.Log(isVisible);
        }
    }
}
