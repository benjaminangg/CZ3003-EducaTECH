using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LoadScene : MonoBehaviour
{

    [SerializeField] private string _sceneToLoad;
    // Start is called before the first frame update
    public void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
        CheckUser();
    }


    private void onDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;
    }

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
        CheckUser();
    }

    private void CheckUser(){
        if (FirebaseAuth.DefaultInstance.CurrentUser != null){
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
   
    
}
