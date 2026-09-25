using UnityEngine;

public class pipamuncul : MonoBehaviour
{
    [SerializeField]
    private GameObject pipaPrefab;
    [SerializeField]
    float starttime, endtime;


void Start()
{

}

void Update()
{
    if(starttime>endtime)
{
    GameObject pipabaru = Instantiate(pipaPrefab);
    pipabaru.transform.position += new Vector3(8, Random.Range(2.5f, -1.5f), 0);
    starttime = 0;
}
    starttime += Time.deltaTime;
}
}