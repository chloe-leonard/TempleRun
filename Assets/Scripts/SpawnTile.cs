using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject TileToSpawn;
    public GameObject referenceObject;
    public GameObject obstacle;

    public float timeOffset = 0.4f;
    public float RandomValue = 0.8f;

    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 currentDirection, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);
    private float tileSizeZ, tileSizeX, tileSizeY;
    private int tilesGenerated = 0; // ✅ Compteur de tuiles fixes
    private bool readyToGenerate = false; // ✅ Pour s'assurer que la première tuile dynamique part de la 3e fixe

    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
        startTime = Time.time;

        // Récupérer la taille du Tile pour éviter tout espace entre eux
        tileSizeZ = TileToSpawn.GetComponent<Renderer>().bounds.size.z;
        tileSizeX = TileToSpawn.GetComponent<Renderer>().bounds.size.x;
        tileSizeY = TileToSpawn.GetComponent<Renderer>().bounds.size.y;

        currentDirection = mainDirection;
    }

    void Update()
    {
        if (Time.time - startTime > timeOffset)
        {
            // ✅ Générer les 3 premières tuiles fixes
            if (tilesGenerated < 2)
            {
                Vector3 spawnPos = previousTilePosition + mainDirection * tileSizeZ;
                Instantiate(TileToSpawn, spawnPos, Quaternion.identity);
                previousTilePosition = spawnPos;
                tilesGenerated++;

                // ✅ Une fois les 3 tuiles fixes posées, on peut commencer la génération normale
                if (tilesGenerated == 2)
                {
                    readyToGenerate = true;
                }
            }
            else if (readyToGenerate) // ✅ Génération normale après la 3e tuile
            {
                // Décider si on change de direction
                if (Random.value >= RandomValue)
                {
                    Vector3 temp = currentDirection;
                    currentDirection = otherDirection;
                    otherDirection = temp;
                }

                // Génération de la nouvelle tuile après la dernière
                Vector3 spawnPos = previousTilePosition + currentDirection * tileSizeZ;
                Instantiate(TileToSpawn, spawnPos, Quaternion.identity);

                // ✅ Vérifier si on place un obstacle sur cette tuile (50% de chance)
                if (Random.value < 0.5f)
                {
                    GameObject instantiated = Instantiate(obstacle);

                    // Obtenir la taille réelle de l'obstacle
                    float obstacleHeight = instantiated.GetComponent<Renderer>().bounds.size.y;
                    float obstacleWidth = instantiated.GetComponent<Renderer>().bounds.size.x;
                    float obstacleDepth = instantiated.GetComponent<Renderer>().bounds.size.z;

                    // Générer une position aléatoire sur la tuile
                    float randomX = Random.Range(spawnPos.x - tileSizeX / 2, spawnPos.x + tileSizeX / 2);// + (obstacleWidth / 2);
                    float randomZ = Random.Range(spawnPos.z - tileSizeZ / 2, spawnPos.z + tileSizeZ / 2);// + (obstacleDepth / 2);
                    float obstacleY = spawnPos.y + (tileSizeY / 2) + (obstacleHeight / 2);

                    instantiated.transform.position = new Vector3(randomX, obstacleY, randomZ);
                }

                // Mise à jour de la position de la dernière tuile
                previousTilePosition = spawnPos;
            }

            startTime = Time.time;
        }
    }
}
