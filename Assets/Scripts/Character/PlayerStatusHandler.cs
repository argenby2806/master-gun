using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour {

    public static PlayerStatusHandler instance;

    public enum Status
    {
        standing,climbing,aiming,shooting,waitingDeath,dead
    }

    public Status status;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeStatus(Status stat)
    {
        status = stat;
    }
}
