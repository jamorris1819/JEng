using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JEng.Content.Pipeline.Data.Animations.Processed;
using JEng.Content.Pipeline.Data.Textures.Processed;
using JEng.Core.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace JEng.Content.Pipeline.Readers
{
    public class AnimationReader : ContentTypeReader<AnimationSet>
    {
        GraphicsDeviceManager _graphics;

        protected override AnimationSet Read(ContentReader input, AnimationSet existingInstance)
        {
            _graphics = (GraphicsDeviceManager)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceManager));

            string id = input.ReadString();
            string category = input.ReadString();
            var animations = input.ReadObject<ProcessedAnimationData[]>();

            var set = new AnimationSet(animations.Select(Convert));

            if(set.Animations.ContainsKey("WalkDown"))
            {
                var wd = set.Animations["WalkDown"];
                set.Animations.Add("StandDown", CreateStandingAnimation("Down", wd));
                var wl = set.Animations["WalkLeft"];
                set.Animations.Add("StandLeft", CreateStandingAnimation("Left", wl));
                var wr = set.Animations["WalkRight"];
                set.Animations.Add("StandRight", CreateStandingAnimation("Right", wr));
                var wu = set.Animations["WalkUp"];
                set.Animations.Add("StandUp", CreateStandingAnimation("Up", wu));
            }

            return set;
        }

        private Animation Convert(ProcessedAnimationData data)
            => new Animation(data.AnimationName, data.Frames.Select(CreateImageFromData).ToList(), 200);

        private Texture2D CreateImageFromData(ProcessedTexture data)
        {
            var texture = new Texture2D(_graphics.GraphicsDevice, data.Width, data.Height, false, data.Format);
            texture.SetData(data.Data);

            return texture;
        }

        private Animation CreateStandingAnimation(string direction, Animation animation)
        {
            return new Animation(
                "Stand" + direction,
                new List<Texture2D> { animation.Frames[1] },
                200);
        }
    }
}