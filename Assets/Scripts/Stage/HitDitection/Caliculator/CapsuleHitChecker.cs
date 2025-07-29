using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �J�v�Z�������蔻��v�Z�N���X
    /// </summary>
    public static class CapsuleHitChecker
    {
        /// <summary>
        /// �J�v�Z�����m�̓����蔻��
        /// </summary>
        /// <param name="capsuleA">����ΏۃJ�v�Z��</param>
        /// <param name="capsuleB">����ΏۃJ�v�Z��</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        public static bool IntersectCapsules(HitCapsule capsuleA, HitCapsule capsuleB)
        {
            // === �e�J�v�Z�������Ԃ̍ŒZ���������߂� ===
            Vector3 bottomA = capsuleA.BottomPoint;
            Vector3 bottomB = capsuleB.BottomPoint;
            Vector3 topA = capsuleA.TopPoint;
            Vector3 topB = capsuleB.TopPoint;
            float dist = CalcSegmentsDist(bottomA, topA, bottomB, topB);

            // === �ŒZ�����Ɣ��a�Ƃ̔�r ===
            return dist <= capsuleA.Radius + capsuleB.Radius;
        }

        /// <summary>
        /// 2�����Ԃ̋��������߂�
        /// </summary>
        /// <param name="bottomA">�J�v�Z��A�̍ŉ��ʓ_</param>
        /// <param name="topA">�J�v�Z��A�̍ŏ�ʓ_</param>
        /// <param name="bottomB">�J�v�Z��B�̍ŉ��ʓ_</param>
        /// <param name="topB">�J�v�Z��A�̍ŏ�ʓ_</param>
        static float CalcSegmentsDist(Vector3 bottomA, Vector3 topA, Vector3 bottomB, Vector3 topB)
        {
            Vector3 hA = Vector3.zero;  // �J�v�Z��A���̐����̑�
            Vector3 hB = Vector3.zero;  // �J�v�Z��B���̐����̑�
            float   tA = 0.0f;  // �J�v�Z��A�̃x�N�g���W��
            float   tB = 0.0f;  // �J�v�Z��B�̃x�N�g���W��

            // �e�J�v�Z���̐������쐬
            Vector3 segmentA = bottomA - topA;
            Vector3 segmentB = bottomB - topB;

            float dist = 0.0f;
            // === 2�̐��������s�̏ꍇ ===
            if (CheckSegmentsParallel(segmentA, segmentB))
            {
                // �ǂ̓_����L�΂��Ă������ɂȂ邽��
                // �����̌v�Z���ȗ����A�d�Ȃ��Ă��邩�̂ݔ���
                dist = CalcPointSegmentDist(topA, topB, bottomB, out hB, out tB);
                if (0.0f <= tB && tB <= 1.0f)
                    return dist;
            }
            // === 2�̐������˂���̊֌W�̏ꍇ ===
            else
            {
                // ���s�ł͂Ȃ����ߒ����ƒ����̍ŒZ���������߂�K�v������
                // �x�N�g���W������������͂ݏo���Ă��Ȃ���ΐ����Ԃ̍ŒZ�����ł���
                dist = CalcLinesDist(segmentA, segmentB, topA, topB, out hA, out hB, out tA, out tB);
                if (0.0f <= tA && tA <= 1.0f &&
                    0.0f <= tB && tB <= 1.0f)
                    return dist;
            }

            // === �����̑��������̊O���ɂ���ꍇ ===
            // lineA���̑���0�`1�ɃN�����v���Đ������~�낷
            tA = Mathf.Clamp01(tA);
            hA = topA + tA * segmentA;
            dist = CalcPointSegmentDist(hA, topB, bottomB, out hB, out tB);
            if (0.0f <= tB && tB <= 1.0f)
                return dist;

            // lineB���̑���0�`1�ɃN�����v���Đ��������낷
            tB = Mathf.Clamp01(tB);
            hB = topB + tB * segmentB;
            dist = CalcPointSegmentDist(hB, topA, bottomA, out hA, out tA);
            if (0.0f <= tA && tA <= 1.0f)
                return dist;

            // �o���̒[�_�Ƃ̋������ŒZ�����Ƃ���
            tA = Mathf.Clamp01(tA);
            hA = topA + tA * segmentA;
            return (hB - hA).magnitude;
        }

        /// <summary>
        /// 2�̐��������s���m�F
        /// </summary>
        /// <param name="segemntA">�J�v�Z��A�̐���</param>
        /// <param name="segmentB">�J�v�Z��B�̐���</param>
        /// <returns></returns>
        static bool CheckSegmentsParallel(Vector3 segmentA, Vector3 segmentB)
        {
            Vector3 cross = Vector3.Cross(segmentA, segmentB);
            
            // �O�ς̌��ʂ�����Ȃ��������l =
            // 2�̐����ԂɊp�x���t���Ă��Ȃ� = ���s
            return cross.sqrMagnitude < 1e-6f;
        }

        /// <summary>
        /// �_�Ɛ����̍ŒZ���������߂�
        /// </summary>
        /// <param name="point">�_</param>
        /// <param name="segTop">�����ŏ�ʓ_</param>
        /// <param name="segBottom">�����ŉ��ʓ_</param>
        /// <param name="h">�����̐������̑�(�߂�l)</param>
        /// <param name="t">�������̃x�N�g���W��(�߂�l)</param>
        /// <returns>�_�Ɛ����̍ŒZ����</returns>
        public static float CalcPointSegmentDist(
            Vector3 point, Vector3 segTop, Vector3 segBottom,
            out Vector3 h, out float t)
        {
            Vector3 segment = segBottom - segTop;

            // �����̒����A�����̑�(������)�y�ѐ����̃x�N�g���W��t���Z�o
            float dist = CalcPointLineDist(point, segment, segTop, out h, out t);

            if (!CheckAngleSharpness(point, segTop, segBottom))
            {
                // �݊p�̂��ߓ_���ŏ�ʓ_�̊O��
                // �܂�A�ŒZ�����������ł͂Ȃ��_�ƒ[�_�Ƃ̋����ɂȂ�
                h = segTop;
                return (point - segTop).magnitude;
            }
            else if(!CheckAngleSharpness(point, segBottom, segTop))
            {
                h = segBottom;
                return (point - segBottom).magnitude;
            }

            // �ǂ�����s�p�̏ꍇ�͐������������̂܂ܕԋp
            return dist;
        }

        /// <summary>
        /// �_�ƒ����̍ŒZ���������߂�
        /// </summary>
        /// <param name="point">�_</param>
        /// <param name="line">����</param>
        /// <param name="lineP">������̂���_</param>
        /// <param name="h">�����ւ̐����̑�(�߂�l)</param>
        /// <param name="t">�����̃x�N�g���W��(�߂�l)</param>
        /// <returns>�_�ƒ����̍ŒZ����</returns>
        static float CalcPointLineDist(
            Vector3 point, Vector3 line, Vector3 lineP, 
            out Vector3 h, out float t)
        {
            // �ŒZ�����ł���point����line�ւ̐��������߂邽�߂ɂ́A
            // ���ł���h�A�܂�h�����߂邽�߂�t(line�̃x�N�g���W��)���K�v�B
            // t�����߂邽�߂̎�(�ȉ�point��p1, line��v, lineP��p2�Ƃ���)
            // 0 = Dot(v, (p2 + tv) - p1)   v�Ɛ����͐����ł��邽�ߒl��0
            // 0 = Dot(v, p2) + tDot(v, v) - Dot(v, p1)
            // t = Dot(v, p1) - Dot(v, p2) / Dot(v, v)
            // ���̎�����ȉ��̌v�Z�ɑ���
            t = Vector3.Dot(line, point - lineP) / line.sqrMagnitude;
            h = lineP + (t * line);
            return (h - point).magnitude;
        }

        /// <summary>
        /// 3�_�������p�x���s�p���݊p���̊m�F
        /// </summary>
        /// <param name="p1">�_1</param>
        /// <param name="p2">�_2</param>
        /// <param name="p3">�_3</param>
        /// <returns>true:�s�p, false:�݊p</returns>
        static bool CheckAngleSharpness(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float dot = Vector3.Dot(p1 - p2, p3 - p2);

            // ���ό��ʂŊp�̑傫����������
            // 0�ȏ�: �s�p(90�x�܂�), 0����: �݊p
            if (dot >= 0.0f)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 2�̒����Ԃ̍ŒZ���������߂�
        /// </summary>
        /// <param name="lineA">����A</param>
        /// <param name="lineB">����B</param>
        /// <param name="pointA">����A�̂����_</param>
        /// <param name="pointB">����B�̂����_</param>
        /// <param name="hA">�����̒���A���̑�</param>
        /// <param name="hB">�����̒���B���̑�</param>
        /// <param name="tA">����A�̃x�N�g���W��</param>
        /// <param name="tB">����B�̃x�N�g���W��</param>
        /// <returns>2�̒����Ԃ̍ŒZ����</returns>
        static float CalcLinesDist(
            Vector3 lineA, Vector3 lineB, Vector3 pointA, Vector3 pointB,
            out Vector3 hA, out Vector3 hB, out float tA, out float tB)
        {
            float dLALB = Vector3.Dot(lineA, lineB);
            float dLALA = lineA.sqrMagnitude;
            float dLBLB = lineB.sqrMagnitude;
            Vector3 pBpA = pointA - pointB;

            // lineA��lineB�o�����݂��ɐ����������Ă���Ɖ��肵���Ƃ��̏�Ԃ�tA�����߂�B
            // tA�����܂��tB, hA, hB�����܂邽��hAhB(2�����Ԃ̍ŒZ����)�����܂�B
            // tA�����߂邽�߂̎�(tB��CalcPointLineDist()�̎��ŋ��܂��Ă���)
            // �ȉ��AlineA��v1, lineB��v2, pointA��p1, pointB��p2, tA��t1, tB��t2�Ƃ���
            // 0 = Dot(v1, hA - hB)
            //   = Dot(v1, p1 + t1 * v1 - p2 - t2 * v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) - t2Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) - Dot(v2, p1 + t1v1 - p2) / Dot(v2, v2) *  Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) + {- Dot(v2, p1 - p2) - t1Dot(v2, v1)} / Dot(v2, v2) * Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1{Dot(v1, v1) - (Dot(v2, v1) * Dot(v1, v2)) / Dot(v2, v2)} - {(Dot(v2, p1 - p2) * Dot(v1, v2)) / Dot(v2, v2)}
            // t1 = Dot(v2, p1 - p2) * Dot(v1, v2) - Dot(v1, p1 - p2) * Dot(v2, v2) / Dot(v1, v1) * Dot(v2, v2) - Dot(v2, v1) * Dot(v1, v2)
            // �ȉ��̎��ɑ���
            tA = (dLALB * Vector3.Dot(lineB, pBpA) - dLBLB * Vector3.Dot(lineA, pBpA)) / (dLALA * dLBLB - dLALB * dLALB);
            hA = pointA + (tA * lineA);
            tB = Vector3.Dot(lineB, pointA - pointB) / dLBLB;
            hB = pointB + (tB * lineB);

            return (hB - hA).magnitude;
        }
    }
}