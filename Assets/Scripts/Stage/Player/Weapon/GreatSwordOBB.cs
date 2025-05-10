using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// ‘åŒ•OBBî•ñ
    /// </summary>
    public class GreatSwordOBB
    {
        // ’†SÀ•W
        public Vector3 Center {  get; private set; }

        // •ª—£²
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // ’†S‚©‚çXYZ•½–Ê‚Ü‚Å‚Ì’·‚³(”¼Œa)
        public Vector3 Radius { get; private set; }

        public GreatSwordOBB(Transform transform, Vector3 size)
        {
            Center = transform.position;
            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;
            Radius = size * 0.5f;
        }
    }
}
