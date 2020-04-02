using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OscPreview : MonoBehaviour
{
    [SerializeField] Text[] oscDataText;
    void Start()
    {
        
    }

    void Update()
    {
        oscDataText[0].text = "vol : " +  OscData.vol.ToString();
        oscDataText[1].text = "beat : " + OscData.beat.ToString();
    }
}