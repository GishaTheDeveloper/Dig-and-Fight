﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapEditor : MonoBehaviour
{
    #region Singleton
    public static TilemapEditor Instance { private set; get; }
    #endregion

    public Tilemap blocksTilemap;

    void Awake()
    {
        Instance = this;
    }

    public void DeleteTile(Vector2 pos)
    {
        Vector3Int intPosition = blocksTilemap.WorldToCell(pos);

        if (!blocksTilemap.HasTile(intPosition))
            return;
        
        blocksTilemap.SetTile(intPosition, null);

        Vector2 worldPosition = (Vector2)blocksTilemap.CellToWorld(intPosition) + Vector2.one / 2f;

        if (GameManager.Instance.IsSetEnemySpawner())
            GameManager.Instance.SpawnEnemySpawner(worldPosition);

        GameManager.Instance.ResetTimer();
        ScoreSystem.Instance.AddScore(50);

        EffectsEmitter.Emit("Small_Dirt_Explosion", worldPosition);
        AudioManager.Instance.PlaySFX("Block_Dig");
    }
}
