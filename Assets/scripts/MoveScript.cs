using UnityEngine;

public class MoveScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(10, 10);

  public Vector2 direction = new Vector2(-1, 0);

  private Vector2 movement;
  private Rigidbody2D rigidbodyComponent;
  private bool freeze;

  void Update()
  {
    if(freeze)
    {
      movement = new Vector2(0, 0);  
    } else {
      movement = new Vector2(
        speed.x * direction.x,
        speed.y * direction.y
      );
    }
  }

  void FixedUpdate()
  {
    if(rigidbodyComponent == null)
    {
      rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    rigidbodyComponent.velocity = movement;
  }

  public void setFreeze(bool isFreezed)
  {
    freeze = isFreezed;
  }
}
