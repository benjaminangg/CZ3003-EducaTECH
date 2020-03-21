using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    public Text emailInput, passwordInput;

    public void Login() { 
    
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
                passwordInput.text).ContinueWith(( task => { 


                    if(task.IsCanceled) {

                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }

                    if (task.IsFaulted) {

                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }

                    if (task.IsCompleted) {
                        print("User is LOGGED IN");
                    }


                }));

    } // login

   

    public void RegisterUser() { 

        if(emailInput.text.Equals("") && passwordInput.text.Equals("")) {

            print("Please enter an email and password to register");
            return;

        }

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text,
                passwordInput.text).ContinueWith((task => { 

                    if(task.IsCanceled) {

                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }

                    if (task.IsFaulted) {

                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }

                    if(task.IsCompleted) {

                        print("Registration COMPLETE");
                    
                    }

                }));

    }

    public void Logout() {

        if(FirebaseAuth.DefaultInstance.CurrentUser != null) {
        
            FirebaseAuth.DefaultInstance.SignOut();
        }

    }


    void GetErrorMessage(AuthError errorCode) {

        string msg = "";

        msg = errorCode.ToString();

        //switch(errorCode) {

        //    case AuthError.AccountExistsWithDifferentCredentials:
        //        // CODE
        //        break;

        //    case AuthError.MissingPassword:
        //        break;

        //    case AuthError.WrongPassword:
        //        break;

        //    case AuthError.InvalidEmail:
        //        break;
        
        //}


        print(msg);

    }

} // class
