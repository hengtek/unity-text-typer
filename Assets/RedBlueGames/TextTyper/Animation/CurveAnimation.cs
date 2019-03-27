﻿namespace RedBlueGames.Tools.TextTyper {
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurveAnimation : TextAnimation 
    {
        [SerializeField]
        [Tooltip("The library of CurvePresets that can be used by this component.")]
        private CurveLibrary curveLibrary;

        [SerializeField]
        [Tooltip("The name (key) of the shake preset this animation should use")]
        private string curvePresetKey;

        private CurvePreset curvePreset;

        private float timeAnimationStarted;

        protected override void OnEnable() 
        {
            this.curvePreset = curveLibrary[curvePresetKey];
            this.timeAnimationStarted = Time.time;
            base.OnEnable();
        }

        protected override void Animate(int characterIndex, out Vector2 translation, out float rotation, out float scale) 
        {
            translation = Vector2.zero;
            rotation = 0f;
            scale = 1f;

            if (this.CharacterShouldAnimate[characterIndex])
            {
                float t = Time.time - this.timeAnimationStarted + (characterIndex * this.curvePreset.timeOffsetPerChar);

                float xPos = this.curvePreset.xPosCurve.Evaluate(t) * this.curvePreset.xPosMultiplier;
                float yPos = this.curvePreset.yPosCurve.Evaluate(t) * this.curvePreset.yPosMultiplier;

                translation = new Vector2(xPos, yPos);

                rotation = this.curvePreset.rotationCurve.Evaluate(t) * this.curvePreset.rotationMultiplier;
                scale += this.curvePreset.scaleCurve.Evaluate(t) * this.curvePreset.scaleMultiplier;
            }
        }
    }
}