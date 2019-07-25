Shader "Hidden/Camera-DepthNormalTexture" 
{
Properties
{
	_MainTex ("", 2D) = "white" {}
	_Cutoff ("", Float) = 0.5
	_Color ("", Color) = (1,1,1,1)
}

SubShader 
{
	Tags { "RenderType"="Opaque" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float4 nz : TEXCOORD0;
		};
		v2f vert( appdata_base v ) 
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag(v2f i) : SV_Target 
		{
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="TransparentCutout" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		uniform float4 _MainTex_ST;
		v2f vert( appdata_base v ) 
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		uniform fixed4 _Color;
		fixed4 frag(v2f i) : SV_Target 
		{
			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a*_Color.a - _Cutoff );
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="TreeBark" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "UnityBuiltin3xTreeLibrary.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		v2f vert( appdata_full v ) 
		{
			v2f o;
			TreeVertBark(v);
	
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag( v2f i ) : SV_Target 
		{
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="TreeLeaf" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "UnityBuiltin3xTreeLibrary.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		v2f vert( appdata_full v ) 
		{
			v2f o;
			TreeVertLeaf(v);
	
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag( v2f i ) : SV_Target 
		{
			half alpha = tex2D(_MainTex, i.uv).a;

			clip (alpha - _Cutoff);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="TreeOpaque" "DisableBatching"="True" }

	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float4 nz : TEXCOORD0;
		};
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
		};
		v2f vert( appdata v ) 
		{
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag(v2f i) : SV_Target {
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
} 

SubShader 
{
	Tags { "RenderType"="TreeTransparentCutout" "DisableBatching"="True" }
	Pass 
	{
		Cull Back
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
			float4 texcoord : TEXCOORD0;
		};
		v2f vert( appdata v ) 
		{
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			half alpha = tex2D(_MainTex, i.uv).a;

			clip (alpha - _Cutoff);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}

	Pass 
	{
		Cull Front
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
			float4 texcoord : TEXCOORD0;
		};
		v2f vert( appdata v ) {
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = -COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a - _Cutoff );
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="TreeBillboard" }
	
	Pass 
	{
		Cull Off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"
		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		v2f vert (appdata_tree_billboard v) {
			v2f o;
			TerrainBillboardTree(v.vertex, v.texcoord1.xy, v.texcoord.y);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv.x = v.texcoord.x;
			o.uv.y = v.texcoord.y > 0;
			o.nz.xyz = float3(0,0,1);
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		fixed4 frag(v2f i) : SV_Target 
		{
			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a - 0.001 );
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="GrassBillboard" }
	
	Pass 
	{
		Cull Off		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};

		v2f vert (appdata_full v) 
		{
			v2f o;
			WavingGrassBillboardVert (v);
			o.color = v.color;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			fixed4 texcol = tex2D( _MainTex, i.uv );
			fixed alpha = texcol.a * i.color.a;
			clip( alpha - _Cutoff );
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="Grass" }
	Pass 
	{
		Cull Off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"
		struct v2f 
		{
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};

		v2f vert (appdata_full v) 
		{
			v2f o;
			WavingGrassVert (v);
			o.color = v.color;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			fixed4 texcol = tex2D( _MainTex, i.uv );
			fixed alpha = texcol.a * i.color.a;
			clip( alpha - _Cutoff );
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}



//Curved World////////////////////////////////////////////////////////////////
SubShader 
{
	Tags { "RenderType"="CurvedWorld_Opaque" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		

		#include "../cginc/CurvedWorld_Base.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float4 nz : TEXCOORD0;
		};
		v2f vert( appdata_tan v ) 
		{
			v2f o;

			V_CW_TransformPointAndNormal(v.vertex, v.normal, v.tangent);

			o.pos = UnityObjectToClipPos(v.vertex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag(v2f i) : SV_Target 
		{ 
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TransparentCutout" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"


		#pragma multi_compile _ V_CW_RANGE_FADE

		
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};
		uniform float4 _MainTex_ST;
		v2f vert( appdata_full v ) 
		{
			v2f o;

			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)

			V_CW_TransformPointAndNormal(v.vertex, v.normal, v.tangent);

			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		uniform fixed4 _Color;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)


			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a*_Color.a - _Cutoff - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TreeBark" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"		


		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;
		};
		v2f vert( appdata_full v ) 
		{
			v2f o;
			TreeVertBark(v);
	
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag( v2f i ) : SV_Target 
		{
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TreeLeaf" }
	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"


		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"
		
		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};
		v2f vert( appdata_full v ) 
		{
			v2f o;
			Input input;
			TreeVertLeaf(v, input);

			#ifdef V_CW_RANGE_FADE
				o.worldPosBeforeBend = input.worldPosBeforeBend;
			#endif

	
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag( v2f i ) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)


			half alpha = tex2D(_MainTex, i.uv).a;

			clip (alpha - _Cutoff - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TreeOpaque" "DisableBatching"="True" }

	Pass 
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"


		#include "../cginc/CurvedWorld_Base.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float4 nz : TEXCOORD0;
		};
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
		};
		v2f vert( appdata v ) 
		{
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);

			float4 tangent = float4(cross(v.normal, float3(0,0,1)), -1);
			V_CW_TransformPointAndNormal(v.vertex, v.normal, tangent);

			o.pos = UnityObjectToClipPos(v.vertex);
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		fixed4 frag(v2f i) : SV_Target {
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TreeTransparentCutout" "DisableBatching"="True" }
	Pass 
	{
		Cull Back
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"

		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;


			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
			float4 texcoord : TEXCOORD0;
		};
		v2f vert( appdata v ) 
		{
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);


			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)


			float4 tangent = float4(cross(v.normal, float3(0,0,1)), -1);
			V_CW_TransformPointAndNormal(v.vertex, v.normal, tangent);

			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)


			half alpha = tex2D(_MainTex, i.uv).a;

			clip (alpha - _Cutoff - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}

	Pass 
	{
		Cull Front
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"

		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			fixed4 color : COLOR;
			float4 texcoord : TEXCOORD0;
		};
		v2f vert( appdata v ) 
		{
			v2f o;
			TerrainAnimateTree(v.vertex, v.color.w);


			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)


			float4 tangent = float4(cross(v.normal, float3(0,0,1)), -1);
			V_CW_TransformPointAndNormal(v.vertex, v.normal, tangent);

			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = -COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)

			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a - _Cutoff  - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_TreeBillboard" }
	
	Pass 
	{
		Cull Off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"


		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};
		v2f vert (appdata_tree_billboard v) 
		{
			v2f o;
			
			
			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)

			V_CW_TransformPoint(v.vertex);

			TerrainBillboardTree(v.vertex, v.texcoord1.xy, v.texcoord.y);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv.x = v.texcoord.x;
			o.uv.y = v.texcoord.y > 0;
			o.nz.xyz = float3(0,0,1);
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)

			fixed4 texcol = tex2D( _MainTex, i.uv );
			clip( texcol.a - 0.001 - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_Grass" }
	Pass 
	{
		Cull Off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"


		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			
			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};

		v2f vert (appdata_full v) 
		{
			v2f o;


			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)

			V_CW_TransformPoint(v.vertex);


			WavingGrassVert (v);
			o.color = v.color;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)

			fixed4 texcol = tex2D( _MainTex, i.uv );
			fixed alpha = texcol.a * i.color.a;
			clip( alpha - _Cutoff  - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

SubShader 
{
	Tags { "RenderType"="CurvedWorld_GrassBillboard" }
	
	Pass 
	{
		Cull Off		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"


		#pragma multi_compile _ V_CW_RANGE_FADE
		#include "../cginc/CurvedWorld_Base.cginc"
		#include "../cginc/CurvedWorld_RangeFade.cginc"

		struct v2f 
		{
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
			float2 uv : TEXCOORD0;
			float4 nz : TEXCOORD1;

			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_COORDINATE(2)
		};

		v2f vert (appdata_full v) 
		{
			v2f o;


			//Curved World Distance Fade
			CURVEDWORLD_RANGE_FADE_SETUP(o, v.vertex)

			V_CW_TransformPoint(v.vertex);


			WavingGrassBillboardVert (v);
			o.color = v.color;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			o.nz.xyz = COMPUTE_VIEW_NORMAL;
			o.nz.w = COMPUTE_DEPTH_01;
			return o;
		}
		uniform sampler2D _MainTex;
		uniform fixed _Cutoff;
		fixed4 frag(v2f i) : SV_Target 
		{
			CURVEDWORLD_RANGE_FADE_CALCULATE(i)

			fixed4 texcol = tex2D( _MainTex, i.uv );
			fixed alpha = texcol.a * i.color.a;
			clip( alpha - _Cutoff - CURVEDWORLD_RANGE_FADE_VALUE);
			return EncodeDepthNormal (i.nz.w, i.nz.xyz);
		}
		ENDCG
	}
}

Fallback Off
}
