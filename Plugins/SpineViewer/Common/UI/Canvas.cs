using Microsoft.Xna.Framework.Content;

namespace SpineViewer.Common.UI
{
    public class Canvas
    {
        private ContentManager _content;

        public TextureManager TexMan { get; set; }

        public Canvas(ContentManager content)
        {
            TexMan = new TextureManager();
            _content = content;
        }
    }
}
