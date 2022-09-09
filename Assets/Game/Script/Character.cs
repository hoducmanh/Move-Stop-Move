using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject tempPlayer;
    public Transform meshPlayer;
    public Animator _animator;
    public LayerMask playerMask;
    public Transform PlayerTrans;
    public float speed;
    protected string currAnim = Value.CURRENT_ANIM_IDLE;
    protected List<Vector3> targetPosition = new List<Vector3>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Value.PLAYER))
        {
            targetPosition.Add(other.transform.position);
            //Debug.Log(targetPosition.Count);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Value.PLAYER))
        {
            targetPosition.Remove(other.transform.position);
        }
    }
    public void changeAnimation(string tempAnim)
    {
        if (currAnim != tempAnim)
        {
            _animator.SetTrigger(tempAnim);
            currAnim = tempAnim;
            Debug.Log(currAnim);
        }
    }
}
