﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    //Determines if this particular checkpoint is active
    public bool activated = false;

    //A list of all the other checkpoints
    public static GameObject[] CheckpointsList;

    public Sprite cpActive;
    public Sprite cpNotActive;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        //print("Test1");
        //CheckpointsList = GameObject.FindGameObjectsWithTag("Checkpoint");
        sr = this.GetComponent<SpriteRenderer>();
	}
	
    //Activate this checkpoint
	public void ActivateCheckpoint()
    {
        //Deactivate other checkpoints
        foreach (GameObject cp in CheckpointsList)
        {
            cp.GetComponent<Checkpoint>().activated = false;
        }
        //...and then activate this one
        activated = true;

        SaveLoad.save.checkpoint = this.name;

        SaveLoad.Save();
    }

    public void Update()
    {
        if (activated) sr.sprite = cpActive;
        else sr.sprite = cpNotActive;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Activate when player touches the checkpoint
        if (other.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }

    //Static function to get activated checkpoint
    public static Vector3 GetActiveCheckpointPosition()
    {
        Vector3 result = new Vector3(0, 0, 0);
        foreach (GameObject cp in CheckpointsList)
        {
            if (cp.GetComponent<Checkpoint>().activated)
            {
                result = cp.transform.position;
                break;
            }
        }
        return result;
    }
}
