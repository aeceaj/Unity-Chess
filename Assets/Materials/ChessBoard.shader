Shader "Unlit/ChessBoard"
{
    Properties
    {
        [HideInInspector]
        _MainTex ("Texture", 2D) = "white" {}
        [IntRange]
        _Num ("Num", Range(1, 16)) = 8
        _Width("Frame Width", Range(0, 0.5)) = 0
        _Color1 ("Color Light", Color) = (1, 0.8, 0.7, 1)
        _Color2 ("Color Dark", Color) = (0.4, 0.1, 0.1, 1)
        _Color3 ("Color Frame", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            sampler2D _MainTex;
            float _Num;
            float _Width;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 repeatUV = i.uv * _Num;
                float2 coo = floor(repeatUV) / 2;
                float fr = frac(coo.x + coo.y);
                float4 col = fr > 0.25 ? _Color1 : _Color2;
                float2 intr = frac(repeatUV);
                if (intr.x < _Width || intr.y < _Width || intr.x > 1 - _Width || intr.y > 1 - _Width)
                {
                    col = _Color3;
                }
                return col;
            }
            ENDCG
        }
    }
}
