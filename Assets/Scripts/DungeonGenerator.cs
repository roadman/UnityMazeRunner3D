using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {
	public GameObject Dungeon;
	public GameObject PrefabBlockA;
	public GameObject StartBlock;
	public float BlockSize;

	// Use this for initialization
	void Start () {
		Vector3 BlockPos  = StartBlock.transform.position;
		Vector3 NewPos;
		for(int cnt = 0; cnt < 100; cnt++) {
			NewPos = new Vector3(BlockPos.x, BlockPos.y, BlockPos.z + BlockSize);
			GameObject NewBlock = (GameObject)Instantiate(PrefabBlockA, NewPos, Quaternion.identity);
			NewBlock.transform.parent = Dungeon.transform;
			BlockPos = NewPos;
		}
	}
}
