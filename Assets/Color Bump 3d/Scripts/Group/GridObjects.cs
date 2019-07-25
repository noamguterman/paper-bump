using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class GridObjects : MonoBehaviour
{
	public enum Constraint
	{
		FixedColumnCount,
		FixedRowCount
	}

	public Vector2 spacing;

	public Constraint constraint;

	public int constraintCount = 2;

	public bool adjustLocalScale = true;

	public Vector3 localScale = Vector3.one;

	private void OnValidate()
	{
		constraintCount = Mathf.Max(constraintCount, 1);
		localScale = new Vector3(Mathf.Max(0f, localScale.x), Mathf.Max(0f, localScale.y), Mathf.Max(0f, localScale.z));
		spacing = new Vector2(Mathf.Max(0f, spacing.x), Mathf.Max(0f, spacing.y));
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			int num = 0;
			float num2 = 2.14748365E+09f;
			float num3 = 2.14748365E+09f;
			float num4 = -2.14748365E+09f;
			float num5 = -2.14748365E+09f;
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					int num6;
					int num7;
					if (constraint == Constraint.FixedColumnCount)
					{
						num6 = num % constraintCount;
						num7 = num / constraintCount;
					}
					else
					{
						num6 = num / constraintCount;
						num7 = num % constraintCount;
					}
					transform.localPosition = new Vector3((float)num6 * spacing.x, 0f, spacing.y * (float)num7);
					num2 = Mathf.Min(num2, (float)num6 * spacing.x);
					num3 = Mathf.Min(num3, (float)num7 * spacing.y);
					num4 = Mathf.Max(num4, (float)num6 * spacing.x);
					num5 = Mathf.Max(num5, (float)num7 * spacing.y);
					if (adjustLocalScale)
					{
						transform.localScale = localScale;
					}
					num++;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			Vector3 vector = new Vector3((num2 + num4) / 2f, 0f, (num3 + num5) / 2f);
			IEnumerator enumerator2 = base.transform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					Transform transform2 = (Transform)enumerator2.Current;
					transform2.localPosition -= vector;
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
	}
}
