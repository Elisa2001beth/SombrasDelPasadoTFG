using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBetweenScenes : MonoBehaviour
{
    private void Awake(){
        var noDestruirEntreEscenas = FindObjectsOfType<SettingBetweenScenes>();
        if(noDestruirEntreEscenas.Length>1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
}
