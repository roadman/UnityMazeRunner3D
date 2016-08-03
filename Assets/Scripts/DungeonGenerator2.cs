using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GenerateObject {
	public Vector3 		position;	
	public Quaternion 	rotate;	
	public GameObject	prefab;
}

public enum DungenObjectType {
	None 		= 0,
	WallLeft 	= 1,
	WallRight	= 2,
	WallForward = 3,
	WallBack	= 4,
	Torch1		= 5,
	Torch2		= 6
}

public class SelectObject {
	public int posX;	
	public int posZ;	
	public DungenObjectType type;
	
	public SelectObject(int posX, int posZ, DungenObjectType type) {
		this.posX = posX;
		this.posZ = posZ;
		this.type = type;
	}
}

public class DungeonGenerator2 : MonoBehaviour {

	public GameObject Dungeon;
	public GameObject StartBlock;

	public GameObject PrefabFloor;
	public GameObject PrefabWall;
	public GameObject PrefabTorch;

	public float BlockSizeZ;
	public float BlockSizeX;

	public int DungeonObjectNum;
	public int FloorZNum;	// フロアの縦の数
	public int FloorXNum;	// フロアの橫の数

	private	List<SelectObject> selectObject = new List<SelectObject>();
	private	Vector3 blockBasePos;
	private	float wallDistanceX;
	private	float wallDistanceZ;

	private void SetSelectObject(int posx, int posz) {
		foreach(DungenObjectType type in Enum.GetValues(typeof(DungenObjectType))) {
			selectObject.Add(new SelectObject(posx,posz,type));
		}
	}
	
	// Use this for initialization
	void Start () {
		blockBasePos  = StartBlock.transform.position;
		List<GenerateObject> generateList = new List<GenerateObject>();

		Vector3 basePos;
		wallDistanceX = (BlockSizeX / 2) + (float)0.1;
		wallDistanceZ = (BlockSizeZ / 2) + (float)0.1;
		
		GenerateObject newObj;
		
		for(int posx = 0; posx < FloorXNum; posx++) {
			for(int posz = 0; posz < FloorZNum; posz++) {
				Debug.Log("loop posx:" + posx +", posz:" + posz);
				
				basePos = new Vector3(blockBasePos.x + (BlockSizeX * posx), blockBasePos.y, blockBasePos.z + (BlockSizeZ * posz));
				
				// randamオブジェクト生成のための配列を作成
				SetSelectObject(posx,posz);
				
				// floor生成
				newObj = new GenerateObject();
				newObj.position = basePos;
				newObj.prefab = PrefabFloor;
				newObj.rotate = Quaternion.identity;
				GenerateDungeon(newObj);
				
				newObj.position = basePos + new Vector3(0, 6, 0);
				newObj.prefab = PrefabFloor;
				newObj.rotate = Quaternion.identity;
				GenerateDungeon(newObj);

				// 外周wall生成
				if(posz == 0) {
					newObj = ConstructGenerateObject(posx, posz, DungenObjectType.WallBack);
					generateList.Add(newObj);
				}
				if(posz == (FloorZNum - 1)) {
					newObj = ConstructGenerateObject(posx, posz, DungenObjectType.WallForward);
					generateList.Add(newObj);
				}
				if(posx == 0) {
					newObj = ConstructGenerateObject(posx, posz, DungenObjectType.WallLeft);
					generateList.Add(newObj);
				}
			 	if(posx == (FloorXNum - 1)) {
					newObj = ConstructGenerateObject(posx, posz, DungenObjectType.WallRight);
					generateList.Add(newObj);
				}
				
				//
			}
		}
		
		// ランダムDungeonオブジェクトの設置
		int selectNum;
		SelectObject sel;
		for(int objnum = 0; objnum < DungeonObjectNum; objnum++) {
			selectNum = UnityEngine.Random.Range(0, selectObject.Count);
			sel = selectObject[selectNum];
			newObj = ConstructGenerateObject(sel.posX, sel.posZ, sel.type);
			if(newObj == null) {
				continue;
			}
			generateList.Add(newObj);
			selectObject.RemoveAt(selectNum);
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
	
	private GenerateObject ConstructGenerateObject(int posX, int posZ, DungenObjectType type) {
		Vector3 posObj = new Vector3(blockBasePos.x + (BlockSizeX * posX), blockBasePos.y, blockBasePos.z + (BlockSizeZ * posZ));
		
		GenerateObject newObj = new GenerateObject();
		
		switch(type) {
			case DungenObjectType.WallLeft:
				newObj.position = posObj + new Vector3(-wallDistanceX, 0, 0);
				newObj.prefab = PrefabWall;
				newObj.rotate = Quaternion.identity;
				break;
			case DungenObjectType.WallRight:
				newObj.position = posObj + new Vector3(wallDistanceX, 0, 0);
				newObj.prefab = PrefabWall;
				newObj.rotate = Quaternion.identity;
				break;
			case DungenObjectType.WallForward:
				newObj.position = posObj + new Vector3(0, 0, wallDistanceZ);
				newObj.prefab = PrefabWall;
				newObj.rotate = Quaternion.Euler(0, 90, 0);
				break;
			case DungenObjectType.WallBack:
				newObj.position = posObj + new Vector3(0, 0, -wallDistanceZ);
				newObj.prefab = PrefabWall;
				newObj.rotate = Quaternion.Euler(0, 90, 0);
				break;
			case DungenObjectType.Torch1:
			case DungenObjectType.Torch2:
				newObj.position = posObj;
				newObj.prefab = PrefabTorch;
				newObj.rotate = Quaternion.identity;
				break;
			default:
				return null;
				break;
		}
		
		return newObj;
	}
}
