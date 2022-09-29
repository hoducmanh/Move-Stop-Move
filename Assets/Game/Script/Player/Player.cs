using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject Target;
    public ChangeMaterial ChangeMat;
    private float inputX;
    private float inputZ;
    public Joystick joystick;
    private Vector3 v_movement;
    private bool isAttackable = true;
    private bool isAttacking;
    private float timer = 0;
    private bool isTheFirstTimeAttack = true;

    void FixedUpdate()
    {
        HandleWithInput();
        ScanningEnemy();
        TargetingEnemy();
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
        if (v_movement.sqrMagnitude <= 0.1f)
        {
            isAttackable = true;
            if (!isAttacking) {
                ChangeAnimation(Value.CURRENT_ANIM_IDLE);
            }
        }
        else
        {
            isAttackable = false;
            isAttacking = false;
            ChangeAnimation(Value.CURRENT_ANIM_RUN);
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
        Vector3 currTarget = Vector3.zero;
        if (isAttackable)
        {
            if (targetPosition.Count > 0)
            {
                if (isTheFirstTimeAttack)
                {
                    isAttacking = true;
                    ChangeAnimation(Value.CURRENT_ANIM_ATTACK);
                    currTarget = (targetPosition[0].PlayerTrans.position - meshPlayer.localPosition).normalized;
                    meshPlayer.LookAt(currTarget);
                    Throwing(currTarget);
                    isTheFirstTimeAttack = false;
                    timer = 0;
                }
                else if (timer > 3f)
                {
                    isTheFirstTimeAttack = true;
                }
                else if (timer > 0.5f)
                {
                    isAttacking = false;
                }

            }
        }
    }
    private void ScanningEnemy()
    {
        if (targetPosition.Count > 0)
        {
            if (targetPosition[0].isDead == false)
            {
                isAttacking = true;
                Attack();
            }
            else
                targetPosition.RemoveAt(0);
        }
        
    }
    private void Counter()
    {
        timer += Time.deltaTime;
    }
    private void TargetingEnemy()
    {
        if (targetPosition.Count > 0)
        {
            if (targetPosition[0].isDead == false)
            {
                Target.SetActive(true);
                Target.transform.position = targetPosition[0].transform.position;
                Target.transform.SetParent(targetPosition[0].transform);
            }
            else
            {
                targetPosition.RemoveAt(0);
            }
        }
        else
        {
            Target.SetActive(false);
        }
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Value.OBSTACLE))
        {
            ChangeMat.MakeTransparent();
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag(Value.OBSTACLE))
        {
            ChangeMat.GiveColor();
        }
    }
}
