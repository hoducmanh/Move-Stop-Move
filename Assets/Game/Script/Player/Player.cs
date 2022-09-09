using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float inputX;
    private float inputZ;
    public Joystick joystick;
    private Vector3 v_movement;
    private bool isAttackable =true;
    private bool isAttacking;
    private float timer = 0;

    void Start()
    {
        meshPlayer = tempPlayer.GetComponent<Transform>();
        _animator = meshPlayer.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        HandleWithInput();
        ScanningEnemy();
        Counter();
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
            isAttackable = true;
            if (!isAttacking) {
                timer = 0;
                changeAnimation(Value.CURRENT_ANIM_IDLE);
            }
        }
        else
        {
            isAttackable = false;
            changeAnimation(Value.CURRENT_ANIM_RUN);
            Move();
        }
    }
    private void joystickInput()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;
    }
    private void Attack()
    {
        //Debug.LogWarning(isAttackable);
        if (isAttackable)
        {
            changeAnimation(Value.CURRENT_ANIM_ATTACK);
            if (timer > 3f)
            {
                isAttacking = false;
                isAttackable = false;
            }
            Debug.Log(isAttacking + " " + isAttackable);
        }
    }
    private void ScanningEnemy()
    {
        if (targetPosition.Count > 0)
        {
            //if (isAttacking == false)
            //{
                isAttacking = true;
                Attack();
                //Debug.Log(isAttacking);
            //}
        }
    }
    private void Counter()
    {
        timer += Time.deltaTime;
    }
}
