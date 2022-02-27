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
    }

    void GetKeyboardInput()
    {
        horizaontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");
        if (horizaontal != 0)
        {
            vertical = 0;
        }

        void SetMovement()
        {
            if (vertical != 0)
            {
                _playerController.SetInputDirection((vertical == 1)? PlayerDirection.UP: PlayerDirection.DOWN);
            }
        }
    }
}
