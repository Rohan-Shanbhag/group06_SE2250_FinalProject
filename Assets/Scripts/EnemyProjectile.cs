using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
  public float projectileSpeed;
  private Vector3 projectileDirection;

  // Start is called before the first frame update
  void Start()
  {
      projectileDirection = CharacterOneController.instance.transform.position - transform.position;
      projectileDirection.Normalize();
  }

  // Update is called once per frame
  void Update()
  {
      transform.position += projectileDirection * projectileSpeed * Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
      if(other.tag == "Player")
      {
          CharacterOneHealthController.instance.DamagePlayer();
      }

      Destroy(gameObject);
  }

  private void OnBecameInvisible()
  {
      Destroy(gameObject);
  }
}
