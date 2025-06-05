using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// �e�I�u�W�F�N�gOBB���
    /// </summary>
    public class OBB
    {
        // ���S���W
        public Vector3 Center { get; private set; }

        // ������
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // ���S����XYZ���ʂ܂ł̒���(���a)
        public Vector3 Radius { get; private set; }

        // �q�b�g�̗L��
        public bool IsHit {  get; private set; }

        public OBB(Transform transform, Vector3 size)
        {
            Center = transform.position;
            AxisX  = transform.right;
            AxisY  = transform.up;
            AxisZ  = transform.forward;
            Radius = size * 0.5f;
            IsHit  = false;
        }

        public void UpdateCenter(Vector3 center)
        {
            Center = center;
        }

        public void UpdateAxes(Transform transform)
        {
            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;
        }

        public void Hit()
        {
            IsHit = true;
        }

        public void ResetHitInfo()
        {
            IsHit = false;
        }
    }
}
