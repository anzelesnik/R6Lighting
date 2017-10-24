# R6Lighting
This is just a simple project I am working on, which get's data from the game Rainbow Six Siege and turns it into effects on Corsair keyboards with the help of CUE SDK and CUE.NET library by @DarthAffe.

## Current Support

Currently the program displays the following on the keyboard:

-Health (numbers from 1 to 0, adjusted brightness)

-Ammo ('R' fades when a reload is suggested)

-Secondary Gadget ('G' is red when gadget is still available)

-TeamID (background lighting adjusted according to the team LocalPlayer is in)

## Battleye Bypass

I have implemented hLeaker by @Schnocker in the Launcher. I suggest you at least encrypt the shellcodes before using it. You have to open the game first, then R6Lighting. Also, currently you have to hardcode in the base address for the game because I can't get it to work by getting it dynamically for some reason (check R6Lighting/MemoryRead.cs). The launcher will probably crash after opening R6Lighting! Working on a fix.

**WARNING!** There is a chance of getting banned. I am not responsible for any bans, neither is @Schnocker!

## Notes

I will be adding new features in the future as I currently have free time and I'm doing this for fun and to practice C# :)
Fell free to to use this crappy source code if you want and opinions and improvement ideas are welcome.

**After compiling you need to include the DLL and the folders found in bin/x64/Release in the executable folder!**

**Also Contents of bin/x64/Release once compiled, need to be placed inside a folder called "Stuff" which needs to be in the same directory as "Launcher.exe"**



