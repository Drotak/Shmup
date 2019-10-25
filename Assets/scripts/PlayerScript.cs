using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(50, 50);

  private float freezeTimer = 0f;
  private float freezeTimerTime = 5f;
  private bool freeze = false;
  private Vector2 movement;
  private Rigidbody2D rigidbodyComponent;
  private float shrinkTimer = 0f;
  private float shrinkTime = 25f;
  private bool wantToCollect = false;
  private bool haveTimeStop = false;

  private GameObject timeStop;
  private Image timeStopImage;
  private GameObject shrinkItem;
  private Image shrinkItemImage;

  private GameOverScript gameOver;

  void Awake()
  {
    gameOver = FindObjectOfType<GameOverScript>();
    timeStop = GameObject.Find("TimeStop");
    timeStopImage = timeStop.gameObject.GetComponent<Image>();
    shrinkItem = GameObject.Find("ShrinkItem");
    shrinkItemImage = shrinkItem.gameObject.GetComponent<Image>();
  }

  void Start()
  {
    timeStop.SetActive(false);
    shrinkItem.SetActive(false);
  }

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
      timeStopImage.fillAmount = freezeTimer/freezeTimerTime;
    } else if(freeze) {
      hideTimeStop();
      setFreeze(false);
    }

    // update shrink timer
    if(isMinimized()){
      if(shrinkTimer > 0)
      {
        shrinkTimer -= Time.deltaTime;
        shrinkItemImage.fillAmount = shrinkTimer/shrinkTime;
      } else {
        Grow();
        shrinkTimer = shrinkTime;
      }
    } else {
      hideShrinkItem();
    }

    if(Input.GetKeyDown("e"))
    {
      wantToCollect = true;
    }

    if(Input.GetKeyUp("e"))
    {
      wantToCollect = false;
    }

    if(wantToCollect && haveTimeStop)
    {
      freezeTimer = freezeTimerTime;
      setFreeze(true);

      haveTimeStop = false;

      wantToCollect = false;
    }
  }

  void showTimeStop()
  {
    timeStop.SetActive(true);
  }

  void hideTimeStop()
  {
    timeStop.SetActive(false);

    if(haveTimeStop)
    {
      timeStop.SetActive(true);
    }

    timeStopImage.fillAmount = 1f;
  }

  void showShrinkItem()
  {
    shrinkItem.SetActive(true);
  }

  void hideShrinkItem()
  {
    shrinkItem.SetActive(false);
    shrinkItemImage.fillAmount = 1f;
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
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    // collision with powerup
    PowerUpScript powerUp = collider.gameObject.GetComponent<PowerUpScript>();
    if(powerUp != null)
    {
      if(!haveTimeStop && wantToCollect)
      {
        if(powerUp.gameObject.name == "TimeStop(Clone)"){
          haveTimeStop = true;
          showTimeStop();
          Destroy(powerUp.gameObject);
        } else if(powerUp.gameObject.name == "ShrinkItem(Clone)") {
          Shrink();
          Destroy(powerUp.gameObject);
        }

        wantToCollect = false;
      }
    }
  }

  void OnDestroy()
  {
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

  bool isMinimized()
  {
    if(this.transform.localScale.x == 0.2f || this.transform.localScale.y == 0.2f)
    {
      return false;
    } else {
      return true;
    }
  }

  void Shrink()
  {
    if(!isMinimized())
    {
      shrinkTimer = shrinkTime;
    }

    showShrinkItem();

    float x_coord = this.transform.localScale.x / 2;
    float y_coord = this.transform.localScale.y / 2;
    this.transform.localScale = new Vector3(x_coord, y_coord, 1);
  }

  void Grow()
  {
    float x_coord = this.transform.localScale.x * 2;
    float y_coord = this.transform.localScale.y * 2;
    this.transform.localScale = new Vector3(x_coord, y_coord, 1);
  }
}
