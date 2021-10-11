using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    private Collider2D collider2D;
    private Rigidbody2D rigidBody;

    public int maxChains = 3;
    private List<GameObject> chains = new List<GameObject>();

    public GameObject searchPrefab, chain;
    private Collider2D closest = null;
    private ColliderDistance2D closestD;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();


        //float dist = float.MaxValue;

        //List<Collider2D> colliders = new List<Collider2D>();
        //ContactFilter2D filter = new ContactFilter2D();
        //int cols = collider2D.OverlapCollider(filter, colliders);
        //foreach (Collider2D collider in colliders)
        //{
        //    if (collider.tag == "Enemy" && collider.Distance(collider2D).distance < dist)
        //    {
        //        closestD = collider.Distance(collider2D);
        //        dist = closestD.distance;
        //        closest = collider;

        //    }
        //}
        GameObject searchRadius = (GameObject)Instantiate(searchPrefab, transform);
        SearchRadiusController search = searchRadius.GetComponent<SearchRadiusController>();
        List<Collider2D> list = new List<Collider2D>();
        list.Add(collider2D);
        closest = search.findClosest(300f, "Enemy", list);
        closestD = search.closestD;
        dist = search.distance;
        Destroy(searchRadius);

        if (closest != null)
        {
            //dist += 300;
            rigidBody.rotation = Mathf.Atan2(closestD.normal.x, closestD.normal.y) * Mathf.Rad2Deg * -1 - 90;
            transform.localScale = new Vector3(dist, .25f, 1);

            float newX = Mathf.Cos(rigidBody.rotation * Mathf.Deg2Rad);
            float newY = Mathf.Sin(rigidBody.rotation * Mathf.Deg2Rad);
            transform.position += new Vector3(dist * newX * .5f, dist * newY * .5f, 0 );

            list.Add(closest);
        }

        for(int i = 0; i < maxChains; i++)
        {
            GameObject chainSearchRadius = (GameObject)Instantiate(searchPrefab, closest.transform);
            SearchRadiusController chainSearch = chainSearchRadius.GetComponent<SearchRadiusController>();
            Collider2D chainClosest = chainSearch.findClosest(300f, "Enemy", list);

            if(chainClosest != null)
            {
                GameObject newChain = (GameObject) Instantiate(chain, closest.transform.position, closest.transform.rotation);
                Rigidbody2D chainRB = newChain.GetComponent<Rigidbody2D>();
                chainRB.rotation = Mathf.Atan2(chainSearch.closestD.normal.x, chainSearch.closestD.normal.y) * Mathf.Rad2Deg * -1 - 90;
                newChain.transform.localScale = new Vector3(chainSearch.distance, .25f, 1);

                float newX = Mathf.Cos(chainRB.rotation * Mathf.Deg2Rad);
                float newY = Mathf.Sin(chainRB.rotation * Mathf.Deg2Rad);
                newChain.transform.position += new Vector3(chainSearch.distance * newX * .5f, chainSearch.distance * newY * .5f, 0);

                chains.Add(newChain);
                list.Add(chainClosest);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}