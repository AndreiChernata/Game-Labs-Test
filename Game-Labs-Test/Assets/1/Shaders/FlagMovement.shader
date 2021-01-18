// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/FlagMovement"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Speed("Speed", Range(0, 30)) = 5
		_Gravity("Gravity", Range(0, 10)) = 1
		_Offset("Offset", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Speed;
			float _Gravity;
			float _Offset;

            v2f vert (appdata v)
            {
                v2f o;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				worldPos.z = lerp(worldPos.z, sin(worldPos.x + _Time * (-_Speed * 10)) + worldPos.z, o.uv.x);
				float yDelta = -o.uv.x * o.uv.x * _Gravity;
				worldPos.y = yDelta + worldPos.y;
				worldPos.x = worldPos.x - o.uv.x * o.uv.x * _Gravity / sqrt(2);
				o.vertex = mul(UNITY_MATRIX_VP, worldPos);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv + float2(_Offset, 0);
                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
