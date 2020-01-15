using System;
using System.Collections.Generic;
using System.Text;

namespace MusicProject
{
    public class Music
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return Name + " | " + Author;
        }
    }
}
