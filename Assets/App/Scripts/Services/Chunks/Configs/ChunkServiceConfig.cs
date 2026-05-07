using System.Collections.Generic;
using App.Scripts.Services._Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Scripts.Services.Chunks.Configs
{
    [CreateAssetMenu(fileName = nameof(ChunkServiceConfig), menuName = "Configs/" + nameof(ChunkServiceConfig))]
    public class ChunkServiceConfig : ServiceConfig
    {
        public float updateChunksStateInterval;
        public List<AssetReferenceGameObject> chunkVariants;
        public int chunkActiveMaxAge;
        public float chunkDimensionLength;
    }
}