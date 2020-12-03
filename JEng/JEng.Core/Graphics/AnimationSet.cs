using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JEng.Core.Graphics
{
    public class AnimationSet
    {
        private Dictionary<string, Animation> _animations;

        public Dictionary<string, Animation> Animations { get => _animations; }

        public AnimationSet(IEnumerable<Animation> animations)
        {
            _animations = new Dictionary<string, Animation>(animations.Count());
            LoadAnimations(animations);
        }

        private void LoadAnimations(IEnumerable<Animation> animations)
        {
            foreach(var animation in animations)
            {
                _animations.Add(animation.Id, animation);
            }
        }
    }
}
