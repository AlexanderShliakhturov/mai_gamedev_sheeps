using App.Scripts.Services._Core.Components.Scan.Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Attraction.Scan
{
    public class AttractableScanProcessor : ScanProcessor
    {
        private int _minPriority;
        private int _maxPriority;

        public void Set(int minPriority, int maxPriority = -1)
        {
            _minPriority = minPriority;
            _maxPriority = maxPriority;
        }

        public override bool ProcessScan(Collider collider)
        {
            if (!collider.TryGetComponent(out AttractionComponent attraction))
                return false;

            if (attraction.Priority <= _minPriority)
                return false;

            if (attraction.Priority <= _maxPriority) 
                return false;
            
            _maxPriority = attraction.Priority;
            return true;
        }

        public override void Reset()
        {
            _minPriority = 0;
            _maxPriority = 0;
        }
    }
}