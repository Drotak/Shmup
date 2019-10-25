using UnityEngine;

public class HealthScript : MonoBehaviour
{
  public int hp = 1;

  public bool isEnemy = true;

  private HighscoreScript highscoreScript;
  private GameObject[] hearts;

  void Awake()
  {
    highscoreScript = FindObjectOfType<HighscoreScript>();
    hearts = GameObject.FindGameObjectsWithTag("Heart");
  }

  public void Damage(int damageCount)
  {
    hp -= damageCount;

    if(!isEnemy){
      //hp because heath is decreased already
      hearts[hp].SetActive(false);
    }

    if(hp <= 0)
    {
      if(isEnemy){
        highscoreScript.addToHighscore(5f);
      }
      Destroy(gameObject);
    }
  }
  void OnTriggerEnter2D(Collider2D otherCollider)
  {
    ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if(shot != null)
    {
      if(shot.isEnemyShot != isEnemy){
        Damage(shot.damage);

        Destroy(shot.gameObject);
      }
    }
  }

  public int getHealth()
  {
    return hp;
  }
}
