Shader "Custom/RadialGradient"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // Требуется для SpriteRenderer
        _InnerColor ("Inner Color", Color) = (1,1,1,0) // Прозрачный центр
        _MiddleColor ("Middle Color", Color) = (0.5,0.5,0.5,1) // Серый
        _OuterColor ("Outer Color", Color) = (1,1,1,1) // Белый край
        _Radius ("Radius", Range(0,1)) = 0.5 // Радиус градиента
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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

            sampler2D _MainTex; // Обязательный параметр для SpriteRenderer
            float4 _InnerColor, _MiddleColor, _OuterColor;
            float _Radius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 2 - 1; // Приводим UV в диапазон (-1,1)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = length(i.uv); // Вычисляем расстояние от центра

                // Создаем градиент
                fixed4 color = lerp(_MiddleColor, _OuterColor, smoothstep(_Radius, 1.0, dist));
                color = lerp(_InnerColor, color, smoothstep(0.0, _Radius, dist));

                // Добавляем текстуру, если она есть
                fixed4 texColor = tex2D(_MainTex, i.uv * 0.5 + 0.5); 
                return color * texColor; // Умножаем градиент на текстуру (или оставляем как есть)
            }
            ENDCG
        }
    }
}