using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tempPlayer;
    private float inputX;
    private float inputZ;
    public Joystick joystick;
    private Vector3 v_movement;
    public float speed;
    public Transform PlayerTrans;
    public Animator _animator;
    private Transform meshPlayer;
    void Start()
    {
        meshPlayer = tempPlayer.GetComponent<Transform>();
        _animator = meshPlayer.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        HandleWithInput();
    }

    private void Move()
    {
        PlayerTrans.position = Vector3.MoveTowards(PlayerTrans.position, PlayerTrans.position + v_movement, Time.deltaTime * speed);
        Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
        meshPlayer.rotation = Quaternion.LookRotation(lookDir);
    }

    private void HandleWithInput()
    {
        joystickInput();
        v_movement = new Vector3(inputX * speed, 0, inputZ * speed);
        if (v_movement.sqrMagnitude <= 0.01f)
        {
            _animator.SetTrigger(Value.CURRENT_ANIM_IDLE);
        }
        else
        {
            _animator.SetTrigger(Value.CURRENT_ANIM_RUN);
            Move();
        }
    }
    private void joystickInput()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;
    }
}
