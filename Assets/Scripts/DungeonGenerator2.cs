using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateObject {
	public Vector3 		position;	
	public Quaternion 	rotate;	
	public GameObject	prefab;
}

public class DungeonGenerator2 : MonoBehaviour {

	enum FloorType {
		None,
		WallLeft,
		WallRight,
		WallUp,
		WallDown
	}
	
	public GameObject Dungeon;
	public GameObject StartBlock;

	public GameObject PrefabFloor;
	public GameObject PrefabWall;

	public float BlockSizeZ;
	public float BlockSizeX;

	public int FloorZNum;	// フロアの縦の数
	public int FloorXNum;	// フロアの橫の数


	// Use this for initialization
	void Start () {
		Vector3 blockPos  = StartBlock.transform.position;
		List<GenerateObject> generateList = new List<GenerateObject>();

		Vector3 basePos;
		float wallDistanceZ = (BlockSizeZ / 2) + (float)0.1;
		float wallDistanceX  = (BlockSizeX / 2) + (float)0.1;
		
		GenerateObject newObj;

		for(int vcnt = 0; vcnt < FloorZNum; vcnt++) {
			for(int hcnt = 0; hcnt < FloorXNum; hcnt++) {
				Debug.Log("loop hcnt:" + hcnt +", vcnt:" + vcnt);
				basePos = new Vector3(blockPos.x + (BlockSizeX * hcnt), blockPos.y, blockPos.z + (BlockSizeZ * vcnt));
				
				// floor生成
				newObj = new GenerateObject();
				newObj.position = basePos;
				newObj.prefab = PrefabFloor;
				newObj.rotate = Quaternion.identity;
				//generateList.Add(newObj);
				GenerateDungeon(newObj);

				// 外周wall生成
				if(vcnt == 0) {
					newObj = new GenerateObject();
					newObj.position = basePos + new Vector3(0, 0, -wallDistanceX);
					newObj.prefab = PrefabWall;
					newObj.rotate = Quaternion.Euler(0, 90, 0);
					generateList.Add(newObj);
				}
				if(vcnt == (FloorZNum - 1)) {
					newObj.position = basePos + new Vector3(0, 0, wallDistanceX);
					newObj.prefab = PrefabWall;
					newObj.rotate = Quaternion.Euler(0, 90, 0);
					generateList.Add(newObj);
				}
				if(hcnt == 0) {
					newObj = new GenerateObject();
					newObj.position = basePos + new Vector3(-wallDistanceZ, 0, 0);
					newObj.prefab = PrefabWall;
					newObj.rotate = Quaternion.identity;
					generateList.Add(newObj);
				}
			 	if(hcnt == (FloorXNum - 1)) {
					newObj = new GenerateObject();
					newObj.position = basePos + new Vector3(wallDistanceZ, 0, 0);
					newObj.prefab = PrefabWall;
					newObj.rotate = Quaternion.identity;
					generateList.Add(newObj);
				}
				
				//
			}
		}

		foreach(GenerateObject obj in generateList) {
			GenerateDungeon(obj);
		}
	}

	private GameObject GenerateDungeon(GenerateObject obj) {
		Debug.Log("generateDungeon. " + obj.prefab.ToString());
		GameObject block = (GameObject)Instantiate(obj.prefab, obj.position, obj.rotate);
		block.transform.parent = Dungeon.transform;
		return block;
	}
}
