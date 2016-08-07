/*
* Shader used to produce a cutoff effect at a specified y position
* Note: If the sprite draws behind all layers this is due to a bug
* https://support.unity3d.com/hc/en-us/articles/207843283
* and can be resolved by resetting the custom material to the default
* sprite shader and then back to the custom shader
*/
Shader "Sprites/CutoffEffect"
{
	Properties
	{
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Cutoff("Cutoff", Float) = 1.0
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		// No culling or depth
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float3 vpos : TEXCOORD2;
			};

			fixed4 _Color;

			v2f vert (appdata IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = IN.uv;
				OUT.vpos = float3(IN.texcoord1.xy, 0);
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}
			
			sampler2D _MainTex;
			float _Cutoff;

			fixed4 frag (v2f IN) : SV_Target
			{
				// get the color at this point
				fixed4 col = tex2D(_MainTex, IN.uv) * IN.color;

				// if the y position is less than the cutoff point, set full alpha transparency
				if (IN.vpos.y < _Cutoff) {
					col.a = 0;
				}

				col.rgb *= col.a;
					
				return col;
			}
			ENDCG
	}
}
}
