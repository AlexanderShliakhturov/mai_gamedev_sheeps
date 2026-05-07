using App.Scripts.Services.Entities.Components.Base;
using UnityEngine;

namespace App.Scripts.Services.Chunks.Components
{
    public class DeadUnderFloor : MonoBehaviour
    {
        private Collider _chunkCollider;

        public void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Entity entity))
                return;

            if (!_chunkCollider)
                _chunkCollider = GetComponentInParent<ChunkComponent>().GetComponent<Collider>();

            entity.transform.position =
                _chunkCollider.ClosestPoint(new Vector3(entity.transform.position.x, 0, entity.transform.position.z)) +
                Vector3.up;

            entity.Reset();
        }
    }
}