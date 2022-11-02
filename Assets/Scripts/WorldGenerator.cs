using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Asset;
using ChunkRenderer; 
using ChunkData;
using Terrain;


namespace WorldGenerator {

    public class WorldGenerator : MonoBehaviour {

        public Dictionary<Vector2Int, ChunkData.ChunkData> chunkDatas = new Dictionary<Vector2Int, ChunkData.ChunkData>();
        public ChunkRenderer.ChunkRenderer ChunkPrefab;
        
        public int _chunkWidth = ChunkRenderer.ChunkRenderer._chunkWidth;
        public int _chunkHeight = ChunkRenderer.ChunkRenderer._chunkHeight;

        void Start() {
            
            for (int x = 0; x < 10; x++) {
                for (int z = 0; z < 10; z++) {

                    ChunkData.ChunkData chunkData = new ChunkData.ChunkData();
                    chunkData.Blocks = Terrain.TerrainGenerator.GenerateTerrain(x * _chunkWidth, z * _chunkWidth);
                    chunkDatas.Add(new Vector2Int(x, z), chunkData);
                }
            }
        }

        // Update is called once per frame
        void Update() {
            
        }
    }
}