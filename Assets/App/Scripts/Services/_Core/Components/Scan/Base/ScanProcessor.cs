using UnityEngine;

namespace App.Scripts.Services._Core.Components.Scan.Base
{
    public abstract class ScanProcessor
    {
        public abstract bool ProcessScan(Collider collider);
        public virtual void Reset() {}
    }
}