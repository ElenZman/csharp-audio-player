using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        List<string> songList = new List<string>();
        Player player;
        
        //if no song is chosen in the listview, the player will play a song with this index
       //also it is used for Next button, that is why it's global
       int songCounter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (songList.Count <= 0)
            {
                MessageBox.Show("Please add at least one song!");
                button1.Enabled = false;
                return;
            }
            

            if (songCounter >= songList.Count) return;

            player = new Player(songList[songCounter]);
            player.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (player == null) return;
            player.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (songList.Count <= 0) return;
            
            songCounter++;

            if (songCounter >= songList.Count)
            {
                MessageBox.Show("End of the playlist.");
                return;
            }

            player = new Player(songList[songCounter]);
            player.Play();

        }

        //playing a song by clicking on it in the ListBox
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                if (listView1.SelectedItems[i].Selected)
                {
                    int songNumber = listView1.SelectedItems[i].Index;
                    player = new Player(songList[songNumber]);
                    player.Play();
                    listView1.SelectedItems[i].Selected = false;
                    songCounter = songNumber;
                    tb_playingNow.Text = player.SongName;
                }
            }

        }

       
        private void addNewSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNewSong();
        }

        //Adds new songs via openFileDialog from any catalogue
        /
        private void addNewSong()
        {
            //string path;
            OpenFileDialog newDialog = new OpenFileDialog()
            {
                Filter = "WAV|*.wav",
                Multiselect = true,
                ValidateNames = true
            };


            if (newDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in newDialog.FileNames)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        songList.Add(item);
                        int lastIndex = item.LastIndexOf("\\");
                        string shortPath = item.Substring(lastIndex + 1);
                        listView1.Items.Add(shortPath);
                    }
                }
            }
            else
            {
                return;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            songList.Clear();
            listView1.Clear();
        }
    }
}
