using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Asset;
using Terrain;
using ChunkData;

namespace ChunkRenderer {

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

    public class ChunkRenderer : MonoBehaviour {

        public const int _chunkWidth = 20;
        public const int _chunkHeight = 128;
        
        public ChunkData.ChunkData chunkData;

        // Vertex coordinated of a new triangle
        private List<Vector3> vertex = new List<Vector3>();
        // Triangle coordinates
        private List<int> triangles = new List<int>();


        public void Start() {

            Mesh chunkMesh = new Mesh();

            // Blocks[0, 0, 0] = BlockType.Grass;
            // Blocks[0, 0, 1] = BlockType.Grass;
            // // Blocks[0, 0, -1] = 1;

            // Blocks[0, 1, 0] = BlockType.Grass;
            // Blocks[0, 2, 0] = BlockType.Grass;

            // Blocks = Terrain.TerrainGenerator.GenerateTerrain(0, 0);
            // chunkData.Blocks = Terrain.TerrainGenerator.GenerateTerrain(100, 100);

            for (int y = 0; y < _chunkHeight; y++) {
                for (int x = 0; x < _chunkWidth; x++) {
                    for (int z = 0; z < _chunkWidth; z++) {
                        GenerateBlock(x, y ,z);
                    }
                }
            }
            
            chunkMesh.vertices = vertex.ToArray();
            chunkMesh.triangles = triangles.ToArray();
            
            chunkMesh.RecalculateBounds();
            chunkMesh.RecalculateNormals();

            GetComponent<MeshFilter>().mesh = chunkMesh;
        } 

        // Update is called once per frame
        public void Update() {
            
        }
    
        
        public BlockType GetBlockAtPosition(Vector3Int blockPosition) {

            if (blockPosition.x >= 0 && blockPosition.x < _chunkWidth &&
                blockPosition.y >= 0 && blockPosition.y < _chunkHeight &&
                blockPosition.z >= 0 && blockPosition.z < _chunkWidth) {
                
                return chunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }
            else {
                return BlockType.Air;
            }
        }

        
        public void GenerateBlock(int x, int y, int z) {

            
            
            Vector3Int blockPosition = new Vector3Int(x, y, z);
            if (GetBlockAtPosition(blockPosition) == 0) return;

            if(GetBlockAtPosition(blockPosition + Vector3Int.right) == 0) GenerateRightSide(blockPosition);
            if(GetBlockAtPosition(blockPosition + Vector3Int.left) == 0) GenerateLeftSide(blockPosition);
            if(GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0) GenerateFrontSide(blockPosition);
            if(GetBlockAtPosition(blockPosition + Vector3Int.back) == 0) GenerateBackSide(blockPosition);
            if(GetBlockAtPosition(blockPosition + Vector3Int.up) == 0) GenerateTopSide(blockPosition);
            if(GetBlockAtPosition(blockPosition + Vector3Int.down) == 0) GenerateBottomSide(blockPosition);
        }


        public void addVertexSquare() {
            triangles.Add(vertex.Count - 4);
            triangles.Add(vertex.Count - 3);
            triangles.Add(vertex.Count - 2);

            triangles.Add(vertex.Count - 3);
            triangles.Add(vertex.Count - 1);
            triangles.Add(vertex.Count - 2);
        }


        public void GenerateRightSide(Vector3Int blockPosition) { 
            vertex.Add(new Vector3(1, 0, 0) + blockPosition);
            vertex.Add(new Vector3(1, 1, 0) + blockPosition);
            vertex.Add(new Vector3(1, 0, 1) + blockPosition);
            vertex.Add(new Vector3(1, 1, 1) + blockPosition);

            addVertexSquare();
        }


        public void GenerateLeftSide(Vector3Int blockPosition) {
            vertex.Add(new Vector3(0, 0, 0) + blockPosition);
            vertex.Add(new Vector3(0, 0, 1) + blockPosition);
            vertex.Add(new Vector3(0, 1, 0) + blockPosition);
            vertex.Add(new Vector3(0, 1, 1) + blockPosition);

            addVertexSquare();
        }


        public void GenerateFrontSide(Vector3Int blockPosition) {
            vertex.Add(new Vector3(0, 0, 1) + blockPosition);
            vertex.Add(new Vector3(1, 0, 1) + blockPosition);
            vertex.Add(new Vector3(0, 1 ,1) + blockPosition);
            vertex.Add(new Vector3(1, 1, 1) + blockPosition);
        

            addVertexSquare();
        }


        public void GenerateBackSide(Vector3Int blockPosition) {
            vertex.Add(new Vector3(0, 0, 0) + blockPosition);
            vertex.Add(new Vector3(0, 1, 0) + blockPosition);
            vertex.Add(new Vector3(1, 0, 0) + blockPosition);
            vertex.Add(new Vector3(1, 1, 0) + blockPosition);

            addVertexSquare();
        }


        public void GenerateTopSide(Vector3Int blockPosition) {
            vertex.Add(new Vector3(0, 1, 0) + blockPosition);
            vertex.Add(new Vector3(0, 1, 1) + blockPosition);
            vertex.Add(new Vector3(1, 1, 0) + blockPosition);
            vertex.Add(new Vector3(1, 1, 1) + blockPosition);

            addVertexSquare();
        }


        public void GenerateBottomSide(Vector3Int blockPosition) {
            vertex.Add(new Vector3(0, 0, 0) + blockPosition);
            vertex.Add(new Vector3(0, 0, 1) + blockPosition);
            vertex.Add(new Vector3(1, 0, 0) + blockPosition);
            vertex.Add(new Vector3(1, 0, 1) + blockPosition);

            addVertexSquare();
        }
    }
}