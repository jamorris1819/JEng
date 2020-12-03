// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary.Components
{
    public static class AudioManager
    {
        private static Dictionary<string, SoundEffect> sounds;
        private static Dictionary<string, SoundEffectInstance> soundsInstances;
        private static int timeLimit;
        private static int timer;
        private static bool loop;
        private static string loopKey;

        /// <summary>
        /// Sounds stored in the audio manager.
        /// </summary>
        public static Dictionary<string, SoundEffect> Sounds
        {
            get { return sounds; }
            set { sounds = value; }
        }

        /// <summary>
        /// Loop a sound.
        /// </summary>
        /// <param name="key">Key of the sound effect</param>
        public static void Loop(string key)
        {
            SoundEffect sound = sounds[key];
            if(loopKey == key)
                return;
            timeLimit = (int)sound.Duration.TotalMilliseconds;
            timer = timeLimit;
            loopKey = key;
            loop = true;
            Play(key);
        }

        /// <summary>
        /// Initializes the audio manager.
        /// </summary>
        public static void Initialize()
        {
            sounds = new Dictionary<string, SoundEffect>();
            soundsInstances = new Dictionary<string, SoundEffectInstance>();
            loop = false;
        }

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="key">Key of the sound effect</param>
        public static void Play(string key)
        {
            SoundEffectInstance effect = sounds[key].CreateInstance();
            effect.Play();
            if (soundsInstances.ContainsKey(key))
            {
                soundsInstances.Remove(key);
            }
            soundsInstances.Add(key, effect);
        }

        public static void StopAll()
        {
            foreach (SoundEffectInstance effect in soundsInstances.Values)
                effect.Stop();
            loopKey = "";
        }

        /// <summary>
        /// Updates the AudioManager
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            if (loop)
            {
                timer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer <= 0)
                {
                    timer = timeLimit;
                    Play(loopKey);
                }
            }
        }
    }
}
