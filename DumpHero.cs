using SFML.System;

namespace MyGame;
using SFML.Graphics;
public class DumpHero : Hero
{
    private Sprite destroyedH;
    private Sprite destroyedV;
    private uint counter = 0;
    public DumpHero()
    {
        // 1. Load original texture
        Texture originalTexture = Constants.texture;

        // 2. Copy to Image
        Image originalImage = originalTexture.CopyToImage();

        // 3. Make black transparent
        Image transparentImage = MakeTransparent(originalImage);

        // 4. Create new Texture
        Texture transparentTexture = new Texture(transparentImage);


        
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
        if (counter++ % 2 == 0)
            window.Draw(destroyedH);
        else    
            window.Draw(destroyedV);
    }
    
    Image MakeTransparent(Image original)
    {
        Image newImage = new Image(original.Size.X, original.Size.Y);

        for (uint y = 0; y < original.Size.Y; y++)
        {
            for (uint x = 0; x < original.Size.X; x++)
            {
                Color pixel = original.GetPixel(x, y);

                if (pixel.R < 50 && pixel.G < 50 && pixel.B < 50)
                {
                    newImage.SetPixel(x, y, new Color(0, 0, 0, 0)); // Fully transparent
                }
                else
                {
                    newImage.SetPixel(x, y, pixel);
                }
            }
        }

        return newImage;
    }

    
    public override void Update() {}
}