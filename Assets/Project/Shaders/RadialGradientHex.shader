Shader "Custom/RadialGradientHex"
{
    Properties
    {
        _MainTex       ("Sprite Texture", 2D)      = "white" {}
        _Color         ("Tint Color", Color)       = (1,1,1,1)
        _LineColor     ("Hex Line Color", Color)   = (0,0,0,1)
        _CenterColor   ("Center Color", Color)     = (0.8,0.8,0.8,1)
        _EdgeColor     ("Edge Color", Color)       = (1,1,1,1)
        _HexRadius     ("Hexagon Radius", Float)   = 0.2
        _LineThickness ("Line Thickness", Float)   = 0.02
        _InnerRadius   ("Transparent Inner Radius", Float) = 0.1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend One OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f {
                float4 pos      : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _LineColor, _CenterColor, _EdgeColor;
            float _HexRadius, _LineThickness, _InnerRadius;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = v.uv;
                float4 wpos = mul(unity_ObjectToWorld, v.vertex);
                o.worldPos = wpos.xyz;
                return o;
            }

            float HexDist(float2 p, float R)
            {
                float2 q = abs(p);
                float d = max(dot(q, float2(0.5, 0.8660254)), q.x) - R;
                return d;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 texcol = tex2D(_MainTex, i.uv);
                if (texcol.a < 0.001) discard;

                float4 wp4 = float4(i.worldPos, 1.0);
                float3 objp = mul(unity_WorldToObject, wp4).xyz;
                float2 localPos = objp.xy;

                float dist = length(localPos);
                float grad = saturate(1.0 - dist * 2.0);
                float4 baseColor = lerp(_EdgeColor, _CenterColor, grad);

                float innerFade = smoothstep(_InnerRadius, _InnerRadius + 0.01, dist);

                float R = _HexRadius;
                float hx = 1.5 * R;
                float hy = 0.8660254 * 2.0 * R;
                float col = floor(localPos.x / hx + 0.5);
                float yshift = fmod(col, 2.0) * (hy * 0.5);
                float row = floor((localPos.y - yshift) / hy + 0.5);
                float centerX = col * hx;
                float centerY = row * hy + yshift;
                float2 rel = float2(localPos.x - centerX, localPos.y - centerY);

                float d = HexDist(rel, R);
                float hexline = smoothstep(_LineThickness, 0.0, abs(d));

                float4 color = lerp(baseColor, _LineColor, hexline);
                color.rgb *= _Color.rgb;
                color.a = texcol.a * _Color.a * innerFade;
                return color;
            }
            ENDCG
        }
    }
}