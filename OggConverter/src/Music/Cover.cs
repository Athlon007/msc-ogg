// MSC Music Manager
// Copyright(C) 2019 Athlon

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.

using System;
using System.Drawing;

namespace OggConverter
{
    class Cover
    {
        /// <summary>
        /// Stores the scale of the currently used coverart.png, compared to the original 512x512 image.
        /// So if the image is 1024x1024, the scale used is 2x
        /// And if the image is 256x256, the scale is 0.5x
        /// </summary>
        public double Scale { get; set; }

        Bitmap CoverArt { get; set; }

        /// <summary>
        /// Creates new cover art based of saved coverart.png
        /// </summary>
        /// <param name="cdText"></param>
        /// <param name="folder"></param>
        public void New(string cdText, string folder)
        {
            Form1.instance.Log(Localisation.Get("Creating new cover art for {0}...", folder));

            // Loading the coverart.png image
            CoverArt = (Bitmap)Image.FromFile($"coverart.png");
            // Calculate the cover art scale compared to the default MSC CD cover art (512x512)
            Scale = (double)CoverArt.Width / (double)512;

            // Default starting point for the list of songs (rescaled according to ScaleValue)
            PointF defaultPoint = new PointF(Rescale(274), Rescale(40));
            PointF currentPoint = defaultPoint;
            // By how many pixels does new line appear (scaled)
            int jumpBy = Rescale(19);
            // Maximum ammount of sogns
            int maxSongs = 11;

            // Initialziing Graphics
            using (Graphics graphics = Graphics.FromImage(CoverArt))
            {
                Font font = new Font(Settings.CoverArtFont, Rescale(10));
                // Drawing the text on the CD from cdText
                graphics.DrawString(cdText, new Font(Settings.CoverArtFont,
                    Rescale(20)), 
                    Brushes.Black, 
                    new PointF(Rescale(299),
                    Rescale(415)));

                // Drawing the song names on CD cover
                for (int i = 0; (i < maxSongs) && (i < Player.WorkingSongList.Count); i++)
                {
                    string songName = Player.WorkingSongList[i].Item2;                    
                    graphics.DrawString(songName, font, Brushes.Black, currentPoint);
                    currentPoint.Y += jumpBy;
                }
            }

            // Saving the edited cover art to the folder
            CoverArt.Save($"{Settings.GamePath}\\{folder}\\coverart.png");
            CoverArt.Dispose();

            Form1.instance.Log(Localisation.Get("Done!"));
            System.Windows.Forms.MessageBox.Show(Localisation.Get("Created CD cover art successfully!"));
        }

        /// <summary>
        /// Multipleis the value by the ScaleValue.
        /// </summary>
        /// <param name="value">Value to scale</param>
        /// <returns></returns>
        public int Rescale(object value)
        {
            return (int)Math.Round(Convert.ToDouble(value) * Scale);
        }

        /// <summary>
        /// Retrieves the info about image from file.
        /// </summary>
        /// <returns></returns>
        public string GetImageInfo()
        {
            return Localisation.Get("Resolution: {0}x{1}\nScale: {2}x", CoverArt.Width, CoverArt.Height, Scale);
        }

        /// <summary>
        /// Retrieves the info about image from file.
        /// </summary>
        /// <returns></returns>
        public string GetImageInfo(Image image)
        {
            return Localisation.Get("Resolution: {0}x{1}\nScale: {2}x", image.Width, image.Height, (double)image.Width / (double)512);
        }
    }
}
