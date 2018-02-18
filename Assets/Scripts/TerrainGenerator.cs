using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
	public int height = 5;
	
	public int size_x = 256;
	public int size_y = 256;

	void Start () {
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}

	TerrainData GenerateTerrain(TerrainData terrainData) {
		terrainData.size = new Vector3(size_x, height, size_y);
		terrainData.SetHeights(0,0, GenerateHeights());
		//Debug.Log(terrainData);
		return terrainData;
	}

	float[,] GenerateHeights() {
		float[,] heights = new float[size_x, size_y];
		for (int i = 0; i < size_x; i++) {
			for(int j = 0; j < size_y; j++) {
				heights[i,j] = GetHeight(i, j);
			}
		}

		return heights;
	}

	float GetHeight(int x, int y) {
		float scale = 10f;

		float xCoordinate = (float)x / size_x * scale;
		float yCooridnate = (float)y / size_y * scale;

		// generate height using perlin noise
		return Mathf.PerlinNoise(xCoordinate, yCooridnate);
	}
}
