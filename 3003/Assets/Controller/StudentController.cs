using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;


public class StudentController : MonoBehaviour
{
    public Text nameInput, classInput;

    private Student data;

    private string DATA_URL = "https://cz3003-aef98.firebaseio.com/";

    private DatabaseReference databaseReference;
    


    void Start() {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData() { 

        if(nameInput.text.Equals("") && classInput.text.Equals("")) {
            print("NO DATA");
            return;
        } 


        var user = FirebaseAuth.DefaultInstance.CurrentUser;

        string uid;

        if (user == null){
            uid = "NULL";
        }

        else{
            uid = user.UserId;
        }

        data = new Student(nameInput.text, classInput.text);

        string jsonData = JsonUtility.ToJson(data);

        databaseReference.Child(uid).
            SetRawJsonValueAsync(jsonData);
            
    }

    public void LoadData() {

        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync()
                    .ContinueWith((task => {

                        if (task.IsCanceled) {

                        }

                        if (task.IsFaulted) { 
                            
                        }

                        if (task.IsCompleted) {

                            DataSnapshot snapshot = task.Result;

                            string studentData = snapshot.GetRawJsonValue();

                            //Student m = JsonUtility.FromJson<Student>(studentData);

                            foreach(var child in snapshot.Children) {

                                string t = child.GetRawJsonValue();

                                Student extractedData = JsonUtility.FromJson<Student>(t);

                                print("The Student is: " + extractedData.Name);
                                print("The Student's class is: " + extractedData.ClassNo);

                            }

                        }

        }));

    }


}