using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OscPreview : MonoBehaviour
{
    [SerializeField] Text[] oscDataText;
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

        if(isVisible)
        {
            for(int i = 0;  i < oscDataText.Length; i++)
            {
                oscDataText[i].enabled  = true;
            }
            
            oscDataText[0].text = "vol : " + OscData.vol.ToString();
            oscDataText[1].text = "beat : " + OscData.beat.ToString();
        } else
        {
            for(int i = 0; i < oscDataText.Length; i++)
            {
                oscDataText[i].enabled = false;
            }
        }
       
    }
}