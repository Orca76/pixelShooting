Shader "Custom/EdgeBlur"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurAmount;

            fixed4 frag(v2f_img i) : SV_Target
            {
                // 画面の中心からの距離を計算
                float2 center = float2(0.5, 0.5);
                float2 offset = abs(i.uv - center);
                float edgeFactor = smoothstep(0.2, 1.0, max(offset.x, offset.y));

                // ブラーの適用
                float2 blurUV = i.uv + (_BlurAmount * edgeFactor * 0.01);
                fixed4 col = tex2D(_MainTex, blurUV);
                return col;
            }
            ENDCG
        }
    }
}