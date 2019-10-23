using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(50, 50);

  private float freezeTimer = 0f;
  private float freezeTimerTime = 5f;
  private bool freeze = false;
  private Vector2 movement;
  private Rigidbody2D rigidbodyComponent;

  void Update()
  {
    float inputX = Input.GetAxis("Horizontal");
    float inputY = Input.GetAxis("Vertical");

    movement = new Vector2(
      speed.x * inputX,
      speed.y * inputY
    );

    bool shoot = Input.GetButtonDown("Fire1");
    shoot |= Input.GetButtonDown("Fire2");

    if(shoot)
    {
      WeaponScript weapon = GetComponent<WeaponScript>();
      if(weapon != null)
      {
        weapon.Attack(false);
      }
    }

    var dist = (transform.position - Camera.main.transform.position).z;

    var leftBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 0, dist)
    ).x;

    var rightBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(1, 0, dist)
    ).x;

    var topBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 0, dist)
    ).y;

    var bottomBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 1, dist)
    ).y;

    transform.position = new Vector3(
      Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
      Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
      transform.position.z
    );

    // update freeze timer
    if(freezeTimer > 0)
    {
      freezeTimer -= Time.deltaTime;
    } else if(freeze) {
      setFreeze(false);
    }
  }

  void FixedUpdate()
  {
    if(rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

    rigidbodyComponent.velocity = movement;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // collision with enemy
    bool damagePlayer = true;

    EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
    if(enemy != null)
    {
      HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
      if(enemyHealth != null)
      {
        enemyHealth.Damage(enemyHealth.hp);
      }

      if(damagePlayer)
      {
        HealthScript playerHealth = this.GetComponent<HealthScript>();

        if(playerHealth != null)
        {
          playerHealth.Damage(1);
        }
      }
    }

    // collision with powerup
    PowerUpScript powerUp = collision.gameObject.GetComponent<PowerUpScript>();
    if(powerUp != null)
    {
      freezeTimer = freezeTimerTime;
      setFreeze(true);
      Destroy(powerUp.gameObject);
    }
  }

  void OnDestroy()
  {
      var gameOver = FindObjectOfType<GameOverScript>();
      gameOver.EndGame();
  }

  void setFreeze(bool isFreezed)
  {
    freeze = isFreezed;

    ScrollingScript[] scrollingScripts = transform.parent.parent.GetComponentsInChildren<ScrollingScript>();
    foreach( ScrollingScript scrollingScript in scrollingScripts )
    {
      scrollingScript.setFreeze(isFreezed);
    }

    // all enemies have to freeze
    EnemyScript[] all_enemies = transform.parent.GetComponentsInChildren<EnemyScript>();
    if(all_enemies != null)
    {
      foreach( EnemyScript all_enemy in all_enemies )
      {
        all_enemy.setFreeze(isFreezed);
      }
    }

    GenerateScript[] generateScripts = transform.parent.parent.GetComponentsInChildren<GenerateScript>();
    foreach( GenerateScript generateScript in generateScripts )
    {
      generateScript.setFreeze(isFreezed);
    }
  }
}