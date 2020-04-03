using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowerScale : MonoBehaviour
{
    [SerializeField] Vector3 target;
    bool isDone = true;
    float duration;
    
    void Start()
    {
        this.transform.localScale = Vector3.zero;
        duration = Random.Range(5.0f, 15.0f);
    }

    void Update()
    {
       
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(isDone)
            {
                target = Vector3.one * Random.Range(5.0f, 15.0f);
                this.transform.DOScale(target, duration).OnComplete(() => isDone = false);
            } else
            {
                target = Vector3.zero;
                this.transform.DOScale(target, duration).OnComplete(() => isDone = true);
            }
        }
        
    }
}
