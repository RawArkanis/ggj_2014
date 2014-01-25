using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MapMasterScript : MonoBehaviour
{

    public const string MAP_PATH = "Assets/Maps/";

    public GameObject CollisioBlock;
    public GameObject Player;

    private class MapJSON
    {
        public int width { get; set; }
        public int height { get; set; }

        public List<MapLayer> layers { get; set; }
    }

    private class MapLayer
    {
        public int width { get; set; }
        public int height { get; set; }

        public string name { get; set; }
        public string type { get; set; }

        public List<int> data { get; set; }
        public List<MapObject> objects { get; set; }
    }

    private class MapObject
    {
        public float x { get; set; }
        public float y { get; set; }

        public float width { get; set; }
        public float height { get; set; }

        public string name { get; set; }

        public object properties { get; set; }
    }

    public void Load(string fileName)
    {
        using (StreamReader reader = new StreamReader(MAP_PATH + fileName))
        {
            string json = reader.ReadToEnd();

            var map = JsonConvert.DeserializeObject<MapJSON>(json);

            GenerateMap(map);
        }
    }

    private void GenerateMap(MapJSON map)
    {
        foreach (var layer in map.layers)
        {
            // For a TileLayer
            if (layer.type.Equals("tilelayer"))
            {
                if (layer.name.Equals("Collision"))
                {
                    for (int i = 0; i < layer.width * layer.height; i++)
                    {
                        if (layer.data[i] == 1) // If is a Collision Tile
                        {
                            var size = ((BoxCollider2D)CollisioBlock.collider2D).size;

                            float x = (i % layer.width) * size.x;
                            float y = (i / layer.width + 1) * size.y;

                            Instantiate(CollisioBlock, new Vector3(x, y, 0), Quaternion.identity);
                        }
                    }
                }
                else if (layer.name.Equals("Foreground")) // TODO Alterar paraos objetosdo Foreground
                {
                    for (int i = 0; i < layer.width * layer.height; i++)
                    {
                        if (layer.data[i] == 2) // If is a Foreground Tile
                        {
                            var size = ((BoxCollider2D)CollisioBlock.collider2D).size;

                            float x = (i % layer.width) * size.x;
                            float y = (i / layer.width + 1) * size.y;

                            Instantiate(CollisioBlock, new Vector3(x, y, 0.1f), Quaternion.identity);
                        }
                    }
                }
            }
                    // For Objects Layers
            else if (layer.type.Equals("objectgroup"))
            {
                if (layer.name.Equals("Objects"))
                {
                    foreach (var obj in layer.objects)
                    {
                        if (obj.name.Equals("Player"))
                        {
                            var blocksize = ((BoxCollider2D) CollisioBlock.collider2D).size;
                            var size = Player.GetComponent<BoxCollider2D>().size;

                            float x = obj.x / 32 * blocksize.x;
                            float y = obj.y / 32 * blocksize.y;

                            Debug.Log(x);
                            Debug.Log(y);

                            Instantiate(Player, new Vector3(x + 1.25f, y, 0), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    void Start()
    {
        Load("intro.json");
    }
}
