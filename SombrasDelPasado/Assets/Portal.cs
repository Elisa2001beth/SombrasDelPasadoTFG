using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public string sceneName;

    void OnCollisionEnter(Collision collision)
    {
        
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
