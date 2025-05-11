Shader"Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;
                float2 pixelSize = float2(_OutlineThickness / _ScreenParams.x, _OutlineThickness / _ScreenParams.y);

                float outline = 0.0;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        float2 offset = float2(x, y) * pixelSize;
                        outline += tex2D(_MainTex, i.uv + offset).a;
                    }
                }

                float4 texColor = tex2D(_MainTex, i.uv) * _Color;

                if (texColor.a > 0.01)
                {
                    return texColor;
                }
                else if (outline > 0.01)
                {
                    float4 oColor = _OutlineColor;
                    oColor.a *= saturate(outline);
                    return oColor;
                }
                else
                {
                    return float4(0,0,0,0);
                }
            }
            ENDCG
        }
    }
}
