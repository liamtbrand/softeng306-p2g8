using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringGridManager : Singleton<HiringGridManager> {

    public List<GameObject> tiles;

    private void Start()
    {
        RefreshTiles();
    }

    public void RefreshTiles() {
        NPCFactory.Instance.ResetPool();
        foreach (GameObject tile in tiles) {
            tile.GetComponent<TileManager>().Refresh();
        }
    }
}
