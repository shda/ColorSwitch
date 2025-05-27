using UnityEngine;

namespace ColorSwitch.Scripts
{
    public abstract class PathAbstract : MonoBehaviour
    {
        public abstract Vector3 GetPointOnPath(float t);
        public abstract void RefreshPath();
    }
}