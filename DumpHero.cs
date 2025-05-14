using SFML.System;

namespace MyGame;
using SFML.Graphics;
public class DumpHero : Hero
{
    private readonly Sprite destroyedH;
    private readonly Sprite destroyedV;
    private uint counter;
    public DumpHero()
    {
        var originalTexture = Constants.texture;
        var originalImage = originalTexture.CopyToImage();
        var transparentImage = MakeTransparent(originalImage);
        var transparentTexture = new Texture(transparentImage);
       
        destroyedH = new Sprite(transparentTexture)
        {
            TextureRect = Constants.destroyedTankCoverH, 
            Scale = Constants.tankScale
        };
        destroyedV = new Sprite(transparentTexture)
        {
            TextureRect = Constants.destroyedTankCoverV, 
            Scale = Constants.tankScale
        };
        destroyedH.Position = new Vector2f(Constants.heroInitPositionX, Constants.heroInitPositionY);
        destroyedV.Position = new Vector2f(Constants.heroInitPositionX, Constants.heroInitPositionY);
    }

    public override void Display(RenderWindow window)
    {
        window.Draw(tankSprite);
        window.Draw(counter++ % 2 == 0 ? destroyedH : destroyedV);
    }

    private static Image MakeTransparent(Image original)
    {
        var newImage = new Image(original.Size.X, original.Size.Y);

        for (uint y = 0; y < original.Size.Y; y++)
        {
            for (uint x = 0; x < original.Size.X; x++)
            {
                var pixel = original.GetPixel(x, y);
                if (pixel is { R: < 50, G: < 50, B: < 50 })
                {
                    pixel = new Color(0, 0, 0, 0);
                }
                newImage.SetPixel(x, y, pixel);
            }
        }

        return newImage;
    }

    
    public override void Update() {}
}