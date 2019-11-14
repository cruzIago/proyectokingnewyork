Shader "Custom/Outline"{

	Properties{

		_MainTex ("Main Texture",2D) = "white" {}
		_Color("Main Color", COLOR) = (1,1,1,1)

		_OutlineColor("Outline Color", COLOR) = (1,1,1,1)
		_OutlineTex("Outline Texture" , 2D) = "white"{}
		_OutlineWidth("Outline Width", Range(0.0, 10.0)) = 2
	}

	SubShader{

			Tags { "Queue" = "Transparent" }

		Pass{
			NAME "OUTLINE"
			ZWRITE OFF
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex: POSITION;
				float2 uv: TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXTCOORD0;
			};

			float4 _OutlineColor;
			sampler2D _OutlineTex;
			float _OutlineWidth;

			v2f vert(appdata IN) {

				v2f OUT;

				IN.vertex.xyz *= _OutlineWidth;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;

			}


			fixed4 frag(v2f IN) : SV_Target {

				float4 texColor = tex2D(_OutlineTex, IN.uv);
				return texColor * _OutlineColor;
			}

			ENDCG

		}


		Pass{

			NAME "Base"

			//ZTEST OFF

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex: POSITION;
				float2 uv: TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXTCOORD0;
			};

			float4 _Color;
			sampler2D _MainTex;

			v2f vert(appdata IN) {
				v2f OUT;

				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;

			}


			fixed4 frag(v2f IN) : SV_Target {

				float4 texColor = tex2D(_MainTex, IN.uv);
				return texColor * _Color;
			}

			ENDCG

		}

	}


}