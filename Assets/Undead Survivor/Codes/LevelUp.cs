using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour
{
	private RectTransform rect;
	private Item[] items;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		items = GetComponentsInChildren<Item>(true);
	}

	public void Show()
	{
		Next();
		rect.localScale = Vector3.one;
		GameManager.instance.Stop();
		AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
		AudioManager.instance.EffectBgm(true);
	}

	public void Hide()
	{
		rect.localScale = Vector3.zero;
		GameManager.instance.Resume();
		AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
		AudioManager.instance.EffectBgm(false);
	}

	public void Select(int index)
	{
		items[index].OnClick();
	}

	private void Next()
	{
		// 1. Disable all items
		foreach (Item item in items)
		{
			item.gameObject.SetActive(false);
		}
		
		// 2. Active 3 items randomly among all stuffs
		int[] rand = Enumerable.Range(0, items.Length).ToArray();

		#region Legacy
		// while (true)
		// {
		// 	rand[0] = Random.Range(0, items.Length);
		// 	rand[1] = Random.Range(0, items.Length);
		// 	rand[2] = Random.Range(0, items.Length);
		//
		// 	if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2])
		// 	{
		// 		break;
		// 	}
		// }
		#endregion
		
		for (int index = 0; index < rand.Length; index++)
		{
			int j = Random.Range(index, rand.Length);
			(rand[index], rand[j]) = (rand[j], rand[index]);
		}

		for (int index = 0; index < 3; index++)
		{
			Item randItem = items[rand[index]];
			
			// 3. Replace the item if it's max level
			if (randItem.level == randItem.data.damages.Length)
			{
				items.Last().gameObject.SetActive(true);
			}
			else
			{
				randItem.gameObject.SetActive(true);
			}
		}
		

	}
}
