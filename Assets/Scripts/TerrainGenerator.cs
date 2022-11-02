using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Asset;
using ChunkRenderer; 

namespace Terrain {

    static class TerrainGenerator {
        
        public static int _chunkWidth = ChunkRenderer.ChunkRenderer._chunkWidth;
        public static int _chunkHeight = ChunkRenderer.ChunkRenderer._chunkHeight;

        public static BlockType[,,] GenerateTerrain(int xOffset, int zOffset) {
            BlockType[,,] result = new BlockType[_chunkWidth, _chunkHeight, _chunkWidth];

            for (int x = 0; x < _chunkWidth; x++) {
                for  (int z = 0; z < _chunkWidth; z++) {
                    float height = Mathf.PerlinNoise((x + xOffset) * 0.2f, (z + zOffset) * 0.2f) * 5 + 10;

                    Debug.Log(height);
                    for (int y = 0; y < height; y++) {
                        result[x, y, z] = BlockType.Grass;
                    }
                }
            }

            return result;
        }
    }
}