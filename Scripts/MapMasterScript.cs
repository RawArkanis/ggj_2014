using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MapMasterScript : MonoBehaviour
{

    public const string MAP_PATH = "/Maps/";

    public GameObject CollisionBlock;
    public GameObject Player;
	public GameObject End;
	public GameObject ForeGroundSprite;
	public GameObject CrateBig;
	public GameObject CrateSmall;
	public GameObject Switch;
	public GameObject TrapDoor;
	public GameObject Elevator;

	public Sprite[] allSprites;

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
		public string type { get; set; }

		public string extra { get; set; }

        public object properties { get; set; }
	}
	
	private class SwitchRelationObject
	{
		public GameObject objSwitch { get; set; }
		public string[] targets { get; set; }
	}

    public void Load(string fileName)
    {
        using (StreamReader reader = new StreamReader(Application.dataPath + MAP_PATH + fileName))
        {
            string json = reader.ReadToEnd();
			Debug.Log ("json: " + json);
            var map = JsonConvert.DeserializeObject<MapJSON>(json);

            GenerateMap(map);
        }
    }

    private void GenerateMap(MapJSON map)
	{
		var blocksize = ((BoxCollider2D) CollisionBlock.collider2D).size;
		float x;
		float y;
        foreach (var layer in map.layers)
        {
			if (layer.type.Equals("tilelayer"))// For a TileLayer
            {
                if (layer.name.Equals("Collision"))
                {
                    for (int i = 0; i < layer.width * layer.height; i++)
                    {
                        if (layer.data[i] == 1) // If is a Collision Tile
                        {
							x = (i % layer.width) * blocksize.x;
							y = (i / layer.width) * blocksize.y;

                            Instantiate(CollisionBlock, new Vector3(x, -y, 0), Quaternion.identity);
                        }
                    }
                }
                else if (layer.name.Equals("Foreground"))
                {
					GameObject fgSprite;
					SpriteRenderer fgRenderer;

                    for (int i = 0; i < layer.width * layer.height; i++)
                    {
                        if (layer.data[i] >= 2) // If is a Foreground Tile
                        {
							x = (i % layer.width) * blocksize.x;
							y = (i / layer.width) * blocksize.y;

							fgSprite = (GameObject) Instantiate(ForeGroundSprite, new Vector3(x, -y, -3f), Quaternion.identity);
							fgRenderer = fgSprite.GetComponent<SpriteRenderer>();
							fgRenderer.sprite = allSprites[layer.data[i]-1];
                        }
                    }
                }
            }
			else if (layer.type.Equals("objectgroup"))// For Objects Layers
            {
                if (layer.name.Equals("Objects"))
				{
					SwitchRelationObject[] switches = new SwitchRelationObject[10];
					GameObject switchObj;

					int switchCount = 0;

                    foreach (var obj in layer.objects)
                    {
                        switch(obj.type)
                        {
							case "player":
	                            x = obj.x / 32 * blocksize.x;
                            	y = obj.y / 32 * blocksize.y;
								
                            	Instantiate(Player, new Vector3(x + 1.25f, -y-0.625f, 0), Quaternion.identity);
								break;
							case "end":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								Instantiate(End, new Vector3(x, -y, 0), Quaternion.identity);
								break;
							case "crate big":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								Instantiate(CrateBig, new Vector3(x, -y, 0), Quaternion.identity);
								break;
							case "crate small":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								Instantiate(CrateSmall, new Vector3(x, -y, 0), Quaternion.identity);
							break;
							case "button":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								switchObj = (GameObject) Instantiate(Switch, new Vector3(x, -y, 0), Quaternion.identity);
								switchObj.name = obj.name;
								switches[switchCount] = new SwitchRelationObject();
								switches[switchCount].objSwitch = switchObj;
								switches[switchCount].targets = obj.name.Split(',');
								switchCount++;
							break;
							case "alcapao":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								switchObj = (GameObject) Instantiate(TrapDoor, new Vector3(x, -y, 0), Quaternion.identity);
								switchObj.name = obj.name;
								break;
							case "elevador":
								x = obj.x / 32 * blocksize.x;
								y = obj.y / 32 * blocksize.y;
								
								switchObj = (GameObject) Instantiate(Elevator, new Vector3(x, -y, 0), Quaternion.identity);
								switchObj.name = obj.name;
								//Debug.Log("From Y: " + (obj.y / 32) + ", to Y: " + (int.Parse(obj.extra) / 32));
								Elevator ele = switchObj.GetComponent<Elevator>();
								ele.setMaxY((int)(obj.y / 32) - (int)(int.Parse(obj.extra) / 32));
								break;
							default:
								Debug.Log("Ainda nao tem implementado objeto do tipo: " + obj.type);
								break;
                        }
                    }
					
					foreach (SwitchRelationObject g in switches)
					{
						if(g == null) continue;

						int switchTargetIndex = 0;
//						int targetNameIndex = 1;
						
						for (int i = 1; i < g.targets.Length; i++)
						{
							Debug.Log ("Relations for switch.name: " + g.objSwitch.name + ": " + g.targets[i]);
							Switch _s = g.objSwitch.GetComponent<Switch>();
							_s.targets[switchTargetIndex] = GameObject.Find(g.targets[i]);
							switchTargetIndex++;
//							targetNameIndex++;
						}
					}
                }
            }
        }
    }
}
