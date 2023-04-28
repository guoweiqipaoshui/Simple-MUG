// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/SpecularColorVertex"
{
	Properties
	{
		_DiffuseColor("��������ɫ", Color) = (1, 1, 1, 1)
		_SpecularColor("�߹ⷴ����ɫ", Color) = (1, 1, 1, 1)
		_Gloss("����ϵ��",Range(5,256)) = 8
	}
		SubShader
	{


		Pass
		{
		//�趨����ģʽΪ��ǰģʽ
		Tags {"LightMode" = "ForwardBase"}

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		//����Cg���Խű�������������ղ���
		//������յ�Cg���ļ���cginc��չ������Ŀ¼��Unity�İ�װĿ¼��Editor/Date/CGIncludes/Linghting.cginc
		#include "UnityCG.cginc" 
		#include "Lighting.cginc"

		//����߹������ɫ
			fixed4 _DiffuseColor;
			fixed4 _SpecularColor;
			float _Gloss;

			//�����CCg����У������ƬԪ��ɫ�����ն�������ýṹ��

			//��CPU���յ�������
			struct c2v
			{
				float4 vertex : POSITION;//ģ�Ϳռ��µ��λ��
				float2 normal: NORMAL;//��ǰ��ģ�Ϳռ��µķ�������
			};

			//������ɫ���У���Ҫ����ü��ռ��µ��λ�ú�Phong��ɫ����������������ض��ɼ�������ɫ
			struct v2f
			{
				float4 pos : SV_POSITION;//����������ɫ�������ǰ��Ĳü��ռ��µ�λ��
				fixed3 color : COLOR;//�����߹ⷴ�乫ʽ�����ǰ�����ɫ
				//����ʽ���߹���� = ��Դ����ɫ + ���ʸ߹ⷴ����ɫ*MAX��0����׼����Ĺ۲췽������*��׼����ķ���������^����ϵ����
				//�ⷴ�䷽��reflect������ⷽ�򣬵�ǰ��ķ��߷���
			};

			//�������ɫ���𶥵���գ������ռ���Ӧ��д�ڶ�����ɫ����
			v2f vert(c2v data)
			{
				//������ɫ�����ݸ�ƬԪ��ɫ�������ݽṹ������
				v2f r;

				//�����ģ�Ϳռ�ת�����ü��ռ�
				r.pos = UnityObjectToClipPos(data.vertex);


				//����������ռ䷢������Ҫ�����ݱ任����������ϵ������
				//���ߴ��ݵ���3x3�о���ʹ��float3x3����ξ�����3x3����
				fixed3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, data.normal));

				//�۲췽�������λ�õ�-����Ⱦ���λ�ã�����ռ��£�
				fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, data.vertex).xyz);

				//_WorldSpaceLightPos0�����嵽��ķ���ȡ��
				//���߷���ķ���
				fixed3 refDir = normalize(reflect(-_WorldSpaceLightPos0, worldNormal));

				//�߹⹫ʽ
				fixed specular = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(viewDir, refDir)), _Gloss);


				//////////////////////////////////////////////////////////////////////
				//������


				//�����ض��ɼ��㣨��ʽ����Դ��ɫ*������������ɫ*MAX(0����׼����������淨������*��׼�����Դ��������)��
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
