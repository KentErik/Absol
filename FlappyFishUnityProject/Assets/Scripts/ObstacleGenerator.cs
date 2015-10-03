using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {



    //tile.transform.SetParent(obstacleHolder.transform);
    //public GameObject obstacleHolder = null;



    // prefabs
    public GameObject girderPrefab = null;
	public GameObject buoyPrefab = null;
    public GameObject coinPrefab = null;
    public GameObject obstacleHolder = null;
    public GameObject scorePrefab = null;


    private GameObject player;

    #region old object generation
    //private float rollForNextGirderX = 0.0f;
	//private float rollForNextBuoyX = 0.0f;
    #endregion


    // X coordinate variables
    private float xTripwire = 0.1f;
    //private float xTripwireGrowth = 2.5f;
    
    private const float xOffsetFromPlayer = 10.0f;
    //private float xOffsetRange = 0.25f;

    /*
    // Phase 1 variables
    private const int progressForPhase1 = 0;
    private const float phase1yMinGirder = 4.2f; // 3.9 was toooough
    private const float phase1yMaxGirder = 5.4f;
    private bool phase1and2isBuoy = false;


    // Phase 2 variables
    private const int progressForPhase2 = 4;
    private const float phase2yMinGirder = 4.2f;
    private const float phase2yMaxGirder = 5.4f;


    // Phase 3 variables
    private const int progressForPhase3 = 10;
    private const float phase3gateHeight = 2.0f + 1.5f;


    // Phase 4 variables
    private const int progressForPhase4 = 18;
    private const float phase4gateHeight = 1.5f + 1.5f;


    // Phase 5 variables
    private const int progressForPhase5 = 27;
    private const float phase5gateHeight = 1.0f + 1.5f;


    // coin generation tracking
    private int coinCounting = 0;
    private const int coinInterval = 3;

    private const float coinMinX = 0.5f;
    private const float coinMaxX = 1.5f;
    private const float coinMinY = 1.0f;
    private const float coinMaxY = 3.5f;

    */


    private const float BLOCKING    = 1.5f;
    private const float FREEBIRD    = 2.2f;
    private const float CHOKE       = 0.8f;
    private const float NOOSENING   = 0.04f;
    private const float SPACING     = 2.8f;
    private const float ALLOWANCE   = 0.1f;
    private const float SCROOGING   = 3f;
    private const float NOBILITY    = 2.0f;
    private const float ABASEMENT   = 0.3f;
    private const float GATEFLOOR   = 0.6f;
    private const float GATECEILING = 1.9f;
    private const float BEARMARKET  = 1.0f;
    private const float BULLMARKET  = 3.5f;
    private int banker = 0;





    /*
     * //TODO: Rework xTripwire and xOffsetFromPlayer into NEWNAMES!
     * 
     * 
     * 
     * Gate Height: Should always be 1.5 + some wiggle room. 1.0 for a wiggle is pretty tight! 2.0 is pretty loose!
     * Gate Wiggle (GW) should be     0.8 < GW < 2.2
     * 
     * 
     * Obstacle/Coin Generation refactoring thoughts:
     * 
     * 
     * --META THOUGHTS--
     * 
     * Should create a few buoys, and then just LAUNCH into gates
     * Gates should steadily get more narrow in the amount of height allowed for the player to pass
     * Gates should keep a steady distance between them 
     * Coins should be generated every X amount of gates
     * When Coins are picked up, we should increase the amount of spacing betweeen gates
     * 
     *
     * 
     * BLOCKING - The base amount of height a gate needs for art purposes = 1.5
     * WIGGLE - The amount of space a gate has in addition to its blocking
     * FREEBIRD - The most amount of wiggle we should allow
     * CHOKE - The least amount of wiggle we can allow
     * NOOSENING - the height of the gates that is lost every gate created
     * SPACING - the space between gates
     * 
     * 
     * ALLOWANCE - the amount of spacing we increase and decrease per coin nabbed and missed
     * SCROOGING - the amount of gates that must be generated before a coin is generated
     * NOBILITY - the base amount of spacing a Coin needs from a gate
     * ABASEMENT - the additonal tolerance a Coin will allow itself to get close to a gate
     * 
     * 
     * GATEFLOOR - the lowest height a gate may have
     * GATECEILING - the highest height a gate may have
     * 
     * *********** Constants: *************
     * 
     * BLOCKING  = 1.5
     * FREEBIRD = 2.2
     * CHOKE = 0.8
     * NOOSENING = 0.05
     * SPACING = 2.8
     * SCROOGING = 3 (maybe 4)
     * ALLOWANCE = 0.2
     * NOBILITY = 1.4
     * ABASEMENT = 0.3
     * GATEFLOOR = 0.6 
     * GATECEILING = 1.9
     * 
     * WIGGLE = Mathf.Max( CHOKE, FREEBIRD - (NOOSENING * ScoreManager.instance.Progress));
     * 
     * 
     * 
     * 
     * 
     * 
     */ 




    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // create our initial buoy obstacles
        createBuoy(0f, 2.0f); // buoy for art fixes

        float x;
        x = Random.Range(3.5f, 4.5f);
        createBuoy(x, Random.Range(1f, 2f));
        createScoreObject(x);

        x = Random.Range(7.0f, 8.0f);
        createBuoy(x, Random.Range(1f, 2f));
        createScoreObject(x);

        
        //x = Random.Range(9.0f, 10.0f);
        //createBuoy(x, Random.Range(1f, 2f));
        //createScoreObject(x);

        createCoin(Random.Range(5.0f, 6.5f), Random.Range(1.5f, 2.5f));
        //createCoin(7.0f, 2.5f);
        //createCoin(8.5f, 1.5f);

        banker = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!GameSingleton.instance.IsGameOver)
        {
            #region old object generation
            // is the player far enough along to create our new obstacle?
            /*
            if(player.transform.position.x >= rollForNextGirderX)
            {

                createGirderBasedOnX(rollForNextGirderX + girderCreationXOffset);
				createBuoyBasedOnX(rollForNextBuoyX + buoyCreationXOffset);

                rollForNextGirderX += girderCreationRollFrequency;
				rollForNextBuoyX += buoyCreationRollFrequency;
            }
            */
            #endregion


            #region object generation version 2
            //is the player far enough long to create the next obstacle
            //if (player.transform.position.x >= xTripwire)
            /*
            if (false)
            {
                ++coinCounting;

                // determine what phase of Progression we are for difficulty
                if ( ScoreManager.instance.Progress < progressForPhase1)
                {
                    createPhase1Obstacle();
                    xTripwireGrowth = 3.0f;
                    print("P 1");
                    // *** PHASE 1 IS NEVER USED!!!
                }
                else if(ScoreManager.instance.Progress < progressForPhase2)
                {
                    createPhase2Obstacle();
                    xTripwireGrowth = 3.0f;
                    print("P 2");
                }
                else if (ScoreManager.instance.Progress < progressForPhase3)
                {
                    createPhase3Obstacle();
                    xTripwireGrowth = 3.9f;
                    print("P 3");
                }
                else if (ScoreManager.instance.Progress < progressForPhase4)
                {
                    createPhase4Obstacle();
                    xTripwireGrowth = 2.8f;
                    print("P 4");
                }
                else
                {
                    createPhase5Obstacle();
                    xTripwireGrowth = 2.5f;
                    print("P 5");
                }


                // now increment our checker
                // tripwire growth is changed determined by what phase we're in
                xTripwire += xTripwireGrowth;

                
                if (canCreateCoin())
                {
                    coinCounting = 0;
                }
            }
            */
            #endregion


            /*
             * 
             * BLOCKING - The base amount of height a gate needs for art purposes = 1.5
             * WIGGLE - The amount of space a gate has in addition to its blocking
             * FREEBIRD - The most amount of wiggle we should allow
             * CHOKE - The least amount of wiggle we can allow
             * NOOSENING - the height of the gates that is lost every gate created
             * SPACING - the space between gates
             * 
             * 
             * ALLOWANCE - the amount of spacing we increase and decrease per coin nabbed and missed
             * SCROOGING - the amount of gates that must be generated before a coin is generated
             * NOBILITY - the base amount of spacing a Coin needs from a gate
             * ABASEMENT - the additonal tolerance a Coin will allow itself to get close to a gate
             * 
             * GATEFLOOR - the lowest height a gate may have
             * GATECEILING - the highest height a gate may have
             * 
             */ 



            if( player.transform.position.x >= xTripwire )
            {
                //TODO: refactor xTripwire, xOffset, and referenceXPoint to NEW BETTER NAMES :-D
                // we create a gate, and check to see if we create a coin

                float wiggle = Mathf.Max( CHOKE, FREEBIRD - (NOOSENING * ScoreManager.instance.Progress));
                float gateHeight = BLOCKING + wiggle;

                float referenceXPoint = player.transform.position.x + xOffsetFromPlayer;

                // createGate -> gate X position, buoy Y positoin,  gate Height
                createGate( referenceXPoint, 
                           Random.Range(GATEFLOOR, GATECEILING), 
                           gateHeight);



                // check to see if we create a coin
                banker++;
                if( banker >= SCROOGING )
                {
                    createCoin( referenceXPoint + NOBILITY - (Random.Range(0, ABASEMENT) + ((ALLOWANCE * ScoreManager.instance.ScoreMultiplier)/2.0f)), 
                               Random.Range(BULLMARKET, BEARMARKET));
                    banker = 0;
                }


                // now increase the amount of space before we make our next gate
                xTripwire += SPACING + (ALLOWANCE * ScoreManager.instance.ScoreMultiplier);
            }


        }
	}

    /*
    private GameObject createPhase1Obstacle()
    {
        return createPhase1OR2Obstacle(phase1yMinGirder, phase1yMaxGirder);
    }

    private GameObject createPhase2Obstacle()
    {
        return createPhase1OR2Obstacle(phase2yMinGirder, phase2yMaxGirder);
    }

    private GameObject createPhase1OR2Obstacle(float y1, float y2)
    {
        float x = player.transform.position.x + xOffsetFromPlayer + Random.Range(-xOffsetRange, xOffsetRange);

        // Y Coordinate:
        // buoy is always 1-2; girder is defined by phase1yMinGirder phase1yMaxGirder


        GameObject obj;
        // randomly roll for a girder or a buoy
        if (phase1and2isBuoy)
        {
            // make buoy
            // buoy Y is always 1-2
            obj = createBuoy(x, (Random.Range(1.0f, 2.0f)));
        }
        else
        {
            // make girder
            obj = createGirder(x, Random.Range(y1, y2));
        }
        createScoreObject(x);

        // coin creation
        if (canCreateCoin())
        {
            x = Random.Range(x - coinMinX, x - coinMaxX);
            float y = Random.Range(coinMinY, coinMaxY);
            createCoin(x, y);
        }

        phase1and2isBuoy = !phase1and2isBuoy;
        return obj;
    }

    private GameObject createPhase3Obstacle()
    {
        return createGateWithBuiltInOffsetAndMaybeCoin(phase3gateHeight);
    }


    private GameObject createPhase4Obstacle()
    {
        return createGateWithBuiltInOffsetAndMaybeCoin(phase4gateHeight);
    }


    private GameObject createPhase5Obstacle()
    {
        return createGateWithBuiltInOffsetAndMaybeCoin(phase5gateHeight);
    }
    */

    private GameObject createScoreObject(float x)
    {
        GameObject obj = GameObject.Instantiate(scorePrefab, new Vector3(x, 0, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(obstacleHolder.transform);
        return obj;
    }

    private GameObject createGirder(float x, float y)
    {
        GameObject obj = GameObject.Instantiate(girderPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(obstacleHolder.transform);
        return obj;
    }

    private GameObject createBuoy(float x, float y)
    {
        GameObject obj = GameObject.Instantiate(buoyPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(obstacleHolder.transform);

        Transform buoyBase = obj.transform.FindChild("BuoyBase");
        buoyBase.localPosition = new Vector3(buoyBase.localPosition.x, (0.3f - y), buoyBase.localPosition.z);
        return obj;
    }


    private void createGate(float x, float buoyY, float gateHeight)
    {
        createBuoy(x, buoyY);
        createGirder(x, buoyY + gateHeight);
        createScoreObject(x);
    }
    
    private void createCoin(float x, float y)
    {
        GameObject.Instantiate(coinPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }


    /*
    private GameObject createGateWithBuiltInOffsetAndMaybeCoin(float gateHeight)
    {
        GameObject obj;

        float x = player.transform.position.x + xOffsetFromPlayer + Random.Range(-xOffsetRange, xOffsetRange);

        float buoyY = Random.Range(0.6f, 1.9f);
        float girderY = buoyY + gateHeight;

        obj = createBuoy(x, buoyY);
        obj = createGirder(x, girderY);
        createScoreObject(x);


        // coin creation
        if (canCreateCoin())
        {
            x = Random.Range(x - coinMinX, x - coinMaxX);
            float y = Random.Range(coinMinY, coinMaxY);
            createCoin(x, y);
        }


        return obj;
    }

                    
    private bool canCreateCoin()
    {
        return (coinCounting >= coinInterval);
    }
    */

    #region Old Obstacle Generation Method
    /*
    private float girderCreationXOffset = 12.0f;
    private float girderCreationXRangeTolerance = 1.0f;
    private float girderCreationRollFrequency = 3.0f;

    private float girderMinYPos = 3.9f;
    private float girderMaxYPos = 5.4f;

    private void createGirderBasedOnX(float xPos)
    {
        // Girder creation rules:
        // X can be +/- 1 from the given X
        // Y can be between 3.5 and 6
        float x = Random.Range(xPos - girderCreationXRangeTolerance, xPos + girderCreationXRangeTolerance);
        float y = Random.Range(girderMinYPos, girderMaxYPos);

        GameObject tile = GameObject.Instantiate(girderPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
    }


    private float buoyCreationXOffset = 12.0f;
    private float buoyCreationXRangeTolerance = 1.0f;
    private float buoyCreationRollFrequency = 3.0f;

    private float buoyMinYPos = 1.0f;
    private float buoyMaxYPos = 1.5f;

    private const float waterLevelOffset = 0.3f;

    private void createBuoyBasedOnX(float xPos)
	{
		// Buoy creation rules:

		float x = Random.Range(xPos - buoyCreationXRangeTolerance, xPos + buoyCreationXRangeTolerance);
		float yCreation = Random.Range(buoyMinYPos, buoyMaxYPos);
		
		GameObject tile = GameObject.Instantiate(buoyPrefab, new Vector3(x, yCreation, 0), Quaternion.identity) as GameObject;

        // Adjust buoy's Y position based on the random range. If we picked a higher Y (greater) then we position ourselves lower
        Transform buoyBase = tile.transform.FindChild("BuoyBase");
        buoyBase.localPosition = new Vector3(buoyBase.localPosition.x, waterLevelOffset - yCreation, buoyBase.localPosition.z);

	}
    */   
    #endregion



}
