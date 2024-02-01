using UnityEngine;

namespace SiberUtility.Tools
{
    public static class ParticleSystemHelper
    {
        /// <summary> 停止特效後，在播放特效 </summary>
        public static void SafePlay(this ParticleSystem particleSystem)
        {
            if (particleSystem == null) return;
            if (particleSystem.isPlaying) particleSystem.Stop();
            particleSystem.Play();
        }
    }
}