using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    [ContextMenu("References")]
    void Start()
    {
        User user1 = new User()
        {
            userID = 1,
            userName = "ABC"
        };
        User user2 = user1;
        user2.userName = "XYZ";
        Debug.Log("User1" + user1.userName);
        Debug.Log("User2" + user2.userName);
    }
}
public class User
{
    public int userID;
    public string userName = string.Empty;
}
