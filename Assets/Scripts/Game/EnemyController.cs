using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 1f;
    public float damage = 3f;

    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        if (_player != null)
        {
            StartCoroutine(PursuePlayer());
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        Die();
    }
    private IEnumerator PursuePlayer()
    {
        animator.SetTrigger("walk");
        while (true)
        {
            Vector3 distance = _player.transform.position - transform.position;
            distance.y = 0;
            if (distance.magnitude > 2f)
            {
                transform.forward = distance;
                transform.Translate(distance.normalized * moveSpeed * Time.deltaTime,Space.World);
            }
            else
            {
                animator.SetTrigger("attack");
                break;
            }
            yield return null;
        }
    }
    private void Die()
    {
        StopAllCoroutines();
        MessageCenter.Instance.PostMessage(new KillEnemyMessage());
        animator.SetTrigger("die");
    }


    public void OnAttackAnimEnd()
    {
        MessageCenter.Instance.PostMessage(new AttackPlayerMessage(damage));
    }
    public void OnDieAnimationEnd()
    {
        Destroy(gameObject);
    }
}
