using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SAVE_FILE_NAME = @"C:\Users\толеутайб\Music\MediaProject";
        private Music currentMusic;
        public MainWindow()
        {
            InitializeComponent();

            using var context = new Context();
            Fill(context);

            foreach (var melody in context.Musics)
            {
                musics.Items.Add(melody);
            }

            if (!File.Exists(SAVE_FILE_NAME))
            {
                Directory.CreateDirectory(SAVE_FILE_NAME);
            }
        }

        private static async Task Fill(Context context)
        {
            var melodies = new List<Music>
            {
                new Music {
                Name = "Alone",
                Author = "Alan Walker",
                Path = "https://muzoff.net/uploads/files/2019-04/1554365257_alan-walker-alone.mp3"
                },
                new Music {
                Name = "Alone",
                Author = "Marshmello",
                Path = "https://dl1.mp3party.net/download/3376856"
                },
                new Music {
                Name = "Believer",
                Author = "Imagine Dragon",
                Path = "http://mp3-muzon.com/dl_save.php?filename=mp3/00-03/Imagine_Dragons_-_Believer_(mp3-muzon.com).mp3"
                },
                new Music {
                Name = "Thunder",
                Author = "Imagine Dragon",
                Path = "https://music.я.ws/public/download_song.php?id=371745442_456391522&title=Thunder&artist=Imagine%20dragons&hash=18810a30482f64107ad154f5ac7bb9742b6ae07247d01213c6d9b3544740efcf"
                },
                new Music {
                Name = "Fade",
                Author = "Alan Walker",
                Path = "https://muzdom.co/download.php?id=1010"
                },
            };

            context.AddRange(melodies);
            await context.SaveChangesAsync();
        }

        private void PlayMusic(object sender, RoutedEventArgs e)
        {
            if (currentMusic != null)
            {
                if (!File.Exists(SAVE_FILE_NAME + '\\' + currentMusic.Name + '_' + currentMusic.Id + ".mp3"))
                {
                    DownloadMusic(currentMusic);
                    load.Visibility = Visibility.Visible;
                    load.Text = "Loading";
                }
                else
                {
                    load.Text = "Cool!";
                }
                music.Source = new Uri(SAVE_FILE_NAME + '\\' + currentMusic.Name + '_' + currentMusic.Id + ".mp3");
                music.Play();
            }
        }

        private void SelectMusic(object sender, MouseButtonEventArgs e)
        {
            if (musics.SelectedItem != null)
            {
                currentMusic = musics.SelectedItem as Music;
                MessageBox.Show("Selected");
            }
        }

        private static async Task DownloadMusic(Music temp)
        {
            WebClient webBrowser = new WebClient();
            await webBrowser.DownloadFileTaskAsync(temp.Path, SAVE_FILE_NAME + '\\' + temp.Name + '_' + temp.Id + ".mp3");
            MessageBox.Show("Done");
        }
    }
}
