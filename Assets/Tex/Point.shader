// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/SpecularColorVertex"
{
	Properties
	{
		_DiffuseColor("漫反射颜色", Color) = (1, 1, 1, 1)
		_SpecularColor("高光反射颜色", Color) = (1, 1, 1, 1)
		_Gloss("光晕系数",Range(5,256)) = 8
	}
		SubShader
	{


		Pass
		{
		//设定光照模式为向前模式
		Tags {"LightMode" = "ForwardBase"}

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		//加载Cg语言脚本，用来处理光照参数
		//处理光照的Cg库文件（cginc扩展名），目录在Unity的安装目录下Editor/Date/CGIncludes/Linghting.cginc
		#include "UnityCG.cginc" 
		#include "Lighting.cginc"

		//导入高光材质颜色
			fixed4 _DiffuseColor;
			fixed4 _SpecularColor;
			float _Gloss;

			//如果在CCg编程中，顶点或片元着色器接收多个数据用结构体

			//从CPU接收到的数据
			struct c2v
			{
				float4 vertex : POSITION;//模型空间下点的位置
				float2 normal: NORMAL;//当前点模型空间下的法线向量
			};

			//顶点着色器中，需要计算裁剪空间下点的位置和Phong着色器计算出来的兰伯特定律计算后的颜色
			struct v2f
			{
				float4 pos : SV_POSITION;//经过顶点着色器计算后当前点的裁剪空间下的位置
				fixed3 color : COLOR;//经过高光反射公式计算后当前点的颜色
				//（公式：高光光照 = 光源的颜色 + 材质高光反射颜色*MAX（0，标准化后的观察方向向量*标准化后的反射向量）^光晕系数）
				//光反射方向：reflect（入射光方向，当前点的法线方向）
			};

			//高洛德着色（逐顶点光照），光照计算应编写在顶点着色器中
			v2f vert(c2v data)
			{
				//顶点着色器传递给片元着色器的数据结构体声明
				v2f r;

				//将点从模型空间转换到裁剪空间
				r.pos = UnityObjectToClipPos(data.vertex);


				//光照在世界空间发生，需要将数据变换至世界坐标系下运算
				//法线传递的是3x3列矩阵，使用float3x3将齐次矩阵变成3x3矩阵
				fixed3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, data.normal));

				//观察方向（相机的位置点-被渲染点的位置，世界空间下）
				fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, data.vertex).xyz);

				//_WorldSpaceLightPos0是物体到光的方向，取负
				//光线反射的方向
				fixed3 refDir = normalize(reflect(-_WorldSpaceLightPos0, worldNormal));

				//高光公式
				fixed specular = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(viewDir, refDir)), _Gloss);


				//////////////////////////////////////////////////////////////////////
				//漫反射


				//兰伯特定律计算（公式：光源颜色*材质漫反射颜色*MAX(0，标准化后物体表面法线向量*标准化后光源方向向量)）
				fixed3 diffuse = _LightColor0.rgb * _DiffuseColor.rgb * max(0, dot(worldNormal, normalize(_WorldSpaceLightPos0.xyz)));

				r.color = UNITY_LIGHTMODEL_AMBIENT.xyz + specular + diffuse;

				return r;
			}

			fixed4 frag(v2f data) : SV_Target
			{
				return fixed4(data.color, 1.0);
			}
			ENDCG
		}
	}
		FallBack "Diffuse"
}
