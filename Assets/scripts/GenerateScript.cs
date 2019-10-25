using UnityEngine;

public class GenerateScript : MonoBehaviour
{
    public Transform platform1Prefab;
    public Transform platform2Prefab;

    public int startAmount = 5;
    public float generationSpeed = 2.5f;
    public float scale_min = 0.05f;
    public float scale_max = 0.5f;
    public int layer = 0;

    private float countDown = 0f;
    private bool freeze = false;

    void Start()
    {
        for( var i = 0; i < startAmount; i++ )
        {
            GeneratePlatform(true);
        }
    }

    void Update()
    {
        if(!freeze)
        {
            if(countDown > 0)
            {
                countDown -= Time.deltaTime;
            } else {
                countDown = generationSpeed;

                GeneratePlatform(false);
            }
        }
    }

    void GeneratePlatform(bool isInit)
    {
        Transform platformPrefab;

        if(Random.Range(0f, 1f) > 0.5f)
        {
            platformPrefab = platform1Prefab;
        } else {
            platformPrefab = platform2Prefab;
        }

        Transform platform = Instantiate(platformPrefab);

        platform.transform.parent = transform;

        var renderer = platform.GetComponent<Renderer>();
        renderer.sortingOrder = layer;

        float scale = Random.Range(scale_min, scale_max);

        platform.transform.localScale = new Vector3(scale, scale, 1);

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        if( isInit )
        {
            Vector3 offset = new Vector3(Random.Range(-width/2, width/2), Random.Range((-height/2)+1f, (height/2)-1f), layer);
            platform.position = transform.position + offset;
        } else {
            Vector3 offset = new Vector3(Random.Range(width/1.5f, width*1.5f), Random.Range((-height/2)+1f, (height/2)-1f), 0);
            platform.position = new Vector3(
                Camera.main.transform.position.x + offset.x,
                Camera.main.transform.position.y + offset.y,
                0
            );
        }        
    }

    public void setFreeze(bool isFreezed)
    {
        freeze = isFreezed;
    }
}