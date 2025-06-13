using UnityEngine;
using Stage.Players;
using Stage.Enemies;
using UnityEngine.UI;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB���m�ɂ�铖���蔻��
    /// </summary>
    public class OBBHitChecker : MonoBehaviour
    {
        [Header("�v���C���[�I�u�W�F�N�g")]
        [SerializeField] GameObject _player;

        [Header("�匕�I�u�W�F�N�g")]
        [SerializeField] GameObject _greatSword;

        [Header("�G�I�u�W�F�N�g")]
        [SerializeField] GameObject _enemy;

        [Header("�G���I�u�W�F�N�g")]
        [SerializeField] GameObject _enemyHead;

        [Header("�q�b�g���e�L�X�g")]
        [SerializeField] Text _hitText;

        // �e�I�u�W�F�N�gOBB
        public OBB PlayerOBB { get; private set; }
        public OBB GreatSwordOBB { get; private set; }
        public OBB EnemyOBB {  get; private set; }
        public OBB EnemyHeadOBB {  get; private set; }

        int _hitNum;
        
        void Start()
        {
            // �e�I�u�W�F�N�gOBB���̓o�^�Ǝ��̂̍쐬
            PlayerOBB     = new OBB(_player.transform, PlayerData.Data.PlayerSize);
            GreatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            EnemyOBB      = new OBB(_enemy.transform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize);
            EnemyHeadOBB  = new OBB(_enemyHead.transform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize);
        }

        void Update()
        {
            UpdateOBBInfo(PlayerOBB, _player);
            UpdateOBBInfo(GreatSwordOBB, _greatSword);
            UpdateOBBInfo(EnemyOBB, _enemy);
            UpdateOBBInfo(EnemyHeadOBB, _enemyHead);

            UpdateUI();
        }

        /// <summary>
        /// OBB���̍X�V
        /// </summary>
        /// <param name="obb">�X�V�Ώۂ�OBB</param>
        /// <param name="go">obb�̌��ƂȂ�I�u�W�F�N�g</param>
        void UpdateOBBInfo(OBB obb, GameObject go)
        {
            // ���W�̍X�V
            obb.UpdateCenter(go.transform.position);
            // �������̍X�V
            obb.UpdateAxes(go.transform);
        }

        void UpdateUI()
        {
            _hitText.text = _hitNum.ToString();
        }

        /// <summary>
        /// �w�肵��OBB���m�̓����蔻��
        /// </summary>
        /// <param name="obbA">�\���IOBB</param>
        /// <param name="obbB">�󓮓IOBB</param>
        /// <returns>true:�ڐG�Afalse:��ڐG</returns>
        public bool IsCollideBoxOBB(OBB obbA, OBB obbB)
        {
            // ����ς݂�OBB�͖���
            if (obbB.IsHit) return false;

            // ���S�Ԃ̋����̎擾
            Vector3 distance = obbA.Center - obbB.Center;

            // ���؎���p���������̔�r
            // A�����؎��Ƃ����ꍇ
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) return false;
            // �G�����؎��Ƃ����ꍇ
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) return false;

            // ���������m�̊O�ς�p���������̔�r
            Vector3 cross = Vector3.zero;
            // �����X���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // �����Y���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // �����Z���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;

            // �ڐG�ς�OBB�̏����X�V
            obbB.Hit();
            _hitNum++;

            return true;
        }

        /// <summary>
        /// �w�肵������OBB�ƓGOBB�̋�����r
        /// </summary>
        /// <param name="separating">�w�肵��������</param>
        /// <param name="distance">2�_�Ԃ̋���</param>
        /// <returns></returns>
        bool CompareLengthOBB(OBB obbA, OBB obbB, Vector3 separating, Vector3 distance)
        {
            // ���؎���̕���ƓG�̋���
            // �}�C�i�X�̃x�N�g���ł���\�������邽�ߐ�Βl��
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // ����̌��؎���ɂ����镐��̋����̔��������߂�
            float weaponDist =
                Mathf.Abs(Vector3.Dot(obbA.AxisX, separating)) * obbA.Radius.x +
                Mathf.Abs(Vector3.Dot(obbA.AxisY, separating)) * obbA.Radius.y +
                Mathf.Abs(Vector3.Dot(obbA.AxisZ, separating)) * obbA.Radius.z;

            // ����̌��؎���ɂ�����G�̋����̔��������߂�
            float enemyDist =
                Mathf.Abs(Vector3.Dot(obbB.AxisX, separating)) * obbB.Radius.x +
                Mathf.Abs(Vector3.Dot(obbB.AxisY, separating)) * obbB.Radius.y +
                Mathf.Abs(Vector3.Dot(obbB.AxisZ, separating)) * obbB.Radius.z;

            // ����ƓG�̋����ƁA��L�̋����̍��v���r
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// ����̃��Z�b�g
        /// </summary>
        public void ResetHitInfo()
        {
            GreatSwordOBB.ResetHitInfo();
            EnemyOBB.ResetHitInfo();
        }

        #region �f�o�b�O�p
        void OnDrawGizmos()
        {
            // �����}�g���b�N�X�̕ۑ�
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // �F�̍쐬
            Color red = new Color(1.0f, 0.0f, 0.0f, 0.5f);
            Color white = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            OBB obb = null;
            Vector3 pos, scale = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            // === �GOBB ===
            if (EnemyOBB != null)
            {
                obb = EnemyOBB;

                // �}�g���b�N�X���̎擾
                pos = obb.Center;
                rot = _enemy.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // �F�̕ύX
                Gizmos.color = obb.IsHit ? red : white;

                // �}�g���b�N�X�̍X�V
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // �`��
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === �匕OBB ===
            if (GreatSwordOBB != null)
            {
                obb = GreatSwordOBB;

                // �}�g���b�N�X���̎擾
                pos = obb.Center;
                rot = _greatSword.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // �F�̕ύX
                Gizmos.color = obb.IsHit ? red : white;

                // �}�g���b�N�X�̍X�V
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // �`��
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === �G��OBB ===
            if (EnemyHeadOBB != null)
            {
                obb = EnemyHeadOBB;

                // �}�g���b�N�X���̎擾
                pos = obb.Center;
                rot = _enemyHead.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // �F�̕ύX
                Gizmos.color = obb.IsHit ? red : white;

                // �}�g���b�N�X�̍X�V
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // �`��
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === �v���C���[OBB ===
            if (PlayerOBB != null)
            {
                obb = PlayerOBB;

                // �}�g���b�N�X���̎擾
                pos = obb.Center;
                rot = _player.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // �F�̕ύX
                Gizmos.color = obb.IsHit ? red : white;

                // �}�g���b�N�X�̍X�V
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // �`��
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }
        }
        #endregion
    }
}
