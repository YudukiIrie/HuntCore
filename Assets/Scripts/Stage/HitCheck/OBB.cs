using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 各オブジェクトOBB情報
    /// </summary>
    public class OBB
    {
        // 中心座標
        Vector3 _center;
        public Vector3 Center => _center;

        // 分離軸
        Vector3 _axisX;
        Vector3 _axisY;
        Vector3 _axisZ;
        public Vector3 AxisX => _axisX;
        public Vector3 AxisY => _axisY;
        public Vector3 AxisZ => _axisZ;

        // 中心からXYZ平面までの長さ(半径)
        Vector3 _radius;
        public Vector3 Radius => _radius;

        public OBB(Transform transform, Vector3 size)
        {
            _center = transform.position;
            _axisX  = transform.right;
            _axisY  = transform.up;
            _axisZ  = transform.forward;
            _radius = size * 0.5f;
        }

        public void UpdateCenter(Vector3 center)
        {
            _center = center;
        }

        public void UpdateAxes(Transform transform)
        {
            _axisX = transform.right;
            _axisY = transform.up;
            _axisZ = transform.forward;
        }
    }
}
