// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DeformSprite" {
	Properties{
		_MainTex("Color Texture (RGB)", 2D) = "white"
	}
		SubShader{ // Unity chooses the subshader that fits the GPU best
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off
		Pass{ // some shaders require multiple passes
		CGPROGRAM // here begins the part in Unity's Cg
		#pragma vertex vert 
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	// 2D Random
	float random(in float2 st) {
		return frac(sin(dot(st.xy, float2(12.9898,78.233))) * 43758.5453123);
	}

	struct v2f {
		float4 pos : SV_POSITION;
		float4 col : TEXCOORD0;
	};

	struct appdata {
		float4 vertexPos : POSITION; // position (in object coordinates, 
									 // i.e. local or model coordinates)
		float4 tangent : TANGENT;
		// vector orthogonal to the surface normal
		float3 normal : NORMAL; // surface normal vector (in object
								// coordinates; usually normalized to unit length)
		float4 texcoord : TEXCOORD0;  // 0th set of texture 
									  // coordinates (a.k.a. “UV”; between 0 and 1) 
		float4 texcoord1 : TEXCOORD1; // 1st set of tex. coors. 
		float4 texcoord2 : TEXCOORD2; // 2nd set of tex. coors. 
		float4 texcoord3 : TEXCOORD3; // 3rd set of tex. coors. 
		float4 color : COLOR; // color (usually constant)
	};

	v2f vert(appdata i) {
		v2f o;
		o.pos = UnityObjectToClipPos(i.vertexPos);
		o.col = i.texcoord1;
		return o;
	}

	float4 frag(v2f i) : COLOR{
		float4 baseTex = tex2D(_MainTex, float2(i.col.x + sin(10.0 * _Time.y + i.col.y) * 0.1 * i.col.y * smoothstep(0.0, 0.3, i.col.y), i.col.y));
		return baseTex;
	}
		ENDCG
	}
	}
}
