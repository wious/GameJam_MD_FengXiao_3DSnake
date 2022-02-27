using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;

    private int horizaontal = 0, vertical = 0;

    public enum Axis
    {

Horizontal,
Vertical

    }

    // Start is called before the first frame update
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizaontal = 0;
        vertical = 0;

        GetKeyboardInput();
        SetMovement();
    }

    void GetKeyboardInput()
    {
        //horizaontal = (int) Input.GetAxisRaw("Horizontal");
        //vertical = (int) Input.GetAxisRaw("Vertical");

        horizaontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);
        if (horizaontal != 0)
        {
            vertical = 0;
        }
    }


    int GetAxisRaw(Axis axis)
        {
            if (axis == Axis.Horizontal)
            {
                bool left = Input.GetKeyDown(KeyCode.A);
                bool right = Input.GetKeyDown(KeyCode.D);
                if (left)
                {
                    return -1;
                }

                if (right)
                {
                    return 1;
                }

                return 0;
            }
            else if(axis == Axis.Vertical)
            {
                bool up = Input.GetKeyDown(KeyCode.W);
                bool dowm = Input.GetKeyDown(KeyCode.S);
                if (up)
                {
                    return 1;
                }

                if (dowm)
                {
                    return -1;
                }

                return 0;

            }
            return 0;
        }
        void SetMovement()
        {
            if (vertical != 0)
            {
                _playerController.SetInputDirection((vertical == 1)? PlayerDirection.UP: PlayerDirection.DOWN);
            }
            else if (horizaontal !=0)
            {
                _playerController.SetInputDirection((horizaontal == 1)? PlayerDirection.RIGHT: PlayerDirection.LEFT);

            }
        }
    }
    

