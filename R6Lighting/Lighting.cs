using System.Threading.Tasks;
using CUE.NET;
using System.Drawing;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Brushes;
using CUE.NET.Groups;
using CUE.NET.Devices.Generic.Enums;

namespace R6Lighting
{
    public struct Stuff
    {
        public static readonly CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
        public static readonly SolidColorBrush brushRed = new SolidColorBrush(Color.Red);
        public static readonly SolidColorBrush brushBlack = new SolidColorBrush(Color.Black);
        public static readonly SolidColorBrush brushWhite = new SolidColorBrush(Color.White);
        public static readonly SolidColorBrush brushGreen = new SolidColorBrush(Color.Green);
    }

    class LightingCtrl
    {
        public static string Initialize()
        {
            if (!CueSDK.IsInitialized)
            {
                try
                {
                    CueSDK.Initialize(true);
                }
                catch 
                {
                    return "Error initializing";
                }
            }
            CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
            if (keyboard == null)
            {
                return "Keyboard not found";
            }
            else
            {
                return "Connected";
            }
        }

        public static void BaseLighting()
        {
            CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
            SolidColorBrush brushBackground = new SolidColorBrush(Color.White);
            brushBackground.Brightness = 0.3F;
            keyboard.Brush = brushBackground; // Change all LEDs on keyboard to the brush
            keyboard.Update();
        }

        public static void GadgetLighting(int gadget)
        {
            ILedGroup LedGroup = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.G);
            if (gadget > 0)
            {
                LedGroup.Brush = Stuff.brushGreen; // Gadget still available
            }
            else
            {
                LedGroup.Brush = Stuff.brushWhite; // No more gadgets
            }
            Stuff.keyboard.Update();
        }

        public static void ReloadLighting(int ammo, int hp)
        {
            SolidColorBrush fade = new SolidColorBrush(Color.Green);
            ILedGroup LedGroup = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.R);
            if ((ammo <= 8) && (hp > 0) && (ammo >= 0)) // Check if the ammo amount in the mag is less or equal to 8
            {
                for(double i = 0; i<=1; i += 0.03) // Fade in
                {
                    fade.Brightness = (float)i;
                    LedGroup.Brush = fade;
                    Task.Delay(15).Wait();
                    Stuff.keyboard.Update();
                }
                for (double i = 1; i >= 0; i -= 0.03) // Fade out
                {
                    fade.Brightness = (float)i;
                    LedGroup.Brush = fade;
                    Task.Delay(15).Wait();
                    Stuff.keyboard.Update();
                }
            }
            else
            {
                LedGroup.Brush = new SolidColorBrush(Color.White); // If mag is full change the LED back to white
                Stuff.keyboard.Update();
            }
        }

        public static void HpLighting(int hp)
        {
            ILedGroup n1 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D1);
            ILedGroup n2 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D2);
            ILedGroup n3 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D3);
            ILedGroup n4 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D4);
            ILedGroup n5 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D5);
            ILedGroup n6 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D6);
            ILedGroup n7 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D7);
            ILedGroup n8 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D8);
            ILedGroup n9 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D9);
            ILedGroup n0 = new ListLedGroup(Stuff.keyboard, true, CorsairLedId.D0);
            SolidColorBrush lastBrush = new SolidColorBrush(Color.Red);
            if ((hp > 90) && (hp<=100)) // Changes the LEDs according to the amount of HP
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushRed;
                n6.Brush = Stuff.brushRed;
                n7.Brush = Stuff.brushRed;
                n8.Brush = Stuff.brushRed;
                n9.Brush = Stuff.brushRed;
                if(hp == 100) // Last LEDs brightness is adjusted according to the exact amount of HP
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n0.Brush = lastBrush;
            }
            else if ((hp <= 90) && (hp > 80))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushRed;
                n6.Brush = Stuff.brushRed;
                n7.Brush = Stuff.brushRed;
                n8.Brush = Stuff.brushRed;
                n0.Brush = Stuff.brushBlack;
                if (hp == 90)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n9.Brush = lastBrush;
            }
            else if ((hp <= 80) && (hp > 70))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushRed;
                n6.Brush = Stuff.brushRed;
                n7.Brush = Stuff.brushRed;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 80)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n8.Brush = lastBrush;
            }
            else if ((hp <= 70) && (hp > 60))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushRed;
                n6.Brush = Stuff.brushRed;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 70)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n7.Brush = lastBrush;
            }
            else if ((hp <= 60) && (hp > 50))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushRed;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 60)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n0.Brush = lastBrush;
            }
            else if ((hp <= 50) && (hp > 40))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushRed;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 50)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n5.Brush = lastBrush;
            }
            else if ((hp <= 40) && (hp > 30))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushRed;
                n5.Brush = Stuff.brushBlack;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 40)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n4.Brush = lastBrush;
            }
            else if ((hp <= 30) && (hp > 20))
            {
                n1.Brush = Stuff.brushRed;
                n2.Brush = Stuff.brushRed;
                n4.Brush = Stuff.brushBlack;
                n5.Brush = Stuff.brushBlack;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 30)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n3.Brush = lastBrush;
            }
            else if ((hp <= 20) && (hp > 10))
            {
                n1.Brush = Stuff.brushRed;
                n3.Brush = Stuff.brushBlack;
                n4.Brush = Stuff.brushBlack;
                n5.Brush = Stuff.brushBlack;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 20)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n2.Brush = lastBrush;
            }
            else if((hp <= 10) && (hp >= 1))
            {
                n2.Brush = Stuff.brushBlack;
                n3.Brush = Stuff.brushBlack;
                n4.Brush = Stuff.brushBlack;
                n5.Brush = Stuff.brushBlack;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
                if (hp == 10)
                {
                    lastBrush.Brightness = (float)1.0;
                }
                else
                {
                    int lastDigit = hp % 10;
                    double brightness = (double)lastDigit / 10;
                    lastBrush.Brightness = (float)brightness;
                }
                n1.Brush = lastBrush;
            }
            else
            {
                n1.Brush = Stuff.brushBlack;
                n2.Brush = Stuff.brushBlack;
                n3.Brush = Stuff.brushBlack;
                n4.Brush = Stuff.brushBlack;
                n5.Brush = Stuff.brushBlack;
                n6.Brush = Stuff.brushBlack;
                n7.Brush = Stuff.brushBlack;
                n8.Brush = Stuff.brushBlack;
                n9.Brush = Stuff.brushBlack;
                n0.Brush = Stuff.brushBlack;
            }
            Stuff.keyboard.Update();
        }
    }
}
