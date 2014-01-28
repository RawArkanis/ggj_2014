using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
	public void setMaxY(int maxY)
	{
		Keyframe key1 = new Keyframe(0,0);
		Keyframe key2 = new Keyframe(0.5f * maxY,(maxY*2.5f));
		AnimationCurve curve = new AnimationCurve(key1,key2);
		AnimationClip clip = new AnimationClip();
		clip.SetCurve("offset/base",typeof(Transform), "localPosition.y",curve);
		clip.wrapMode = WrapMode.PingPong;
		animation.AddClip(clip, "Elevator");
		animation.clip = animation.GetClip("Elevator");
	}
}