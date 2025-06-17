using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB可視化用クラス
    /// </summary>
    public class OBBVisualizer : MonoBehaviour
    {
        [Header("OBB可視化用ゲームオブジェクト")]
        [SerializeField] GameObject _obbVisualBox;

        [Header("OBB非接触マテリアル")]
        [SerializeField] Material _obbNoHitImage;

        [Header("OBB接触マテリアル")]
        [SerializeField] Material _obbHitImage;

        [Header("当たり判定クラス")]
        [SerializeField] OBBHitChecker _hitChecker;

        // 各OBB可視化ゲームオブジェクト
        GameObject _playerOBBVisualBox;
        GameObject _swordOBBVisualBox;
        GameObject _enemyOBBVisualBox;
        GameObject _enemyHeadOBBVisualBox;

        // 表示切替関連
        const float SWITCH_DISPLAY_INTERVAL = 0.3f;
        float _switchDisplayTimer;
        bool _isActive = true;

        //void Start()
        //{
        //    // 各OBB可視化ゲームオブジェクトの作成
        //    _playerOBBVisualBox = CreateVisualBox(_hitChecker.PlayerOBB.VisualBox);
        //    _swordOBBVisualBox  = CreateVisualBox(_hitChecker.GreatSwordOBB.VisualBox);
        //    _enemyOBBVisualBox  = CreateVisualBox(_hitChecker.EnemyOBB.VisualBox);
        //    _enemyHeadOBBVisualBox = CreateVisualBox(_hitChecker.EnemyHeadOBB.VisualBox);
        //}

        //void Update()
        //{
        //    SwitchDisplay();
          
        //    // 各OBB可視化ゲームオブジェクト情報の更新
        //    UpdateVisualBoxInfo(_playerOBBVisualBox, _hitChecker.PlayerOBB);
        //    UpdateVisualBoxInfo(_swordOBBVisualBox, _hitChecker.GreatSwordOBB);
        //    UpdateVisualBoxInfo(_enemyOBBVisualBox, _hitChecker.EnemyOBB);
        //    UpdateVisualBoxInfo(_enemyHeadOBBVisualBox, _hitChecker.EnemyHeadOBB);
        //}

        /// <summary>
        /// OBB可視化ゲームオブジェクトの生成
        /// </summary>
        /// <param name="box">生成元OBBVisualbox</param>
        /// <returns>生成済みゲームオブジェクト</returns>
        GameObject CreateVisualBox(OBBVisualBox box)
        {
            // 生成
            var go = Instantiate(_obbVisualBox, box.Position, box.Rotation, transform);
            go.transform.localScale = box.Scale;

            // MeshRendererの登録
            box.SetMeshRenderer(go.GetComponent<MeshRenderer>());

            return go;
        }

        /// <summary>
        /// OBB可視化ゲームオブジェクト情報の更新
        /// </summary>
        /// <param name="go">更新するゲームオブジェクト</param>
        /// <param name="obb">更新元OBB</param>
        void UpdateVisualBoxInfo(GameObject go, OBB obb)
        {
            // Transform情報の更新
            go.transform.position = obb.VisualBox.Position;
            go.transform.rotation = obb.VisualBox.Rotation;

            // マテリアルの更新
            var mr = obb.VisualBox.Renderer;
            Material[] mats = mr.materials;
            mats[0] = obb.IsHit ? _obbHitImage : _obbNoHitImage;
            mr.materials = mats;
        }

        /// <summary>
        /// OBB可視化ゲームオブジェクトの表示切替
        /// </summary>
        void SwitchDisplay()
        {
            _switchDisplayTimer += Time.deltaTime;
            
            if (_switchDisplayTimer >= SWITCH_DISPLAY_INTERVAL)
            {
                if (GameManager.Action.Player.SwitchDisplay.IsPressed())
                {
                    _isActive = !_isActive;
                    _playerOBBVisualBox.SetActive(_isActive);
                    _swordOBBVisualBox.SetActive(_isActive);
                    _enemyOBBVisualBox.SetActive(_isActive);
                    _enemyHeadOBBVisualBox.SetActive(_isActive);

                    _switchDisplayTimer = 0.0f;
                }
            }
        }
    }
}