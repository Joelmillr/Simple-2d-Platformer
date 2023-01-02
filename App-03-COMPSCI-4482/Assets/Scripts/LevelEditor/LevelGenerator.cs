using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    public GameObject door;

    Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();   
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
                if(x == map.width - 1 && y == map.height - 1)
                {
                    GenerateDoor();
                }
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            // Pixel is transparent
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                lastPos = new Vector2(x, y);
                Instantiate(colorMapping.prefab, lastPos, Quaternion.identity, transform);
            }
        }
    }

    void GenerateDoor()
    {
        // position equals lastPos + 1
        Vector2 doorPos = new Vector2(lastPos.x, lastPos.y+1.5f);
        Instantiate(door, doorPos, Quaternion.identity, transform);
    }
}
