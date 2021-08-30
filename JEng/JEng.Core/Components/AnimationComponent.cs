using JEng.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEng.Core.Components
{
    public class AnimationComponent
    {
        private AnimationSet _animations;
        private string _currentAnimation;


        public float Timer { get; set; }

        public float Layer { get; set; }

        public AnimationComponent(AnimationSet animations)
        {
            _animations = animations;
            _currentAnimation = animations.Animations.First().Key;
            Timer = 0;
        }

        public Animation CurrentAnimation
        {
            get => _animations.Animations[_currentAnimation];
        }

        public Animation ChangeAnimation(string key)
        {
            _currentAnimation = key;
            return CurrentAnimation;
        }
    }
}
