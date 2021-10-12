using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using System.Drawing;

namespace MusicPlayer
{
    class Player
    {

        public string SongName { get; private set; }
        private SoundPlayer _player;

        public Player(string path)
        {
            _player = new SoundPlayer(path);
            int lastIndex = path.LastIndexOf("\\");
            string shortPath = path.Substring(lastIndex + 1);
            SongName = shortPath;// only file name, not the whole path
        }

        public void Play()
        {
            _player.Play();
        }

        public void Stop()
        {
            _player.Stop();
        }
    }
}
