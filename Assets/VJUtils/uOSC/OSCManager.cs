using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;


[System.Serializable]
static public class OscData
{
    public static float vol;
    public static float beat;
};

public class OSCManager : MonoBehaviour
{

    //[SerializeField] string[] addresses;
   
    

    void Start()
    {
        var server = GetComponent<uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    void Update() { 
        
       
    }

    void OnDataReceived(Message message)
    {

        // address
        var address = message.address;
        
        switch(address)
        {
            
            case "/vol":
                OscData.vol = float.Parse (message.values[0].ToString());
                break;

            case "/beat":
                OscData.beat = float.Parse(message.values[0].ToString());
                break;

       
            //-------------------------------------------------------------------------------
            //今回は未使用
            /*
            case "/scene1":
                if( float.Parse(message.values[0].ToString()) == 1.0f ){
                    OscData.scene = 1;
                }
                
                break;

            case "/scene2":
                if(float.Parse(message.values[0].ToString()) == 1.0f)
                {

                    OscData.scene = 2;
                }
                break;

            case "/scene3":
                if(float.Parse(message.values[0].ToString()) == 1.0f)
                {
                    OscData.scene = 3;
                }
                break;

            case "/scene4":
                if(float.Parse(message.values[0].ToString()) == 1.0f)
                {
                    OscData.scene = 4;
                }
                break;

            case "/scene5":
                if(float.Parse(message.values[0].ToString()) == 1.0f)
                {
                    OscData.scene = 5;
                }
                break;

            case "/brightWhiteNoise":
                
                OscData.brightWhiteNoise = float.Parse(message.values[0].ToString());
                break;

            case "/whiteNoise3":
                OscData.whiteNoise3 = float.Parse(message.values[0].ToString());
                break;

            case "/cawbel":
                
                OscData.cawbel = float.Parse(message.values[0].ToString());
                break;
            */

        }

        //Debug.Log(OscData.scene);
    }
}
