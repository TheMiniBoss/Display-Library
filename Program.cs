using System.IO;
using TMB.Display;

//Display.WriteLine("                                                                    ", EColorizerMode.Background,
//    new Color(Color.EColorPreset.Red), new Color(0, 255, 0),
//    new Color(0, 0, 255), new Color(255, 255, 0),
//    new Color(255, 0, 255), new Color(0, 255, 255),
//    new Color(255, 255, 255));

//Display display = new Display();

//for (int i = 0; i < Display.GetWindowSize().X; i++)
//{
//    for (int j = 0; j < Display.GetWindowSize().Y; j++)
//    {
//        Display.SetCursorPosition(i, j);
//        Display.Write(" ", ETextColorMode.Background, new Color(i, j, 0));
//    }
//} // Gradient 2D

Display.SetShowCursor(false);

string text = "TEST PADDING\n coucou";
//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.TopCenter, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Yellow));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.TopLeft, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Red));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.TopRight, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Gray));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.CenterLeft, 2, 0);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Purple));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.Center, 2, 0);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.LightRed));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.CenterRight, 2, 0);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.LightPurple));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.BottomLeft, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.White));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.BottomCenter, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Orange));

//Display.SetCursorPositionWithAnchors(text, Display.EDisplayAnchors.BottomRight, 2, 2);
//Display.Write(text, ETextColorMode.Text, new Color(Color.EColorPreset.Dark));

Display.Sparkle(text, 27);

//Display.WaitKey(27);