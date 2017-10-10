using System.Threading.Tasks;
using CUE.NET;
using System.Drawing;
using CUE.NET.Devices.Keyboard;

namespace R6Lighting
{
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
            CUE.NET.Brushes.SolidColorBrush brush = new CUE.NET.Brushes.SolidColorBrush(Color.White);
            keyboard.Brush = brush; // Change all LEDs on keyboard to the brush
            keyboard.Update();
        }

        public static void ReloadLighting(int ammo, int hp)
        {
            CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
            CUE.NET.Brushes.SolidColorBrush fade = new CUE.NET.Brushes.SolidColorBrush(Color.Green);
            CUE.NET.Groups.ILedGroup LedGroup = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.R);
            if ((ammo <= 8) && (hp > 0)) // Check if the ammo amount in the mag is less or equal to 8
            {
                for(double i = 0; i<=1; i += 0.03) // Fade in
                {
                    fade.Brightness = (float)i;
                    LedGroup.Brush = fade;
                    Task.Delay(15).Wait();
                    keyboard.Update();
                }
                for (double i = 1; i >= 0; i -= 0.03) // Fade out
                {
                    fade.Brightness = (float)i;
                    LedGroup.Brush = fade;
                    Task.Delay(15).Wait();
                    keyboard.Update();
                }
            }
            else
            {
                LedGroup.Brush = new CUE.NET.Brushes.SolidColorBrush(Color.White); // If mag is full change the LED back to white
                keyboard.Update();
            }
        }

        public static void HpLighting(int hp)
        {
            CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
            CUE.NET.Groups.ILedGroup n1 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D1); // Damn, I should really make this stuff non-static :/
            CUE.NET.Groups.ILedGroup n2 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D2);
            CUE.NET.Groups.ILedGroup n3 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D3);
            CUE.NET.Groups.ILedGroup n4 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D4);
            CUE.NET.Groups.ILedGroup n5 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D5);
            CUE.NET.Groups.ILedGroup n6 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D6);
            CUE.NET.Groups.ILedGroup n7 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D7);
            CUE.NET.Groups.ILedGroup n8 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D8);
            CUE.NET.Groups.ILedGroup n9 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D9);
            CUE.NET.Groups.ILedGroup n0 = new CUE.NET.Groups.ListLedGroup(keyboard, true, CUE.NET.Devices.Generic.Enums.CorsairLedId.D0);
            CUE.NET.Brushes.SolidColorBrush brushRed = new CUE.NET.Brushes.SolidColorBrush(Color.Red);
            CUE.NET.Brushes.SolidColorBrush lastBrush = new CUE.NET.Brushes.SolidColorBrush(Color.Red);
            CUE.NET.Brushes.SolidColorBrush brushBlack = new CUE.NET.Brushes.SolidColorBrush(Color.Black);
            if ((hp > 90) && (hp<=100)) // Changes the LEDs according to the amount of HP
            {
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n5.Brush = brushRed;
                n6.Brush = brushRed;
                n7.Brush = brushRed;
                n8.Brush = brushRed;
                n9.Brush = brushRed;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n5.Brush = brushRed;
                n6.Brush = brushRed;
                n7.Brush = brushRed;
                n8.Brush = brushRed;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n5.Brush = brushRed;
                n6.Brush = brushRed;
                n7.Brush = brushRed;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n5.Brush = brushRed;
                n6.Brush = brushRed;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n5.Brush = brushRed;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n4.Brush = brushRed;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n3.Brush = brushRed;
                n5.Brush = brushBlack;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n2.Brush = brushRed;
                n4.Brush = brushBlack;
                n5.Brush = brushBlack;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushRed;
                n3.Brush = brushBlack;
                n4.Brush = brushBlack;
                n5.Brush = brushBlack;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n2.Brush = brushBlack;
                n3.Brush = brushBlack;
                n4.Brush = brushBlack;
                n5.Brush = brushBlack;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
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
                n1.Brush = brushBlack;
                n2.Brush = brushBlack;
                n3.Brush = brushBlack;
                n4.Brush = brushBlack;
                n5.Brush = brushBlack;
                n6.Brush = brushBlack;
                n7.Brush = brushBlack;
                n8.Brush = brushBlack;
                n9.Brush = brushBlack;
                n0.Brush = brushBlack;
            }
            keyboard.Update();
        }
    }
}
