using System.IO;
using TMB.Display;

Display.Clear();
Display.SetShowCursor(false);

//Display In Top Left with padding for demo
void DisplayHint(string _text)
{
    Display.SetCursorPositionWithAnchors(_text, Display.EAnchors.TopLeft, 5, 1);
    Display.WriteLine(_text, EMode.BlinkText, new Color(Color.EPreset.White), new Color(Color.EPreset.Gray));
    Console.ReadLine();
    Display.Clear();
}

//Display In Center
string _enterText = "Press enter to continue the demo !";
Display.SetCursorPositionWithAnchors(_enterText, Display.EAnchors.Center);
Display.WriteLine(_enterText, EMode.BlinkText, new Color(21, 240, 12), new Color(2, 250, 147));
Console.ReadLine();


//Display in Rainbow
string _rainbowText = "                                                                    ";
Display.SetCursorPositionWithAnchors(_rainbowText, Display.EAnchors.Center);
Display.WriteLine(_rainbowText, EMode.Background,
    new Color(255, 0, 0), new Color(255, 255, 0),
    new Color(0, 255, 0), new Color(0, 255, 255),
    new Color(0, 0, 255), new Color(255, 0, 255),
    new Color(255, 0, 0));

DisplayHint("Display in Rainbow");

// Gradient 2D
for (int i = 0; i < Display.GetWindowSize().X; i++)
{
    for (int j = 0; j < Display.GetWindowSize().Y; j++)
    {
        Display.SetCursorPosition(i, j);
        Display.Write(" ", EMode.Background, new Color(i, j, 0));
    }
}

DisplayHint("Gradient 2D");

// All the Anchors
string text = "TEST PADDING WITH ANCHORS";
Display.SetCursorPositionWithAnchors(text, Display.EAnchors.TopCenter, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Yellow));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.TopLeft, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Red));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.TopRight, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Gray));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.CenterLeft, 2, 0);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Purple));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.Center, 2, 0);
Display.Write(text, EMode.Text, new Color(Color.EPreset.LightRed));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.CenterRight, 2, 0);
Display.Write(text, EMode.Text, new Color(Color.EPreset.LightPurple));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.BottomLeft, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.White));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.BottomCenter, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Orange));

Display.SetCursorPositionWithAnchors(text, Display.EAnchors.BottomRight, 2, 2);
Display.Write(text, EMode.Text, new Color(Color.EPreset.Dark));

DisplayHint("Display all the anchors");

//Display Sparkle

Display.SetCursorPositionWithAnchors("Sparkle text without sync", Display.EAnchors.Center);
Display.Sparkle("Sparkle text without sync", 13, false);

Display.Clear();

Display.SetCursorPositionWithAnchors("Sparkle text with sync", Display.EAnchors.Center);
Display.Sparkle("Sparkle text with sync", 13, true);