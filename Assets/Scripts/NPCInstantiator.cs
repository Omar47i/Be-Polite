using UnityEngine;
using System.Collections;

public class NPCInstantiator : MonoBehaviour {

	public Vector3 rightSpawningPoint = new Vector3(5f, -10f, 0f);
	public Vector3 leftSpawningPoint = new Vector3(-5f, -10f, 0f);

	public GameObject male_prefab;
	public GameObject female_prefab;

	public float incSpeedNPC = 2f;
	public float incSpeedPlayer = 1f;

	private float currTime;

	public float easyWaveNum = 3;
    public float normalWaveNum = 2;
	public float hardWaveNum = 1;

	int NPCTypeRandom;
	int MaleFemaleRandom;

	float spawnTime = 0f;         // used to split the time between each instantiated prefab

	int firstWaveCount = 0;
	int secondWaveCount = 0;
	int thirdWaveCount = 0;
	int fourthWaveCount = 0;

	private AudioSource bgMusic;
	private GameObject instantiatedNPC;

	void Start()
	{
		bgMusic = GetComponent<AudioSource>();
	}

	void Update()
	{
		currTime = Time.time;        // store the time since starting of the game in seconds

		if (currTime <= 12f)         // Do the logic of spawning enemies in the first 12 seconds
		{
			if (spawnTime <= currTime)
			{
				firstWaveCount++;    // to indicate that an NPC is instantiated

                NPCTypeRandom = Random.Range(0, 100);
                MaleFemaleRandom = Random.Range(0, 100);

                if (NPCTypeRandom >= 0 && NPCTypeRandom < 40)          // ratio of generating perverted people the hard difficulty is 40 good to 65 perverted
                {
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                }
                else
                {
                    // The type of spawning NPC type is 50/50
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                    }
                }

				spawnTime = currTime + easyWaveNum;
			}
		}

		else if (currTime <= 24f)        // Do the logic of spawning enemies in the second 12 seconds
		{
			if (spawnTime <= currTime)
			{
				secondWaveCount++;

                NPCTypeRandom = Random.Range(0, 100);
                MaleFemaleRandom = Random.Range(0, 100);

                if (NPCTypeRandom >= 0 && NPCTypeRandom < 50)          // ratio of generating perverted people the hard difficulty is 40 good to 65 perverted
                {
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 60)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                }
                else
                {
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 60)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                    }
                }
				
				spawnTime = currTime + normalWaveNum;

				instantiatedNPC.GetComponent<CitizenMovement>().speed += incSpeedNPC;
			}
		}

		else if (currTime <= 36f)			// Do the logic of spawning enemies in the third 12 seconds
		{
			if (spawnTime <= currTime)
			{
				thirdWaveCount++;

                NPCTypeRandom = Random.Range(0, 100);
                MaleFemaleRandom = Random.Range(0, 100);

                if (NPCTypeRandom >= 0 && NPCTypeRandom < 65)          // ratio of generating perverted people the hard difficulty is 40 good to 65 perverted
                {
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                        instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
                    }
                }
                else
                {
                    if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
                    {
                        instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
                    }
                }
				
				spawnTime = currTime + hardWaveNum;
				
				instantiatedNPC.GetComponent<CitizenMovement>().speed += incSpeedNPC * 2f;

				// increase player speed in the third 12 seocnds
				float speed = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMovement>().speed;
				if (speed < 15f)
					GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMovement>().speed = speed * incSpeedPlayer * 1f;

			}
		}

		else       // current time is greater that 36 (passed all three 12 seconds)
		{
			if (spawnTime <= currTime)
			{
				fourthWaveCount++;

				NPCTypeRandom = Random.Range(0, 100);
				MaleFemaleRandom = Random.Range(0, 100);

				if (NPCTypeRandom >= 0 && NPCTypeRandom <= 70)          // ratio of generating perverted people the hard difficulty is 70 to 30
				{
					// The type of spawning NPC type is 50/50
					if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
					{
						instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
						instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
					}
					else
					{
						instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
						instantiatedNPC.GetComponent<CitizenAIManager>().type = CitizenAIManager.CitizenType.Perverted;
					}
				}
				else
				{
					// The type of spawning NPC type is 50/50
					if (MaleFemaleRandom >= 0 && MaleFemaleRandom < 50)
					{
						instantiatedNPC = Instantiate(male_prefab, rightSpawningPoint, Quaternion.identity) as GameObject;
					}
					else
					{
						instantiatedNPC = Instantiate(female_prefab, leftSpawningPoint, Quaternion.identity) as GameObject;
					}				
				}
				
				spawnTime = currTime + hardWaveNum;
				
				instantiatedNPC.GetComponent<CitizenMovement>().speed += incSpeedNPC * 4f;

				float speed = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMovement>().speed;
				if (speed < 15f)
					GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMovement>().speed = speed * incSpeedPlayer * 2f;
			}
		}
	}
}
