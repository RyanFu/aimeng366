using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace All.Core
{
    /// <summary>
    /// 音频播放辅助类
    /// </summary>
    public class SoundPlayHelper
    {
        public static void Play(Stream st)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stream = st;
            player.Load();
            player.Play();
        }

        public static void Play(string filePath)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = filePath;
            player.Load();
            player.Play();
        }
    }
}
