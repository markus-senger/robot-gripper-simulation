using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class FirebaseHandler : MonoBehaviour
{
    private Dictionary<string, object> data;

    private void Awake()
    {
        data = new Dictionary<string, object>();
    }

    public async void UpdateFirebase(bool isClose, bool isOpen, bool isDown, bool isUp, bool isPos1, bool isPos2)
    {
        bool newData = data.Count == 0 ? true : false;
        foreach(var entry in data)
        {
            switch(entry.Key)
            {
                case "is_close": newData = isClose != (bool)entry.Value; break;
                case "is_open": newData = isOpen != (bool)entry.Value; break;
                case "is_down": newData = isDown != (bool)entry.Value; break;
                case "is_up": newData = isUp != (bool)entry.Value; break;
                case "is_pos1": newData = isPos1 != (bool)entry.Value; break;
                case "is_pos2": newData = isPos2 != (bool)entry.Value; break;
            }
            if (newData) break;
        }


        if(newData)
        {
            data.Clear();

            data.Add("is_close", isClose);
            data.Add("is_intermediate", !isClose && !isOpen);
            data.Add("is_open", isOpen);
            data.Add("is_down", isDown);
            data.Add("is_up", isUp);
            data.Add("is_pos1", isPos1);
            data.Add("is_pos2", isPos2);

            try
            {
                string pathToServiceAccountKey = "Assets/control-panel-robotic-gripper-firebase-adminsdk-ggh0q-2c6b32518a.json";
                var jsonString = File.ReadAllText(pathToServiceAccountKey);
                var builder = new FirestoreClientBuilder
                {
                    JsonCredentials = jsonString
                };
    
                FirestoreDb db = FirestoreDb.Create("control-panel-robotic-gripper", builder.Build());
    
                CollectionReference collection = db.Collection("RoboticGripper");
            
                await collection.Document("c33p54088NhwVib4yqU4").SetAsync(data);
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
