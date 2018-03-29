// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CameraEffects/ScreenSpaceSnow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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
			sampler2D _CameraDepthNormalsTexture;
			float4x4 _CamToWorld;
			sampler2D _SnowTex;
			float _SnowTexScale;
			half4 _SnowColor;
			fixed _BottomThreshold;
			fixed _TopThreshold;

			fixed4 frag (v2f i) : SV_Target
			{
				half3 normal;
				float depth;
				
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.uv), depth, normal);
				normal = mul((float3x3) _CamToWorld, normal);

				float isClose = 1 - max(0, sign(depth - 1));

				half snowAmount = normal.g;
				half scale = (_BottomThreshold + 1 - _TopThreshold) + 1;
				snowAmount = saturate( (snowAmount - _BottomThreshold) * scale * isClose);
				
				float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
				float3 vpos = float3(((i.uv * 2) - 1) / p11_22, -1) * depth * _ProjectionParams.z;
				float4 wpos = mul(_CamToWorld, float4(vpos, 1));
				wpos += float4(_WorldSpaceCameraPos, 0) / _ProjectionParams.z;
				wpos *= _SnowTexScale;

				float height = wpos.y;

				snowAmount = saturate(snowAmount * height);

				half4 snowColor = tex2D(_SnowTex, wpos.xz * _SnowTexScale) * _SnowColor;
				half4 col = tex2D(_MainTex, i.uv); 
				return lerp(col, snowColor, saturate(snowAmount));
			}
			ENDCG
		}
	}
}
