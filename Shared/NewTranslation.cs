using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    internal class NewTranslation
    {
            public class Root
    {
        public List<Translation> translations { get; set; }
    }

    public class Translation
    {
        public string text { get; set; }
        public string to { get; set; }
    }


    }
}
