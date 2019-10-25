using UnityEngine;

public class ShotScript : MonoBehaviour
{
  public int damage = 1;

  public bool isEnemyShot = false;

  private SpriteRenderer rendererComponent;

  void Awake()
  {
    rendererComponent = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if( rendererComponent.IsVisibleFrom(Camera.main) == false )
    {
      Destroy(gameObject);
    }
  }
}
