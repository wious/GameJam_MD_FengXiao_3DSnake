using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector] public float step_Length = 0.2f;

    [HideInInspector]
    public float movment_Frequency = 0.1f;

    private float counter;
    public bool move;
    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> delta_Position;

    private List<Rigidbody> nodes;

    private Rigidbody main_Body;

    private Rigidbody head_Body;

    private Transform tr;

    private bool create_Node_At_Tail;

    void Awake()
    {
        tr = transform;
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();
        delta_Position = new List<Vector3>()
        {
            new Vector3(-step_Length, 0), //Left
            new Vector3(0, step_Length), // UP
            new Vector3(step_Length, 0), // RIGHT
            new Vector3(0, -step_Length) // DOWN
        };
    }


    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();
    }

    private void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }

    void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());
        head_Body = nodes[0];
    }

    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int) PlayerDirection.COUNT);
        direction = (PlayerDirection) dirRandom;
    }

    void InitPlayer()
    {
        SetDirectionRandom();
        switch (direction)
        {
            
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0, 0);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0, 0);
                break;
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0, 0);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0, 0);

                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3( 0,Metrics.NODE, 0);
                nodes[2].position = nodes[0].position - new Vector3( 0,Metrics.NODE * 2f, 0);

                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3( 0,Metrics.NODE, 0);
                nodes[2].position = nodes[0].position + new Vector3( 0,Metrics.NODE * 2f, 0);
                break;
        }
    }

    void Move()
    {
        Vector3 dPosition = delta_Position[(int) direction];

        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;
        main_Body.position = main_Body.position + dPosition;
        head_Body.position = head_Body.position + dPosition;

        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;
            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }

        if (create_Node_At_Tail)
        {
            create_Node_At_Tail = false;
            GameObject newNode = Instantiate(tailPrefab,nodes[nodes.Count-1].position,Quaternion.identity);
            newNode.transform.SetParent(transform,true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
        }
    }

    void CheckMovementFrequency()
    {
        counter += Time.deltaTime;
        if (counter >= movment_Frequency)
        {
            counter = 0;
            move = true;
        }
    }

    public void SetInputDirection(PlayerDirection dir)
    {
        if (dir == PlayerDirection.UP&& direction == PlayerDirection.DOWN||
            dir == PlayerDirection.DOWN&& direction == PlayerDirection.UP||
            dir == PlayerDirection.RIGHT&& direction == PlayerDirection.LEFT||
            dir == PlayerDirection.LEFT&& direction == PlayerDirection.RIGHT)
        {
            return;
        }

        direction = dir;

        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;
        move = false;
        Move();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.FRUIT)
        {
            target.gameObject.SetActive(false);
            create_Node_At_Tail = true;
            GameplayerController.instance.IncreaseScore();
            AudioManager.instance.Player_PickUpSound();
        }
        if (target.tag == Tags.WALL|| target.tag == Tags.BOMB||target.tag == Tags.TAIL)
        {
            Time.timeScale = 0f;
            AudioManager.instance.Player_DeadSound();
            SceneManager.LoadScene("GameOver");
        }
    }


}
