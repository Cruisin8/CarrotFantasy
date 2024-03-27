using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class Saver
{
	static public int GetProgress()
	{
		return PlayerPrefs.GetInt(Consts.GameProgress, -1);
	}

	static public void SetProgress(int levelIndex)
	{
		PlayerPrefs.SetInt(Consts.GameProgress, levelIndex);
	}
}