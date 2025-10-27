using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class SequenceService 
{
    private static readonly Dictionary<int, Sequence> activeSequences = new();

    public static Sequence PlaySequence(AnimationSequenceItem[] items, Action onComplete = null)
    {
        if (items == null || items.Length == 0)
        {
            onComplete?.Invoke();
            return null;
        }

        Sequence seq = DOTween.Sequence();
        float currentTime = 0f;

        foreach (var item in items)
        {
            if (item == null)
                continue;

            Tween tween = TweenFactory.CreateTween(item.gameObject, item.animation);
            
            if (tween == null)
                continue;

            if (item.delay > 0)
                tween.SetDelay(item.delay);

            seq.Insert(currentTime, tween);
            currentTime += item.animation.duration; // istersen delay yerine transitionDuration kullan
        }

        // Unique ID ile kayıt
        int id = seq.GetHashCode();
        activeSequences[id] = seq;

        seq.OnComplete(() =>
        {
            onComplete?.Invoke();
            activeSequences.Remove(id);
        });

        seq.Play();
        return seq;
    }

    public static void StopSequence(Sequence sequence)
    {
        if (sequence == null)
            return;

        int id = sequence.GetHashCode();
        if (activeSequences.ContainsKey(id))
            activeSequences.Remove(id);

        if (sequence.IsActive())
            sequence.Kill();
    }

    public static void StopAll()
    {
        foreach (var seq in activeSequences.Values)
        {
            if (seq.IsActive())
                seq.Kill();
        }

        activeSequences.Clear();
    }


    public static Task PlaySequenceAsync(AnimationSequenceItem[] items)
    {
        var tcs = new TaskCompletionSource<bool>();

        PlaySequence(items, () => tcs.SetResult(true));

        return tcs.Task;
    }
}

