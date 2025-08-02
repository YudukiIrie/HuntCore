Shader "Custom/BloodImage"  // Unity上での表示名
{
    Properties  // エディタ上で変更可能なパラメータ
    {
        // 血しぶき全体の色
        _Color("Tint Color", Color) = (1, 0, 0, 1)

        // エッジの柔らかさ(値が大きいほど端がぼやける)
        _Softness("Edge Softness", Range(0, 5)) = 2
    }

    SubShader   // 実際の描画処理
    {
        // 描画順を透明系に設定(透明物として描画するという指示)
        // 本来はOpaque(不透明), Transparent(透明), Overlay(UI)の順に描画
        Tags{"Queue"="Transparent" "RenderType"="Transparent"}

        // αブレンドを指定(SrcAlpha: 自分のアルファ, OneMinusSrcAlpha: 背景の反対)
        // αブレンド: 色を半透明で合成する(例: 赤0.5 + 背景(グレー)0.5 = くすんだ赤色)
        Blend SrcAlpha OneMinusSrcAlpha

        // 両面描画(ビルボードは裏表を持たないので必要)
        // ビルボード: 常にカメラの方を向く四角い板
        Cull off

        // 深度バッファに書き込まない(透過オブジェクト同士のZ競合を防ぐ)
        ZWrite off

        // 光の影響を受けない
        Lighting off

        Pass    // 一回の描画処理
        {
            CGPROGRAM

            // 頂点シェーダーとしてvert関数を使う
            // vert関数はappdata型の頂点データを受け取り、v2f型のデータを返す
            #pragma vertex vert

            // フラグメントシェーダーとしてfrag関数を使う
            // フラグメントシェーダー: ピクセルシェーダーとも言い、ピクセルの最終的な色を決める
            // vert関数から渡された情報(v2f)を使って1ピクセルごとの色を返す
            #pragma fragment frag

            // Propertiesで宣言した変数をHLSL側でも受け取る
            fixed4 _Color;
            float _Softness;

            // Unityが値を渡すためのデータ
            // パーティクルの場合ParticleSystemから値が設定される
            struct appdata
            {
                float4 vertex : POSITION;   // 頂点の位置
                float2 uv : TEXCOORD0;      // uv座標(0～1)
                fixed4 color : COLOR;       // パーティクルが持つ頂点カラー
            };

            // uv: ビルボード上にテクスチャを貼る際の2D座標
            // U: X座標, V: Y座標
            // frag関数でこの値を元に色、透明度を決める

            // vert関数がappdataを変換してfrag関数に渡すためのデータ
            struct v2f
            {
                float4 pos : SV_POSITION;   // 返還後の頂点位置(スクリーン空間上での位置)
                float2 uv : TEXCOORD0;      // uvをフラグメントシェーダーに渡す
                fixed4 color : COLOR;       // 頂点カラーを渡す
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // 頂点位置をカメラ視点に変換
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // uvの中心からの距離を計算(中心: 0.5, 0.5)
                float2 center = float2(0.5, 0.5);
                float2 dist = distance(i.uv, center);

                // 距離に応じて中心は濃く、端は薄く
                float alpha = saturate(1.0 - dist * _Softness);

                fixed4 col = i.color;
                col.a *= alpha;

                // RGBはそのままで、アルファのみ中心→端でグラデーション
                return col;
            }
            ENDCG
        }
    }
}
