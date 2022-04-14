using System.Collections.Generic;

namespace AbstractForgeBusinessLogic.OfficePackage.HelperModels
{
    public class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; }
        public WordTextProperties TextProperties { get; set; }
    }

}
