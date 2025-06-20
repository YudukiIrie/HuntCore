using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// �e�I�u�W�F�N�gOBB���
    /// </summary>
    public class OBB
    {
        // OBB�̎��
        public enum OBBType
        {
            Body,   // �̂̕���
            Weapon, // ����܂��͍U���̈ꕔ
            Guard,  // �h����
            None    // �Ȃ�
        }
        public OBBType Type { get; private set; }

        // �q�b�g���
        public struct HitInformation
        {
            public bool isHit;          // �q�b�g�������ǂ���
            public OBBType targetType;  // �ڐG�����OBBType

            public HitInformation(bool isHit, OBBType targetType)
            {
                this.isHit = isHit;
                this.targetType = targetType;
            }

            /// <summary>
            /// �q�b�g���̎擾
            /// </summary>
            /// <param name="type">�����OBBType</param>
            public void SetHitInfo(OBBType type)
            {
                isHit = true;
                targetType = type;
            }

            /// <summary>
            /// �q�b�g���̃��Z�b�g
            /// </summary>
            public void ResetHitInfo()
            {
                isHit = false;
                targetType = OBBType.None;
            }
        }
        public HitInformation HitInfo;

        // ���S���W
        public Vector3 Center { get; private set; }

        // ������
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // ���S����XYZ���ʂ܂ł̒���(���a)
        public Vector3 Radius { get; private set; }

        // �����p�I�u�W�F�N�g
        public OBBVisualBox VisualBox { get; private set; }

        public OBB(Transform transform, Vector3 size, OBBType type)
        {
            Center = transform.position;
            AxisX  = transform.right;
            AxisY  = transform.up;
            AxisZ  = transform.forward;
            Radius = size * 0.5f;
            Type   = type;
            HitInfo = new HitInformation(false, OBBType.None);

            VisualBox = new OBBVisualBox(Center, transform.rotation, Radius);
        }

        public void UpdateInfo(Transform transform)
        {
            Center = transform.position;

            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;

            VisualBox.UpdateInfo(transform, HitInfo.isHit);
        }

        public void SetOBBType(OBBType type)
        {
            Type = type;
        }

        /// <summary>
        /// �q�b�g���̎擾
        /// </summary>
        /// <param name="other">����OBB</param>
        public void SetHitInfo(OBB other)
        {
            HitInformation info = HitInfo;
            info.SetHitInfo(other.Type);
            HitInfo = info;
        }

        /// <summary>
        /// �q�b�g���̃��Z�b�g
        /// </summary>
        public void ResetHitInfo()
        {
            HitInformation info = HitInfo;
            info.ResetHitInfo();
            HitInfo = info;
        }
    }
}
