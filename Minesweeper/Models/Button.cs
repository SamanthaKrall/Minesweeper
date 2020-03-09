using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Models
{
    public class Button
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public int Neighbors { get; set; }
        public string Text { get; set; }
        public string Id { get; set; }
        public bool Win { get; set; }

        public Button()
        {
            this.Row = -1;
            this.Column = -1;
            this.Visited = false;
            this.Live = false;
            this.Neighbors = 0;
            this.Text = "";
            this.Id = "null";
            this.Win = false;
        }
    }
}