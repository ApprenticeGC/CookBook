namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class MapData : MonoBehaviour
    {
        public int width = 10;
        public int height = 5;

        public TextAsset textAsset;

        public List<string> GetTextFromFile(TextAsset ta)
        {
            var lines = new List<string>();

            if (ta != null)
            {
                var textData = ta.text;

                string[] delimiters = {"\r\n", "\n"};

                lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
                lines.Reverse();
            }
            else
            {
                Debug.LogWarning($"GetTextFromFile - invalid text asset");
            }

            return lines;
        }

        public List<string> GetTextFromFile()
        {
            return GetTextFromFile(textAsset);
        }

        public void SetDimensions(List<string> textLines)
        {
            height = textLines.Count;
            foreach (var line in textLines)
            {
                if (line.Length > width)
                {
                    width = line.Length;
                }
            }
        }

        public int[,] MakeMap()
        {
            var lines = new List<string>();
            lines = GetTextFromFile();
            SetDimensions(lines);

            // var desc = lines.Aggregate("", (acc, next) => { return $"{acc}\n {next}"; });
            // Debug.Log(desc);
            
            int[,] map = new int[width, height];

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    // map[x, y] = 0;
                    if (lines[y].Length > x)
                    {
                        var c = lines[y][x];
                        var v = int.Parse(c.ToString());
                        map[x, y] = v;
                    }
                }
            }
            
            //
            // MakeBlockStub(map);
            
            
            return map;
        }

        private void MakeBlockStub(int[,] map)
        {
            //
            map[1, 0] = 1;
            map[1, 1] = 1;
            map[1, 2] = 1;
            map[3, 2] = 1;
            map[3, 3] = 1;
            map[3, 4] = 1;
            map[4, 2] = 1;
            map[5, 1] = 1;
            map[5, 2] = 1;
            map[6, 2] = 1;
            map[6, 3] = 1;
            map[8, 0] = 1;
            map[8, 1] = 1;
            map[8, 2] = 1;
            map[8, 4] = 1;
        }
    }
}
