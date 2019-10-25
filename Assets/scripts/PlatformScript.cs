using UnityEngine;

public class PlatformScript : MonoBehaviour
{
  private SpriteRenderer rendererComponent;
  private bool hasSpawn;

  void Awake()
  {
    rendererComponent = GetComponent<SpriteRenderer>();
  }

  void Start()
  {
      hasSpawn = false;
  }

  void Update()
  {
    if( rendererComponent.IsVisibleFrom(Camera.main) == true )
    {
      hasSpawn = true;
    } else if(hasSpawn) {
      Destroy(gameObject);
    }
  }
}