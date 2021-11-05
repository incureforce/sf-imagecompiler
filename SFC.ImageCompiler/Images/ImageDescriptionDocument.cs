using System.Drawing;

namespace SFC.ImageCompiler
{
    public class ImageDescriptionDocument
    {
        public ImageDimensions Dimensions {
            get; set;
        }

        public ImageDescriptions Descriptions {
            get; set;
        } = new ImageDescriptions();

        public void AddImage(string name, int x, int y)
        {
            var description = new ImageDescription {
                Name = name,
                X = x,
                Y = y,
            };

            Descriptions.Add(description);
        }
    }

    public class ImageDimensions
    {
        public int W {
            get; set;
        }

        public int H {
            get; set;
        }
    }
}
