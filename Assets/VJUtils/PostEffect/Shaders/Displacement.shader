﻿Shader "Hidden/Displacement"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile HORIZONTAL VERTICAL
            #include "UnityCG.cginc"
			#include "Libs/SimplexNoise3D.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D displacementTex;
			float power;

            fixed4 frag (v2f i) : SV_Target
            { 
				 fixed4 col = tex2D(_MainTex, i.uv);
				float2 uv = i.uv;
			#ifdef HORIZONTAL
				float2 dispUV = float2( frac(uv.x +_Time.y), uv.y);
				float disp = tex2D(displacementTex, dispUV).r;
				col.rgb = tex2D(_MainTex,
					uv + float2(disp*power, 0.0)).rgb;

			#elif VERTICAL
				float2 dispUV = float2(uv.x, frac(uv.y + _Time.y));
				float disp = tex2D(displacementTex, dispUV).r;
				col.rgb = tex2D(_MainTex,
					uv + float2(0.0, disp*power)).rgb;
			#endif

				
                return col;
            }
            ENDCG
        }
    }
}
