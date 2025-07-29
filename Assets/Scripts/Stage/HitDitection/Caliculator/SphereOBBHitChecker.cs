using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// ���̂�OBB�̓����蔻��v�Z�N���X
    /// </summary>
    public static class SphereOBBHitChecker
    {

        /// <summary>
        /// ���̂�OBB�̓����蔻��
        /// </summary>
        /// <param name="sphere">����Ώۋ���</param>
        /// <param name="obbs">����Ώ�OBB</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        public static bool IntersectSphereOBB(HitSphere sphere, OBB obb)
        {
            // ���ς̍ۂɎg�p����x�N�g���̎擾
            Vector3 direction = sphere.Center - obb.Center;

            // �ŋߐړ_�̌��ƂȂ�_�̎擾
            Vector3 closestPoint = obb.Center;

            // OBB�̊e���ɑ΂��錟��
            int axisNum = 3;
            for (int i = 0; i < axisNum; ++i)
            {
                // ���̐؂�ւ�
                Vector3 axis = Vector3.zero;
                switch (i)
                {
                    case 0: axis = obb.AxisX; break;
                    case 1: axis = obb.AxisY; break;
                    case 2: axis = obb.AxisZ; break;
                }

                // ���ςŏo���e�̒�����
                // OBB�\�ʏ�ɐ�������ۂɎg�p����l�̎擾(�e���ɑΉ����锼�a)
                float extent = obb.Radius[i];

                // OBB���狅�ւ̃x�N�g�����e���Ɏˉe�����������擾
                float projection = Vector3.Dot(direction, axis);

                // ��L�����𐧌�
                projection = Mathf.Clamp(projection, -extent, extent);

                // �e����projection��ςݏd�˂Ă���
                // �ŏI�I�Ȓl���ŋߐړ_�Ƃ���
                closestPoint += axis * projection;
            }

            // ���̂ƍŋߐړ_�̋������擾
            float distance = Vector3.Distance(closestPoint, sphere.Center);

            // ��L�ŋ��߂����������̂̔��a���Z���ꍇ�͐ڐG
            return distance <= sphere.Radius;
        }
    }
}