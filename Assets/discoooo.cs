using System.Reflection;
using UnityEngine;

public class discoooo : MonoBehaviour
{
	[SerializeField]
	private KMSelectable _kickButton;
	[SerializeField]
	private KMSelectable _balnButton;

	private KMNeedyModule _module;
	private KMAudio _audio;

	private bool _needyActive;

	private void Start()
	{
		_module = GetComponent<KMNeedyModule>();
		_audio = GetComponent<KMAudio>();

		_module.OnNeedyActivation += OnNeedyActivation;
		_module.OnNeedyDeactivation += OnNeedyDeactivation;
		_module.OnTimerExpired += OnTimerExpired;

		_kickButton.OnInteract += PressKick;
		_balnButton.OnInteract += PressBaln;
	}

	private void OnNeedyActivation()
	{
		_needyActive = true;
		Debug.Log("active");
	}

	private void OnNeedyDeactivation()
	{
		_needyActive = false;
		Debug.Log("inactive");
	}

	private void OnTimerExpired()
	{
		_module.HandleStrike();
		//OnNeedyDeactivation();
	}

	private bool PressKick()
	{
		_kickButton.AddInteractionPunch();
		_audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, _kickButton.transform);

		if (!_needyActive)
			return false;

		_module.HandleStrike();
		_module.HandlePass();
		OnNeedyDeactivation();
		return false;
	}

	private bool PressBaln()
	{
		_balnButton.AddInteractionPunch();
		_audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, _balnButton.transform);

		if (!_needyActive)
			return false;

		_audio.PlaySoundAtTransform("disco", transform);
		_module.HandlePass();
		OnNeedyDeactivation();
		return false;
	}
}
