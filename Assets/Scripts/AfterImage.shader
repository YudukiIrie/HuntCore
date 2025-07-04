Shader "Custom/AfterImage"
{
    Properties
    {
        _MainColor("Main Color", Color) = (0.5, 0.8, 1.0, 0.5)
        _FadeAmount("Fade Amount", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _MainColor;
            float _FadeAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = _MainColor;
                col *= float4(0.4f, 0.2f, 0.2f, _FadeAmount);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
