using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;

    private const float distanceBetweenBlock = 0.6f;
    private int playWidh = 8;
    private int rowSpawned;

    private List<Block> blocksSpawned = new List<Block>();
    
    private void OnEnable()
    {
        for (int i = 0; i < 1; i++)
            SpawnRowOfBlocks();
    }

    public void SpawnRowOfBlocks()
    {
        foreach (var block in blocksSpawned)
        {
            if (block != null)
                block.transform.position += Vector3.down * distanceBetweenBlock; 
        }
        
        for (int i = 0; i < playWidh; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= 30)
            {
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + i + rowSpawned++;

                block.SetHits(hits);
                blocksSpawned.Add(block);
            }
        }

        rowSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBetweenBlock;
        return position;
    }
}
