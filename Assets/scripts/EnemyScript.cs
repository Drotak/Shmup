using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  private bool hasSpawn;
  private bool freeze;
  private MoveScript moveScript;
  private Collider2D coliderComponent;
  private SpriteRenderer rendererComponent;
  private WeaponScript[] weapons;
  private HighscoreScript highscoreScript;


  void Awake()
  {
    weapons = GetComponentsInChildren<WeaponScript>();

    moveScript = GetComponent<MoveScript>();
    coliderComponent = GetComponent<Collider2D>();
    rendererComponent = GetComponent<SpriteRenderer>();
    highscoreScript = FindObjectOfType<HighscoreScript>();
  }

  void Start()
  {
    hasSpawn = false;
    freeze = false;

    coliderComponent.enabled = false;
    moveScript.enabled = false;

    foreach(WeaponScript weapon in weapons)
    {
      weapon.enabled = false;
    }
  }

  void Update()
  {
    if(!hasSpawn){
      if (rendererComponent.IsVisibleFrom(Camera.main))
      {
          Spawn();
      }
    } else {  
      if(!freeze)
      {
        foreach(WeaponScript weapon in weapons)
        {
          if(weapon != null && weapon.CanAttack)
          {
            weapon.Attack(true);
          }
        }
      }

      if( rendererComponent.IsVisibleFrom(Camera.main) == false )
      {
        // Player doesn't shoot the enemy - punish him for that
        highscoreScript.addToHighscore(-2f);
        Destroy(gameObject);
      }
    }
  }

  private void Spawn()
  {
    hasSpawn = true;

    coliderComponent.enabled = true;
    moveScript.enabled = true;

    foreach(WeaponScript weapon in weapons)
    {
      weapon.enabled = true;
    }
  }

  public void setFreeze(bool isFreezed)
  {
    freeze = isFreezed;

    MoveScript move = transform.GetComponent<MoveScript>();
    move.setFreeze(isFreezed);
  }
}
