using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour {

    public GameObject groundTilePrefab = null;
    public GameObject groundHolder = null;

    public GameObject ceilingTilePrefab = null;
    public GameObject ceilingHolder = null;

    public GameObject frontWaterTilePrefab = null;
    public GameObject frontWaterHolder = null;

    private float triggerNewTilingPosition = 0f;

    private Transform player;

	// Use this for initialization
	void Start ()
    {
        // first get our player's transform
        player = GameObject.FindGameObjectWithTag("Player").transform;


        // now create our initial ground and ceiling
        if (groundTilePrefab!=null
            && groundHolder != null
            && ceilingTilePrefab != null
            && ceilingHolder != null)
        {
            for( int i = -3; i < 4; ++i)
            {
                createNewGroundTileAtX(i * 4);
                createNewCelingTileAtX(i * 4);
                createNewFrontWaterTile(i * 4);
            }
        }

        // initialize our triggering X coordinate to start
        triggerNewTilingPosition = 2.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
        // first, see if we need to make a new ground tile and ceiling tile
        // if the bird's position is > the make new tile position, make a tile and update
        if (player.position.x >= triggerNewTilingPosition)
        {
            float flCreationPosition = triggerNewTilingPosition + 2.0f + 12.0f;

            // make our new tiles
            createNewGroundTileAtX(flCreationPosition);
            createNewCelingTileAtX(flCreationPosition);
            createNewFrontWaterTile(flCreationPosition);

            // increment triggerNewGroundPosition
            triggerNewTilingPosition += 4.0f;
        }

	}


    private GameObject createNewGroundTileAtX(float x)
    {
        GameObject tile = GameObject.Instantiate(groundTilePrefab, new Vector3(x, 0, 0), Quaternion.identity) as GameObject;
        tile.transform.SetParent(groundHolder.transform);

        return tile;
    }

    private GameObject createNewFrontWaterTile(float x)
    {
        GameObject frontTile = GameObject.Instantiate(frontWaterTilePrefab, new Vector3(x, 0, 0), Quaternion.identity) as GameObject;
        frontTile.transform.SetParent(frontWaterHolder.transform, false);

        return frontTile;
    }

    private GameObject createNewCelingTileAtX(float x)
    {
        GameObject tile = GameObject.Instantiate(ceilingTilePrefab, new Vector3(x, 4.5f, 0), Quaternion.identity) as GameObject;
        tile.transform.SetParent(ceilingHolder.transform);
        return tile;
    }
}
